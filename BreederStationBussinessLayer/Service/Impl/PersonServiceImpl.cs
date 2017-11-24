using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BreederStationBussinessLayer.Domain;
using BreederStationDataLayer.Orm.Dao;
using BreederStationDAOLayer.Database;
using BreederStationDataLayer.Orm.SelectCriteria;
using BreederStationBussinessLayer.Domain.Enums;

namespace BreederStationBussinessLayer.Service.Impl
{
    public class PersonServiceImpl : PersonService
    {
        PersonGateway personGateway = RepositoryRegister.getInstance().Get<PersonGateway>();

        public Person Authorize(string login, string password)
        {
            if (String.IsNullOrEmpty(login) || String.IsNullOrEmpty(password))
            {
                return null;
            }

            BreederStationDataLayer.Orm.Dto.Person dtoPerson = personGateway.Select(login);

            if (dtoPerson == null || !password.Equals(dtoPerson.Password))
            {
                return null;
            }

            Person person = new Person
            {
                Id = dtoPerson.Id,
                Login = dtoPerson.Login,
                Role = new Role() { Id = dtoPerson.Role.Id, Description = dtoPerson.Role.Description, Type = (RoleEnum)dtoPerson.Role.Id }
            };

            return person;
        }

        public IList<Person> GetAllUsers()
        {
            IList<BreederStationDataLayer.Orm.Dto.Person> dtoPersons = personGateway.Select(new PersonCriteria());
            IList<Person> persons = new List<Person>();

            foreach(BreederStationDataLayer.Orm.Dto.Person dtoPerson in dtoPersons)
            {
                Person person = new Person
                {
                    Active = dtoPerson.Active,
                    BirthDate = dtoPerson.BirthDate,
                    FirstName = dtoPerson.FirstName,
                    Id = dtoPerson.Id,
                    LastActiveDate = dtoPerson.LastActiveDate,
                    LastName = dtoPerson.LastName,
                    Login = dtoPerson.Login,
                    Phone = dtoPerson.Phone,
                    Role = new Role() { Id = dtoPerson.Role.Id, Description = dtoPerson.Role.Description, Type = (RoleEnum)dtoPerson.Role.Id }
                };
                persons.Add(person);
            }
            return persons;
        }

        public bool RegisterUser(Person user)
        {
            throw new NotImplementedException();
        }

        public bool RemovePerson(int id)
        {
            return personGateway.Delete(id) > 0 ? true : false;
        }

        public Person UpdateUser(Person user)
        {
            throw new NotImplementedException();
        }
    }
}
