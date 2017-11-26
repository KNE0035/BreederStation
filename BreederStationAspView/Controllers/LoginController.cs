using BreederStationAspView.Models;
using BreederStationBussinessLayer.Service;
using System.Web.Mvc;

namespace BreederStationAspView.Controllers
{
    public class LoginController : Controller
    {
        PersonService personService = ServiceRegister.getInstance().Get<PersonService>();

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string Login, string Password)
        {
            BreederStationBussinessLayer.Domain.Person person = personService.Authorize(Login, Password);
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
            return RedirectToAction("Login", "Login");
        }

        public ActionResult ChangePasswordForm()
        {
            if(Session["user"] == null)
            {
                return RedirectToAction("Login", "Login");
            } 
            return View();
        }

        public ActionResult ChangePassword(User user)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            User sessionedUser = Session["user"] as User;
            personService.UpdateUserPassword(sessionedUser.Login, user.Password);
            Session.Clear();
            return RedirectToAction("Login", "Login");
        }
    }
}