using PagedList;
using Project.BLL.DesignPatterns.GenericRepository.ConcRep;
using Project.COMMON.Tools;
using Project.ENTITIES.Models;
using Project.MVCUI.ShoppingTools;
using Project.MVCUI.VMClasses;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace Project.MVCUI.Controllers
{
    public class ShoppingController : Controller
    {
        OrderRepository _oRep;
        ProductRepository _pRep;
        CategoryRepository _cRep;
        OrderDetailRepository _odRep;

        public ShoppingController()
        {
            _oRep = new OrderRepository();
            _pRep = new ProductRepository();
            _cRep = new CategoryRepository();
            _odRep = new OrderDetailRepository();
        }

        // GET: Shopping
        public ActionResult ShoppingList(int? page, int? categoryID)
        {
            PaginationVM pgvm = new PaginationVM
            {
                PagedProducts = categoryID == null ? _pRep.GetActives().ToPagedList(page ?? 1, 9) : _pRep.Where(x => x.CategoryID == categoryID).ToPagedList(page ?? 1, 9),
                Categories = _cRep.GetActives()
            };

            if (categoryID != null) TempData["catID"] = categoryID;

            return View(pgvm);
        }


        public PartialViewResult PartialShoppingList(int? page, int? categoryID, string search, string sortOrder)
        {
            PaginationVM pgvm;

            if (search != null)
            {
                pgvm = new PaginationVM
                {
                    PagedProducts = categoryID == null ? _pRep.Where(x => x.ProductName.Contains(search)).ToPagedList(page ?? 1, 9) : _pRep.Where(x => x.CategoryID == categoryID).ToPagedList(page ?? 1, 9),
                    Categories = _cRep.GetActives()
                };
            }
            else
            {
                pgvm = new PaginationVM { Categories = _cRep.GetActives() };

                switch (sortOrder)
                {

                    case "artan":
                        pgvm.PagedProducts = categoryID == null ? _pRep.GetActives().OrderBy(x => x.UnitPrice).ToPagedList(page ?? 1, 9) : _pRep.Where(x => x.CategoryID == categoryID).OrderBy(x => x.UnitPrice).ToPagedList(page ?? 1, 9);
                        break;
                    case "azalan":
                        pgvm.PagedProducts = categoryID == null ? _pRep.GetActives().OrderByDescending(x => x.UnitPrice).ToPagedList(page ?? 1, 9) : _pRep.Where(x => x.CategoryID == categoryID).OrderByDescending(x => x.UnitPrice).ToPagedList(page ?? 1, 9);
                        break;
                    default:
                        pgvm.PagedProducts = categoryID == null ? _pRep.GetActives().ToPagedList(page ?? 1, 9) : _pRep.Where(x => x.CategoryID == categoryID).ToPagedList(page ?? 1, 9);
                        break;
                }
            }

            if (categoryID != null) TempData["catID"] = categoryID;
            if (sortOrder != null) TempData["sortOrder"] = sortOrder;

            return PartialView("_ShoppingListPartial", pgvm);

        }

        public ActionResult AddToCart(int id)
        {
            Cart c = Session["scart"] == null ? new Cart() : Session["scart"] as Cart;

            Product eklenecekUrun = _pRep.Find(id);

            CartItem ci = new CartItem
            {
                ID = eklenecekUrun.ID,
                Name = eklenecekUrun.ProductName,
                Price = eklenecekUrun.UnitPrice,
                ImagePath = eklenecekUrun.ImagePath
            };

            c.SepeteEkle(ci);
            Session["scart"] = c;

            return RedirectToAction("ShoppingList");
        }

        public ActionResult CartPage()
        {
            if (Session["scart"] != null)
            {
                CartPageVM cpvm = new CartPageVM();
                Cart c = Session["scart"] as Cart;
                cpvm.Cart = c;
                return View(cpvm);
            }

            TempData["bos"] = "Sepetinizde ürün bulunmamaktadır.";

            return RedirectToAction("ShoppingList");
        }

        public ActionResult DeleteFromCart(int id)
        {
            if (Session["scart"] != null)
            {
                Cart c = Session["scart"] as Cart;
                c.SepettenSil(id);
                if(c.Sepetim.Count == 0)
                {
                    Session.Remove("scart");
                    TempData["sepetBos"] = "Sepetinizde ürün bulunmamaktadır";
                    return RedirectToAction("ShoppingList");
                }
                return RedirectToAction("CartPage");
            }

            return RedirectToAction("ShoppingList");
        }

        public ActionResult SiparisiOnayla()
        {
            AppUser mevcutKullanici;

            if (Session["member"] != null)
            {
                mevcutKullanici = Session["member"] as AppUser;
            }
            else TempData["anonim"] = "Kullanıcı üye değil";

            return View();
        }

        //https://localhost:44395/api/Payment/ReceivePayment

        //WebApiRestService.WebApiClient kütüphanesini indir. API ile bu kütüphane sayesinde BackEnd'in haberleşmesi sağlanır.
        [HttpPost]
        public ActionResult SiparisiOnayla(OrderVM ovm)
        {
            bool result;
            Cart sepet = Session["scart"] as Cart;

            if (Session["member"] != null)
            {
                AppUser kullanici = Session["member"] as AppUser;

                ovm.Order.Email = kullanici.Email;
                ovm.Order.UserName = kullanici.UserName;
                ovm.Order.AppUserID = kullanici.ID;
            }
            else ovm.Order.UserName = TempData["anonim"].ToString();

            ovm.Order.TotalPrice = ovm.PaymentDTO.ShoppingPrice = sepet.TotalPrice;

            #region APISection

            using(HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44395/api/");

                Task<HttpResponseMessage> postTask = client.PostAsJsonAsync("Payment/ReceivePayment", ovm.PaymentDTO);

                HttpResponseMessage sonuc;

                try
                {
                    sonuc = postTask.Result;
                }
                catch (Exception ex)
                {
                    TempData["baglantiRed"] = "Banka bağlantıyı reddetti";
                    return RedirectToAction("ShoppingList");
                }

                if (sonuc.IsSuccessStatusCode) result = true;
                else result = false;

                if (result)
                {
                    _oRep.Add(ovm.Order);

                    foreach (CartItem item in sepet.Sepetim)
                    {
                        OrderDetail od = new OrderDetail();

                        od.OrderID = ovm.Order.ID;
                        od.ProductID = item.ID;
                        od.SubTotal = item.SubTotal;
                        od.Quantity = item.Amount;
                        _odRep.Add(od);

                        //Stoktan Düşürme
                        Product stokDus = _pRep.Find(item.ID);
                        stokDus.UnitsInStock -= item.Amount;
                        _pRep.Update(stokDus);
                    }

                    TempData["odeme"] = "Siparişiniz bize ulaşmıştır... Teşekkür ederiz...";
                    MailService.Send(ovm.Order.Email, body: $"Siparişiniz başarıyla alındı...{ovm.Order.TotalPrice}");
                    Session.Remove("scart");

                    return RedirectToAction("ShoppingList");

                }
                else
                {
                    TempData["sorun"] = "Ödeme ile ilgili bir sorun oluştu... Lütfen bankanızla iletişime geçiniz...";
                    return RedirectToAction("ShoppingList");
                }

            }

            #endregion

        }

    }
}