using BreederStationDAOLayer.Database;
using BreederStationDataLayer.Database;
using BreederStationDataLayer.Orm.Dao;
using BreederStationDataLayer.Orm.Dto;
using System;
using System.Collections.Generic;

namespace BreederStationDataLayer
{
    class Program
    {
        static void Main(string[] args)
        {
            /*RepositoryRegister register = RepositoryRegister.getInstance();
            DatabaseService.init(new OracleConnection());
            //register.Register(typeof(DatabaseService), DatabaseService.getInstance());
            register.Register(typeof(OraclePersonGateway), new OraclePersonGateway(DatabaseService.getInstance()));





            tesPersonTable();
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
            PersonGateway personTable = RepositoryRegister.getInstance().Get<PersonGateway>();

            IList<Person> persons = new List<Person>();

            /*persons = personTable.Select();
            Console.WriteLine("---------------------------------------------------------------------------------------");
            Console.WriteLine("PesonTable test");
            Console.WriteLine("---------------------------------------------------------------------------------------");
            Console.WriteLine();*/

            Cleaner cleaner = new Cleaner
            {
                Login = "test_s",
                Password = "sa",
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


            /*Console.WriteLine("Vypis zamestnancu");
            Console.WriteLine("-------------------------------------------------------------------------");
            foreach (Person item in persons)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("-------------------------------------------------------------------------");

            Person selected_person = personTable.Select("mkneys");
            Console.WriteLine("Vypis zamestnance s loginem mkneys");
            Console.WriteLine("-------------------------------------------------------------------------");
            Console.WriteLine(selected_person);
            Console.WriteLine("-------------------------------------------------------------------------");

            Console.WriteLine("-------------------------------------------------------------------------");
            Console.WriteLine("testing deleting...");
            Console.WriteLine("-------------------------------------------------------------------------");
            personTable.Delete(person.Login);


            Console.WriteLine("-------------------------------------------------------------------------");
            Console.WriteLine("testing updating...");
            Console.WriteLine("-------------------------------------------------------------------------");
            person.FirstName = "update_test";
            personTable.Update(person);


            Console.WriteLine("-------------------------------------------------------------------------");
            Console.WriteLine("testing inserting responsibility...");
            Console.WriteLine("-------------------------------------------------------------------------");
            personTable.InsertResponsibilityForCage(2, 3);

            Console.WriteLine("-------------------------------------------------------------------------");
            Console.WriteLine("testing deleting responsibility...");
            Console.WriteLine("-------------------------------------------------------------------------");
            personTable.DeleteResponsibilityForCage(2, 3);

            Console.WriteLine();
            Console.WriteLine("---------------------------------------------------------------------------------------");
            Console.WriteLine("End of PersonTable test");
            Console.WriteLine("---------------------------------------------------------------------------------------");*/
        }


        /*private static void testRoleTable()
        {
            RoleTable roleTable = new RoleTable();

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
            role.Description = "test role update";
            roleTable.Update(role);

            Console.WriteLine("-------------------------------------------------------------------------");
            Console.WriteLine("testing deleting...");
            Console.WriteLine("-------------------------------------------------------------------------");
            roleTable.Delete(RoleEnum.ROLE_INSERT_DELETE_TEST.ToString());


            Console.WriteLine();
            Console.WriteLine("---------------------------------------------------------------------------------------");
            Console.WriteLine("End of RoleTable test");
            Console.WriteLine("---------------------------------------------------------------------------------------");
        }

        private static void testCageTable()
        {
            CageTable cageTable = new CageTable();

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

        /*private static void testAnimalTable()
        {
            AnimalTable animalTable = new AnimalTable();

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

            IList<Animal> animals = animalTable.Select();
            Console.WriteLine("Vypis zvirat");
            Console.WriteLine("-------------------------------------------------------------------------");
            foreach (Animal item in animals)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("-------------------------------------------------------------------------");

            Animal selected_animal = animalTable.Select(1);
            Console.WriteLine("Vypis zvirete s id 1");
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

        private static void testFoodOrderPending()
        {
            FoodOrderPendingTable foodOrderPendingTable = new FoodOrderPendingTable();

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

            Console.WriteLine();
            Console.WriteLine("---------------------------------------------------------------------------------------");
            Console.WriteLine("End of FoodOrderPendingTable test");
            Console.WriteLine("---------------------------------------------------------------------------------------");
        }*/
    }
}
