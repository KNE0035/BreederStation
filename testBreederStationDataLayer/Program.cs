using BreederStation.Orm.Dao;
using BreederStationDAOLayer.Database;
using BreederStationDataLayer;
using BreederStationDataLayer.Database;
using BreederStationDataLayer.Orm.Dao;
using BreederStationDataLayer.Orm.Dto;
using BreederStationDataLayer.Orm.OracleGateway.Dao;
using BreederStationDataLayer.Orm.SelectCriteria;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testBreederStationDataLayer
{
    class Program
    {
        static void Main(string[] args)
        {
            RepositoryRegister register = RepositoryRegister.getInstance();
            DatabaseService.init(new OracleConnection());
            //register.Register(typeof(DatabaseService), DatabaseService.getInstance());
            register.Register(typeof(PersonGateway), new OraclePersonGateway(DatabaseService.getInstance()));
            register.Register(typeof(AddressGateway), new OracleAddressGateway(DatabaseService.getInstance()));



            tesPersonTable();
            //testAdressTable();

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
                Login = "test61",
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


            Console.WriteLine("Vypis zamestnancu");
            Console.WriteLine("-------------------------------------------------------------------------");
            foreach (Person item in persons)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("-------------------------------------------------------------------------");

            Person selected_person = personTable.Select("test61");
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
            personTable.InsertResponsibilityForCage(selected_person.Id, 41);

            Console.WriteLine("-------------------------------------------------------------------------");
            Console.WriteLine("testing deleting responsibility...");
            Console.WriteLine("-------------------------------------------------------------------------");
            personTable.DeleteResponsibilityForCage(selected_person.Id, 41);

            Console.WriteLine();
            Console.WriteLine("---------------------------------------------------------------------------------------");
            Console.WriteLine("End of PersonTable test");
            Console.WriteLine("---------------------------------------------------------------------------------------");
        }

        private static void testAdressTable()
        {
            AddressGateway addressGateway = RepositoryRegister.getInstance().Get<AddressGateway>();

            IList<Address> addresses = new List<Address>();

            addresses = addressGateway.Select();
            Console.WriteLine("---------------------------------------------------------------------------------------");
            Console.WriteLine("addressGateway test");
            Console.WriteLine("---------------------------------------------------------------------------------------");
            Console.WriteLine();

            Address address = new Address
            {
                City = "ostravaasdsadfasf",
                Street = "Krestopva_update",
                Zipcode = "70030"
            };


            Console.WriteLine("-------------------------------------------------------------------------");
            Console.WriteLine("testing createing address...");
            Console.WriteLine("-------------------------------------------------------------------------");
            addressGateway.Insert(address);


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
            addressGateway.Delete(15);


            Console.WriteLine();
            Console.WriteLine("---------------------------------------------------------------------------------------");
            Console.WriteLine("End of addressTable test");
            Console.WriteLine("---------------------------------------------------------------------------------------");
        }
    }
}
