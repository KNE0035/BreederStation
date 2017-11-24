using BreederStationAspView.Models;
using BreederStationBussinessLayer.Domain;
using BreederStationBussinessLayer.Service;
using BreederStationBussinessLayer.Service.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BreederStationAspView.Controllers
{
    public class LoginController : Controller
    {
        PersonService personService = ServiceRegister.getInstance().Get<PersonService>();

        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string Login, string Password)
        {
            Person person = personService.Authorize(Login, Password);
            if(person != null)
            {
                Session["user"] = new User() { Id = person.Id, Login = person.Login, Role = person.Role.Type };
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}