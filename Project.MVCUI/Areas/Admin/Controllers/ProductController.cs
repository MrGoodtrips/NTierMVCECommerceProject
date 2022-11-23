using Project.BLL.DesignPatterns.GenericRepository.ConcRep;
using Project.COMMON.Tools;
using Project.ENTITIES.Models;
using Project.MVCUI.Areas.Admin.Data.AdminVMClasses;
using Project.MVCUI.AuthenticationClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.Areas.Admin.Controllers
{
    [AdminAuthentication]
    public class ProductController : Controller
    {
        ProductRepository _pRep;
        CategoryRepository _cRep;

        public ProductController()
        {
            _pRep= new ProductRepository();
            _cRep= new CategoryRepository();
        }

        //Parametre olarak istenen id CategoryID dir, ProductID değildir.
        public ActionResult ProductList(int? id)
        {
            ProductVM pvm = new ProductVM
            {
                Products = id == null ? _pRep.GetActives() : _pRep.Where(x => x.CategoryID == id)
            };

            return View(pvm);
        }

        public ActionResult AddProduct()
        {
            ProductVM pvm = new ProductVM
            {
                Categories = _cRep.GetActives()
            };

            return View(pvm);
        }

        [HttpPost]
        public ActionResult AddProduct(Product product, HttpPostedFileBase image)
        {
            product.ImagePath = ImageUploader.UploadImage("/Pictures/", image);
            _pRep.Add(product);

            return RedirectToAction("ProductList");
        }

        public ActionResult UpdateProduct(int id)
        {
            ProductVM pvm = new ProductVM
            {
                Categories = _cRep.GetActives(),
                Product = _pRep.Find(id)
            };

            return View(pvm);
        }

        [HttpPost]
        public ActionResult UpdateProduct(Product product, HttpPostedFileBase image, bool? remember)
        {
            if (remember == null)
            {
                remember = false;
            }

            if (remember.Value) //CheckBox tikli ise
            {
                Product origin = _pRep.Find(product.ID);
                product.ImagePath = origin.ImagePath;
            }
            else
            {
                if (image== null) //Resim seçilmediyse
                {
                    product.ImagePath = "~/Pictures/Sekiro.jpg";
                }
                else
                {
                    product.ImagePath = ImageUploader.UploadImage("/Pictures/", image);
                }
            }

            _pRep.Update(product);

            return RedirectToAction("ProductList");
        }

        public ActionResult DeleteProduct(int id)
        {
            _pRep.Delete(_pRep.Find(id));

            return RedirectToAction("ProductList");
        }

        public ActionResult DestroyProduct(int id)
        {
            _pRep.Destroy(_pRep.Find(id));

            return RedirectToAction("ProductList");
        }

    }
}