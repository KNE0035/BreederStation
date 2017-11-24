using BreederStationAspView.Models;
using BreederStationBussinessLayer.Domain;
using BreederStationBussinessLayer.Domain.Enums;
using BreederStationBussinessLayer.Service;
using BreederStationBussinessLayer.Service.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BreederStationAspView.Controllers
{
    public class PersonController : Controller
    {
        PersonService personService = ServiceRegister.getInstance().Get<PersonService>();
        AnimalGroupService animalGroupService = ServiceRegister.getInstance().Get<AnimalGroupService>();

        // GET: Person
        public ActionResult PersonList()
        {
            if (isUserAdminOrDirector())
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Funds = personService.GetAllUsers();

            return View();
        }

        public ActionResult Delete(int? id)
        {
            int ids = (int)id;
            if (isUserAdminOrDirector())
            {
                return RedirectToAction("Index", "Home");
            }

            personService.RemovePerson(ids);

            return RedirectToAction("PersonList", "Person");
        }

        public ActionResult AddPersonForm(Person person)
        {
            if (isUserAdminOrDirector())
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.possibleGroups = animalGroupService.GetAllAnimalGroups(false);

            return View();
        }

        public ActionResult AddPerson(Person person)
        {
            if (isUserAdminOrDirector())
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("PersonList", "Person");
        }


        private bool isUserAdminOrDirector() {
            User user = Session["user"] as User;
            return (user == null || (user.Role != RoleEnum.ADMIN && user.Role != RoleEnum.REDITEL));
        }

        

    }
}