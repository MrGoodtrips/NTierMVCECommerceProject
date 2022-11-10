using Project.BLL.DesignPatterns.GenericRepository.ConcRep;
using Project.COMMON.Tools;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.Controllers
{
    public class RegisterController : Controller
    {
        AppUserRepository _apRep;
        AppUserProfileRepository _apProRep;

        public RegisterController()
        {
            _apRep = new AppUserRepository();
            _apProRep = new AppUserProfileRepository();
        }

        
        public ActionResult RegisterNow()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegisterNow(AppUser appUser, AppUserProfile appUserProfile)
        {
            appUser.ActivationCode = Guid.NewGuid();
            appUser.Password = DantexCrypt.Crypt(appUser.Password);

            //if (_apRep.Any(x => x.UserName == appUser.UserName) || _apRep.Any(x => x.Email == appUser.Email))
            //{
            //    ViewBag.ZatenVar = "Kullanıcı zaten kayıtlı";
            //}

            if (_apRep.Any(x => x.UserName == appUser.UserName))
            {
                ViewBag.ZatenVar = "Kullanıcı ismi daha önce alnımış";
            }
            else if (_apRep.Any(x => x.Email == appUser.Email))
            {
                ViewBag.ZatenVar = "Email zaten kayıtlı";
            }

            string gonderilecekMail = "Tebrikler... Hesabınız oluşturulmuştur... Hesabınızı aktive etmek için https://localhost:44396/Register/Activation/" +appUser.ActivationCode+ " linkine tıklayabilirsiniz";

            MailService.Send(appUser.Email, body: gonderilecekMail, subject: "Hesap aktivasyon!!!");

            _apRep.Add(appUser);

            if (!string.IsNullOrEmpty(appUserProfile.FirstName.Trim()) || !string.IsNullOrEmpty(appUserProfile.LastName.Trim()))
            {
                appUserProfile.ID = appUser.ID;
                _apProRep.Add(appUserProfile);
            }

            return View("RegisterOk");
        }

        public ActionResult Activation(Guid id)
        {
            AppUser aktifEdilecek = _apRep.FirstOrDefault(x => x.ActivationCode == id);

            if (aktifEdilecek != null)
            {
                aktifEdilecek.Active = true;
                _apRep.Update(aktifEdilecek);
                TempData["HesapAktifMi"] = "Hesabınız aktif hale getirilmiştir";
                return RedirectToAction("Login", "Home");
            }

            TempData["HesapAktifMi"] = "Hesabınız bulunamadı";
            return RedirectToAction("Login", "Home");
        }

        public ActionResult RegisterOk()
        {
            return View();
        }

    }
}