using BreederStationAspView.Models;
using BreederStationBussinessLayer.Domain.Enums;
using BreederStationBussinessLayer.Service;
using BreederStationBussinessLayer.ValidationObjects;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
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
            if (!isUserAdminOrDirector())
            {
                return RedirectToAction("Index", "Home");
            }

            IList<Person> aspModelPersons = new List<Person>();
            foreach(BreederStationBussinessLayer.Domain.Person domainPerson in personService.GetAllUsers())
            {
                aspModelPersons.Add(mapDomainToAspModelObject(domainPerson));
            }

            ViewBag.Funds = aspModelPersons;

            return View();
        }

        public ActionResult Delete(int? id)
        {
            int ids = (int)id;
            if (!isUserAdminOrDirector())
            {
                return RedirectToAction("Index", "Home");
            }

            personService.RemovePerson(ids);

            return RedirectToAction("PersonList", "Person");
        }

        public ActionResult AddUpdatePersonForm(string personLogin)
        {
            if (!isUserAdminOrDirector())
            {
                return RedirectToAction("Index", "Home");
            }

            if (TempData["Messages"] != null)
            {
                ViewBag.Messages = TempData["Messages"];
            }

            ViewBag.possibleGroups = animalGroupService.GetAllAnimalGroups(false);
            ViewBag.edit = personLogin == null ? false : true;

            return personLogin == null ? View() : View(mapDomainToAspModelObject(personService.GetUserByLogin(personLogin)));
        }

        public ActionResult AddUpdatePerson(Person person, string oldLogin, bool edit)
        {
            if (!isUserAdminOrDirector())
            {
                return RedirectToAction("Index", "Home");
            }

            IList<string> validatingMessages = validateNewUser(person);

            PersonValidationObject validationObj;
            if (edit)
            {
                validationObj = personService.UpdateUserBaseInfo(mapAspModelToDomainObject(person));
                User sessionedUser = Session["user"] as User;

                if (sessionedUser.Id == person.Id)
                {
                    sessionedUser.Login = person.Login;
                }
            } else
            {
                validationObj = personService.RegisterUser(mapAspModelToDomainObject(person));
            }

            if (validationObj.DirectorDuplicity)
            {
                validatingMessages.Add("Jeden Ředitel již ve stanici je");
            }

            if (validationObj.LoginDuplicity)
            {
                validatingMessages.Add("Duplicitní login");
            }

            if (validatingMessages.Count > 0)
            {
                TempData["Messages"] = validatingMessages;
                return RedirectToAction("AddUpdatePersonForm", "Person", new { personLogin = oldLogin });
            }

            return RedirectToAction("PersonList", "Person");
        }


        private bool isUserAdminOrDirector() {
            User user = Session["user"] as User;
            return (user != null && (user.Role == RoleEnum.ADMIN || user.Role == RoleEnum.REDITEL));
        }

        private IList<string> validateNewUser(Person person)
        {
            IList<string> validatingMesseage = new List<string>();

            if (String.IsNullOrEmpty(person.Login) || String.IsNullOrEmpty(person.FirstName) || String.IsNullOrEmpty(person.LastName))
            {
                validatingMesseage.Add("Login, jméno nebo příjmení jsou prázdné");
            }

            if (String.IsNullOrEmpty(person.Phone) || !Regex.Match(person.Phone, @"^(\d{3} \d{3} \d{3} \d{3})$").Success)
            {
                validatingMesseage.Add("Telefon je ve špatném tvaru, správný tvar (ČČČ ČČČ ČČČ ČČČ)");
            }
            return validatingMesseage;
        }

        private BreederStationBussinessLayer.Domain.Person mapAspModelToDomainObject(Person person)
        {
            BreederStationBussinessLayer.Domain.Person domainPerson =  new BreederStationBussinessLayer.Domain.Person();

            if (person.Role.Type == RoleEnum.CHOVATEL)
            {
                domainPerson = new BreederStationBussinessLayer.Domain.Breeder();
                ((BreederStationBussinessLayer.Domain.Breeder)domainPerson).AnimalGroup = new BreederStationBussinessLayer.Domain.AnimalGroup(){ Id = person.Breeder.AnimalGroup.Id};
            }

            if (person.Role.Type == RoleEnum.UKLIZEC)
            {
                domainPerson = new BreederStationBussinessLayer.Domain.Cleaner();
                ((BreederStationBussinessLayer.Domain.Cleaner)domainPerson).ChemicalQualification = person.Cleaner.ChemicalQualification;
                ((BreederStationBussinessLayer.Domain.Cleaner)domainPerson).Cages = person.Cleaner.Cages;
            }

            domainPerson.Id = person.Id;
            domainPerson.Password = person.Password;
            domainPerson.Login = person.Login;
            domainPerson.LastName = person.LastName;
            domainPerson.FirstName = person.FirstName;
            domainPerson.BirthDate = person.BirthDate;
            domainPerson.LastActiveDate = person.LastActiveDate;
            domainPerson.Phone = person.Phone;
            domainPerson.Role = person.Role;

            return domainPerson;
        }

        private Person mapDomainToAspModelObject(BreederStationBussinessLayer.Domain.Person person)
        {
            Person aspModelPerson = new Person();

            aspModelPerson.Id = person.Id;
            aspModelPerson.Password = person.Password;
            aspModelPerson.Login = person.Login;
            aspModelPerson.LastName = person.LastName;
            aspModelPerson.FirstName = person.FirstName;
            aspModelPerson.BirthDate = person.BirthDate;
            aspModelPerson.LastActiveDate = person.LastActiveDate;
            aspModelPerson.Phone = person.Phone;
            aspModelPerson.Role = person.Role;

            if (person is BreederStationBussinessLayer.Domain.Breeder)
            {
                Breeder breeder = new Breeder();
                breeder.AnimalGroup = new BreederStationBussinessLayer.Domain.AnimalGroup
                {
                    Id = ((BreederStationBussinessLayer.Domain.Breeder)person).AnimalGroup.Id,
                    Description = ((BreederStationBussinessLayer.Domain.Breeder)person).AnimalGroup.Description,
                };
                aspModelPerson.Breeder = breeder;
            }

            if (person is BreederStationBussinessLayer.Domain.Cleaner)
            {
                Cleaner cleaner = new Cleaner()
                {
                    ChemicalQualification = ((BreederStationBussinessLayer.Domain.Cleaner)person).ChemicalQualification,
                    Cages = ((BreederStationBussinessLayer.Domain.Cleaner)person).Cages
                };
                aspModelPerson.Cleaner = cleaner;
            }

            return aspModelPerson;
        }



    }
}