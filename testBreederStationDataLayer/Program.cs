using BreederStationDAOLayer.Database;
using BreederStationDataLayer;
using BreederStationDataLayer.Orm.Dao;
using BreederStationDataLayer.Orm.Dto;
using BreederStationDataLayer.Orm.SelectCriteria;
using DAIS_KNE0035.Orm.SelectCriteria;
using System;
using System.Collections.Generic;

namespace testBreederStationDataLayer
{
    class Program
    {
        static void Main(string[] args)
        {
            DatabaseTypeInitializer.InitializeDatabaseType(DatabaseTypeEnum.ORACLE_DATABASE);

            tesPersonTable();
            testAdressTable();
            testCageTable();
            testAnimalTable();
            testAnimalGroupTable();
            testCompanyTable();
            testEventTable();
            testFoodOrderPending();
            testFoodTable();
            testRoleTable();
            Console.Read();

            Console.WriteLine(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
            /*testRoleTable();
            Console.WriteLine(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
            testCageTable();
            Console.WriteLine(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
            testAnimalTable();
            Console.WriteLine(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
            testFoodOrderPending();*/
        }

        private static void tesPersonTable()
        {
            var personTable = RepositoryRegister.getInstance().Get<PersonGateway>();

            IList<Person> persons = new List<Person>();

            persons = personTable.Select(new PersonCriteria());
            Console.WriteLine("---------------------------------------------------------------------------------------");
            Console.WriteLine("PesonTable test");
            Console.WriteLine("---------------------------------------------------------------------------------------");
            Console.WriteLine();

            Cleaner cleaner = new Cleaner
            {
                Login = "test4",
                Password = "sas",
                FirstName = "Marek_testp",
                LastName = "Kneys_",
                Phone = "420 456 987 842",
                BirthDate = new DateTime(1994, 06, 30),
                Role = new Role { Type = RoleEnum.UKLIZEC },
                ChemicalQualification = true
            };


            Console.WriteLine("-------------------------------------------------------------------------");
            Console.WriteLine("testing createing person...");
            Console.WriteLine("-------------------------------------------------------------------------");
            personTable.Insert(cleaner);


            Console.WriteLine("Vypis zamestnancu");
            Console.WriteLine("-------------------------------------------------------------------------");
            foreach (Person item in persons)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("-------------------------------------------------------------------------");

            Person selected_person = personTable.Select("test4");
            Console.WriteLine("Vypis zamestnance s loginem mkneys");
            Console.WriteLine("-------------------------------------------------------------------------");
            Console.WriteLine(selected_person);
            Console.WriteLine("-------------------------------------------------------------------------");

            Console.WriteLine("-------------------------------------------------------------------------");
            Console.WriteLine("testing deleting...");
            Console.WriteLine("-------------------------------------------------------------------------");
            personTable.Delete(cleaner.Login);


            Console.WriteLine("-------------------------------------------------------------------------");
            Console.WriteLine("testing updating...");
            Console.WriteLine("-------------------------------------------------------------------------");
            cleaner.FirstName = "update_test";
            personTable.Update(cleaner);


            Console.WriteLine("-------------------------------------------------------------------------");
            Console.WriteLine("testing inserting responsibility...");
            Console.WriteLine("-------------------------------------------------------------------------");
            personTable.InsertResponsibilityForCage(selected_person.Id, 1);

            Console.WriteLine("-------------------------------------------------------------------------");
            Console.WriteLine("testing deleting responsibility...");
            Console.WriteLine("-------------------------------------------------------------------------");
            personTable.DeleteResponsibilityForCage(selected_person.Id, 1);

            Console.WriteLine();
            Console.WriteLine("---------------------------------------------------------------------------------------");
            Console.WriteLine("End of PersonTable test");
            Console.WriteLine("---------------------------------------------------------------------------------------");
        }

        private static void testAdressTable()
        {
            AddressGateway addressGateway = RepositoryRegister.getInstance().Get<AddressGateway>();

            IList<Address> addresses = new List<Address>();

            Console.WriteLine("---------------------------------------------------------------------------------------");
            Console.WriteLine("addressGateway test");
            Console.WriteLine("---------------------------------------------------------------------------------------");
            Console.WriteLine();

            Address address = new Address
            {
                City = "ostravaasdsadfasdasfasasfdssf",
                Street = "Krestopva_updatesadsa",
                Zipcode = "70040"
            };


            Console.WriteLine("-------------------------------------------------------------------------");
            Console.WriteLine("testing createing address...");
            Console.WriteLine("-------------------------------------------------------------------------");
            addressGateway.Insert(address);
            addresses = addressGateway.Select();

            Console.WriteLine("Vypis address");
            Console.WriteLine("-------------------------------------------------------------------------");
            foreach (Address item in addresses)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("-------------------------------------------------------------------------");

            address.Id = 12;
            address.City = "test_update";
            addressGateway.Update(address);
            Console.WriteLine("-------------------------------------------------------------------------");
            Console.WriteLine("testing deleting...");
            Console.WriteLine("-------------------------------------------------------------------------");
            addressGateway.Delete(addresses[addresses.Count-1].Id);


            Console.WriteLine();
            Console.WriteLine("---------------------------------------------------------------------------------------");
            Console.WriteLine("End of addressTable test");
            Console.WriteLine("---------------------------------------------------------------------------------------");
        }

        private static void testCageTable()
        {
            CageGateway cageTable = RepositoryRegister.getInstance().Get<CageGateway>();

            Console.WriteLine("---------------------------------------------------------------------------------------");
            Console.WriteLine("CageTable test");
            Console.WriteLine("---------------------------------------------------------------------------------------");
            Console.WriteLine();

            Cage cage = new Cage
            {
                LengthM = 500,
                WidthM = 1000
            };

            Console.WriteLine("-------------------------------------------------------------------------");
            Console.WriteLine("testing inserting...");
            Console.WriteLine("-------------------------------------------------------------------------");
            cageTable.Insert(cage);

            IList<Cage> cages = cageTable.Select();
            Console.WriteLine("Vypis kleci");
            Console.WriteLine("-------------------------------------------------------------------------");
            foreach (Cage item in cages)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("-------------------------------------------------------------------------");

            if (cages.Count > 1)
            {
                Console.WriteLine("-------------------------------------------------------------------------");
                Console.WriteLine("testing updating...");
                Console.WriteLine("-------------------------------------------------------------------------");
                cages[cages.Count - 1].LengthM = 2000;
                cages[cages.Count - 1].WidthM = 2000;
                cageTable.Update(cages[cages.Count - 1]);

                Console.WriteLine("-------------------------------------------------------------------------");
                Console.WriteLine("testing deleting...");
                Console.WriteLine("-------------------------------------------------------------------------");
                cageTable.Delete(cages[cages.Count - 1].Id);
            }

            Console.WriteLine();
            Console.WriteLine("---------------------------------------------------------------------------------------");
            Console.WriteLine("End of CageTable test");
            Console.WriteLine("---------------------------------------------------------------------------------------");
        }

        private static void testAnimalTable()
        {
            AnimalGateway animalTable = RepositoryRegister.getInstance().Get<AnimalGateway>();

            Console.WriteLine("---------------------------------------------------------------------------------------");
            Console.WriteLine("AnimalTable test");
            Console.WriteLine("---------------------------------------------------------------------------------------");
            Console.WriteLine();

            Animal animal = new Animal
            {
                Name = "test_animal",
                Active = true,
                Race = "test race",
                Sex = SexEnum.samec,
                BirthDate = new DateTime(2013, 06, 30),
                AnimalGroup = new AnimalGroup { Id = 1 },
                Cage = new Cage { Id = 1 },
                Food = new Food { Id = 1 }
            };

            Console.WriteLine("-------------------------------------------------------------------------");
            Console.WriteLine("testing inserting...");
            Console.WriteLine("-------------------------------------------------------------------------");
            animalTable.Insert(animal);

            IList<Animal> animals = animalTable.Select(new AnimalCriteria());
            Console.WriteLine("Vypis zvirat");
            Console.WriteLine("-------------------------------------------------------------------------");
            foreach (Animal item in animals)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("-------------------------------------------------------------------------");

            Animal selected_animal = animalTable.Select(animals[animals.Count - 1].Id);
            Console.WriteLine("Vypis zvirete s id " + animals[animals.Count - 1].Id);
            Console.WriteLine("-------------------------------------------------------------------------");
            Console.WriteLine(selected_animal);
            Console.WriteLine("-------------------------------------------------------------------------");

            if (animals.Count > 1)
            {
                Console.WriteLine("-------------------------------------------------------------------------");
                Console.WriteLine("testing updating...");
                Console.WriteLine("-------------------------------------------------------------------------");

                animals[animals.Count - 1].Name = "test_update";
                animalTable.Update(animals[animals.Count - 1]);

                Console.WriteLine("-------------------------------------------------------------------------");
                Console.WriteLine("testing deleting...");
                Console.WriteLine("-------------------------------------------------------------------------");
                animalTable.Delete(animals[animals.Count - 1].Id);
            }

            Console.WriteLine();
            Console.WriteLine("---------------------------------------------------------------------------------------");
            Console.WriteLine("End of AnimalTable test");
            Console.WriteLine("---------------------------------------------------------------------------------------");
        }

        private static void testAnimalGroupTable()
        {
            AnimalGroupGateway animalGroupTable = RepositoryRegister.getInstance().Get<AnimalGroupGateway>();

            Console.WriteLine("---------------------------------------------------------------------------------------");
            Console.WriteLine("animalGroupTable test");
            Console.WriteLine("---------------------------------------------------------------------------------------");
            Console.WriteLine();

            AnimalGroup animalGroup = new AnimalGroup
            {
                Description = "test_grupy"
            };

            Console.WriteLine("-------------------------------------------------------------------------");
            Console.WriteLine("testing inserting...");
            Console.WriteLine("-------------------------------------------------------------------------");
            animalGroupTable.Insert(animalGroup);

            IList<AnimalGroup> groups = animalGroupTable.Select(false);
            Console.WriteLine("Vypis grup jednoduchy");
            Console.WriteLine("-------------------------------------------------------------------------");
            foreach (AnimalGroup item in groups)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("-------------------------------------------------------------------------");

            groups = animalGroupTable.Select(true);
            Console.WriteLine("Vypis grup slozity");
            Console.WriteLine("-------------------------------------------------------------------------");
            foreach (AnimalGroup item in groups)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("-------------------------------------------------------------------------");

            AnimalGroup animalGroupSelected = animalGroupTable.Select(1);
            Console.WriteLine("Vypis grupy s id " + 1);
            Console.WriteLine("-------------------------------------------------------------------------");
            Console.WriteLine(animalGroupSelected);
            Console.WriteLine("-------------------------------------------------------------------------");

            if (groups.Count > 1)
            {
                Console.WriteLine("-------------------------------------------------------------------------");
                Console.WriteLine("testing updating...");
                Console.WriteLine("-------------------------------------------------------------------------");
                groups[groups.Count - 1].Description = "test_update";
                animalGroupTable.Update(groups[groups.Count - 1]);

                Console.WriteLine("-------------------------------------------------------------------------");
                Console.WriteLine("testing deleting...");
                Console.WriteLine("-------------------------------------------------------------------------");
                animalGroupTable.Delete(groups[groups.Count - 1].Id);
            }

            Console.WriteLine();
            Console.WriteLine("---------------------------------------------------------------------------------------");
            Console.WriteLine("End of AnimalTable test");
            Console.WriteLine("---------------------------------------------------------------------------------------");
        }

        private static void testCompanyTable()
        {
            CompanyGateway companyTable = RepositoryRegister.getInstance().Get<CompanyGateway>();

            Console.WriteLine("---------------------------------------------------------------------------------------");
            Console.WriteLine("Company test");
            Console.WriteLine("---------------------------------------------------------------------------------------");
            Console.WriteLine();

            Company company = new Company
            {
                Address = new Address { Id = 1 },
                Email = "mkneys@vsb.cz",
                Phone = "454 545 122 454",
                Trademark = "test_trademark_2"
            };

            Console.WriteLine("-------------------------------------------------------------------------");
            Console.WriteLine("testing inserting...");
            Console.WriteLine("-------------------------------------------------------------------------");
            companyTable.Insert(company);

            IList<Company> companies = companyTable.Select(new CompanyCriteria());
            Console.WriteLine("Vypis companies");
            Console.WriteLine("-------------------------------------------------------------------------");
            foreach (Company item in companies)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("-------------------------------------------------------------------------");

            Company companySelected = companyTable.Select("Nutram");
            Console.WriteLine("Vypis grupy s trademarkem nutram");
            Console.WriteLine("-------------------------------------------------------------------------");
            Console.WriteLine(companySelected);
            Console.WriteLine("-------------------------------------------------------------------------");

            if (companies.Count > 1)
            {
                Console.WriteLine("-------------------------------------------------------------------------");
                Console.WriteLine("testing updating...");
                Console.WriteLine("-------------------------------------------------------------------------");
                companies[companies.Count - 1].Email = "mkneys@vsb.test";
                companyTable.Update(companies[companies.Count - 1]);

                Console.WriteLine("-------------------------------------------------------------------------");
                Console.WriteLine("testing deleting...");
                Console.WriteLine("-------------------------------------------------------------------------");
                companyTable.Delete(companies[companies.Count - 1].Trademark);
            }

            Console.WriteLine();
            Console.WriteLine("---------------------------------------------------------------------------------------");
            Console.WriteLine("End of AnimalTable test");
            Console.WriteLine("---------------------------------------------------------------------------------------");
        }

        private static void testEventTable()
        {
            EventGateway eventTable = RepositoryRegister.getInstance().Get<EventGateway>();

            Console.WriteLine("---------------------------------------------------------------------------------------");
            Console.WriteLine("eventTable test");
            Console.WriteLine("---------------------------------------------------------------------------------------");
            Console.WriteLine();

            IList<int> animalIds = new List<int>();
            animalIds.Add(1);
            animalIds.Add(2);
            animalIds.Add(3);

            Event animalEvent = new Event
            {
                Description = "test_event",
                Breeder = new Breeder { Id = 5 },
                EndDate = DateTime.Now,
                StartDate = DateTime.Now,
                AnimalIds = animalIds
            };

            Console.WriteLine("-------------------------------------------------------------------------");
            Console.WriteLine("testing inserting...");
            Console.WriteLine("-------------------------------------------------------------------------");
            eventTable.Insert(animalEvent);

            IList<Event> events = eventTable.Select();
            Console.WriteLine("Vypis eventu");
            Console.WriteLine("-------------------------------------------------------------------------");
            foreach (Event item in events)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("-------------------------------------------------------------------------");

            Event eventSelected = eventTable.Select(events[events.Count - 1].Id);
            Console.WriteLine("Vypis eventu s id " + events[events.Count - 1].Id);
            Console.WriteLine("-------------------------------------------------------------------------");
            Console.WriteLine(eventSelected);
            Console.WriteLine("-------------------------------------------------------------------------");

            if (events.Count > 0)
            {
                Console.WriteLine("-------------------------------------------------------------------------");
                Console.WriteLine("testing updating...");
                Console.WriteLine("-------------------------------------------------------------------------");
                events[events.Count - 1].Description = "test_update";
                eventTable.Update(events[events.Count - 1]);

                Console.WriteLine("-------------------------------------------------------------------------");
                Console.WriteLine("testing deleting...");
                Console.WriteLine("-------------------------------------------------------------------------");
                eventTable.Delete(events[events.Count - 1].Id);
            }

            Console.WriteLine();
            Console.WriteLine("---------------------------------------------------------------------------------------");
            Console.WriteLine("End of AnimalTable test");
            Console.WriteLine("---------------------------------------------------------------------------------------");
        }

        private static void testFoodOrderPending()
        {
            FoodOrderPendingGateway foodOrderPendingTable = RepositoryRegister.getInstance().Get<FoodOrderPendingGateway>(); ;

            Console.WriteLine("---------------------------------------------------------------------------------------");
            Console.WriteLine("FoodOrderPendingTable test");
            Console.WriteLine("---------------------------------------------------------------------------------------");
            Console.WriteLine();

            IList<FoodOrderPending> foodOrderPendingsResolved = foodOrderPendingTable.Select(true);
            Console.WriteLine("Vypis cekajicich objednavek na jidlo ktere byly vyrizeny");
            Console.WriteLine("-------------------------------------------------------------------------");
            foreach (FoodOrderPending item in foodOrderPendingsResolved)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("-------------------------------------------------------------------------");

            Console.WriteLine();

            IList<FoodOrderPending> foodOrderPendingsNotResolved = foodOrderPendingTable.Select(false);
            Console.WriteLine("Vypis cekajicich objednavek na jidlo ktere nebyly vyrizeny");
            Console.WriteLine("-------------------------------------------------------------------------");
            foreach (FoodOrderPending item in foodOrderPendingsNotResolved)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("-------------------------------------------------------------------------");

            if (foodOrderPendingsNotResolved.Count > 0)
            {
                Console.WriteLine("-------------------------------------------------------------------------");
                Console.WriteLine("testing updating...");
                Console.WriteLine("-------------------------------------------------------------------------");

                foodOrderPendingsNotResolved[0].ResolvedDate = DateTime.Now;
                foodOrderPendingTable.Update(foodOrderPendingsNotResolved[0]);
            }


            FoodOrderPending fop = new FoodOrderPending
            {
                Food = new Food { Id = 1 },
                Priority = FoodOrderPriorityEnum.medium,
                ResolvedDate = DateTime.Now,
                StartDate = DateTime.Now
            };
            Console.WriteLine("-------------------------------------------------------------------------");
            Console.WriteLine("testing creating...");
            Console.WriteLine("-------------------------------------------------------------------------");

            foodOrderPendingTable.Insert(fop);

            Console.WriteLine();
            Console.WriteLine("---------------------------------------------------------------------------------------");
            Console.WriteLine("End of FoodOrderPendingTable test");
            Console.WriteLine("---------------------------------------------------------------------------------------");
        }

        private static void testFoodTable()
        {
            FoodGateway foodTable = RepositoryRegister.getInstance().Get<FoodGateway>();

            Console.WriteLine("---------------------------------------------------------------------------------------");
            Console.WriteLine("foodTable test");
            Console.WriteLine("---------------------------------------------------------------------------------------");
            Console.WriteLine();

            Food food = new Food
            {
                Carbohydrates = 50,
                Company = new Company { Id = 1},
                Fat = 20,
                Proteins = 30,
                Name = "test_insert_food",
                Price = 456.45
            };

            Console.WriteLine("-------------------------------------------------------------------------");
            Console.WriteLine("testing inserting...");
            Console.WriteLine("-------------------------------------------------------------------------");
            foodTable.Insert(food);

            IList<Food> foods = foodTable.Select(new FoodCriteria());
            Console.WriteLine("Vypis jidla");
            Console.WriteLine("-------------------------------------------------------------------------");
            foreach (Food item in foods)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("-------------------------------------------------------------------------");

            Food foodSelected = foodTable.Select(foods[foods.Count - 1].Id);
            Console.WriteLine("Vypis eventu s id " + foods[foods.Count - 1].Id);
            Console.WriteLine("-------------------------------------------------------------------------");
            Console.WriteLine(foodSelected);
            Console.WriteLine("-------------------------------------------------------------------------");

            if (foods.Count > 0)
            {
                Console.WriteLine("-------------------------------------------------------------------------");
                Console.WriteLine("testing updating...");
                Console.WriteLine("-------------------------------------------------------------------------");
                foods[foods.Count - 1].Name = "test_update_food";
                foodTable.Update(foods[foods.Count - 1]);

                Console.WriteLine("-------------------------------------------------------------------------");
                Console.WriteLine("testing deleting...");
                Console.WriteLine("-------------------------------------------------------------------------");
                foodTable.Delete(foods[foods.Count - 1].Id);
            }

            Console.WriteLine();
            Console.WriteLine("---------------------------------------------------------------------------------------");
            Console.WriteLine("End of AnimalTable test");
            Console.WriteLine("---------------------------------------------------------------------------------------");
        }

        private static void testRoleTable()
        {
            RoleGateway roleTable = RepositoryRegister.getInstance().Get<RoleGateway>();

            Console.WriteLine("---------------------------------------------------------------------------------------");
            Console.WriteLine("RoleTable test");
            Console.WriteLine("---------------------------------------------------------------------------------------");
            Console.WriteLine();

            Role role = new Role
            {
                Type = RoleEnum.ROLE_INSERT_DELETE_TEST,
                Description = "testing role"
            };

            Console.WriteLine("-------------------------------------------------------------------------");
            Console.WriteLine("testing inserting...");
            Console.WriteLine("-------------------------------------------------------------------------");
            roleTable.Insert(role);

            IList<Role> roles = roleTable.Select();
            Console.WriteLine("Vypis roli");
            Console.WriteLine("-------------------------------------------------------------------------");
            foreach (Role item in roles)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("-------------------------------------------------------------------------");

            Console.WriteLine("-------------------------------------------------------------------------");
            Console.WriteLine("testing updating...");
            Console.WriteLine("-------------------------------------------------------------------------");
            roles[roles.Count - 1].Description = "test role update";
            roleTable.Update(roles[roles.Count - 1]);

            Console.WriteLine("-------------------------------------------------------------------------");
            Console.WriteLine("testing deleting...");
            Console.WriteLine("-------------------------------------------------------------------------");
            roleTable.Delete((int)RoleEnum.ROLE_INSERT_DELETE_TEST);


            Console.WriteLine();
            Console.WriteLine("---------------------------------------------------------------------------------------");
            Console.WriteLine("End of RoleTable test");
            Console.WriteLine("---------------------------------------------------------------------------------------");
        }

    }
}
