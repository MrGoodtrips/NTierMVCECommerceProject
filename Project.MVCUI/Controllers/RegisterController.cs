using Project.BLL.DesignPatterns.GenericRepository.ConcRep;
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
            return View();
        }
    }
}