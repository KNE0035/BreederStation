using BreederStationAspView.Models;
using BreederStationBussinessLayer.Domain;
using BreederStationBussinessLayer.Domain.Enums;
using BreederStationBussinessLayer.SelectCriteria;
using BreederStationBussinessLayer.Service;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BreederStationAspView.Controllers
{
    public class AnimalController : Controller
    {
        AnimalService animalService = ServiceRegister.getInstance().Get<AnimalService>();
        AnimalGroupService animalGroupService = ServiceRegister.getInstance().Get<AnimalGroupService>();
        CageService cageService = ServiceRegister.getInstance().Get<CageService>();

        public ActionResult AnimalSearch()
        {
            AnimalCriteria criteria = null;
            if (!isUserAdminDirectorOrBreeder())
            {
                return RedirectToAction("Index", "Home");
            }

            if (TempData["SearchedAnimals"] != null)
            {
                ViewBag.SearchedAnimals = TempData["SearchedAnimals"];
            }

            if (TempData["LastCriteria"] != null)
            {
                criteria = (AnimalCriteria)TempData["LastCriteria"];
            }

            IList<AnimalGroup> groups = animalGroupService.GetAllAnimalGroups(false);
            groups.Insert(0, null);
            ViewBag.possibleGroups = groups;

            IList<Cage> cages = cageService.GetAllCages();
            cages.Insert(0, null);
            ViewBag.possibleCages = cages;
            return View(criteria);
        }

        public ActionResult SearchAnimals(AnimalCriteria criteria)
        {
            if (!isUserAdminDirectorOrBreeder())
            {
                return RedirectToAction("Index", "Home");
            }

            TempData["SearchedAnimals"] = animalService.GetAnimals(criteria);
            TempData["LastCriteria"] = criteria;


            return RedirectToAction("AnimalSearch", "Animal");
        }

        public ActionResult Delete(int id)
        {
            if (!isUserAdminDirector())
            {
                return RedirectToAction("Index", "Home");
            }

            animalService.RemoveAnimal(id);
            return RedirectToAction("SearchAnimals", "Animal");
        }


        private bool isUserAdminDirectorOrBreeder()
        {
            User user = Session["user"] as User;
            return (user != null && (user.Role == RoleEnum.ADMIN || user.Role == RoleEnum.REDITEL || user.Role == RoleEnum.CHOVATEL));
        }

        private bool isUserAdminDirector()
        {
            User user = Session["user"] as User;
            return (user != null && (user.Role == RoleEnum.ADMIN || user.Role == RoleEnum.REDITEL));
        }
    }
}