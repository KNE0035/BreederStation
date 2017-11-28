using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BreederStationBussinessLayer.Domain;
using BreederStationDataLayer.Orm.Dao;
using BreederStationDAOLayer.Database;
using BreederStationBussinessLayer.Domain.Enums;
using BreederStationBussinessLayer.ValidationObjects;
using BreederStationBussinessLayer.SelectCriteria;

namespace BreederStationBussinessLayer.Service.Impl
{
    public class PersonServiceImpl : PersonService
    {
        private PersonGateway personGateway = RepositoryRegister.getInstance().Get<PersonGateway>();

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

            Person person = new Person()
            {
                Id = dtoPerson.Id,
                Login = dtoPerson.Login,
                Role = new Role() { Id = dtoPerson.Role.Id, Description = dtoPerson.Role.Description, Type = (RoleEnum)dtoPerson.Role.Id }
            };

            return person;
        }

        public IList<Person> GetAllUsers(PersonCriteria criteria)
        {
            IList<BreederStationDataLayer.Orm.Dto.Person> dtoPersons;
            if (criteria.Role == null)
            {
                dtoPersons = personGateway.Select(new BreederStationDataLayer.Orm.SelectCriteria.PersonCriteria { Role = null, WholeName = criteria.WholeName });
            }
            else
            {
                dtoPersons = personGateway.Select(new BreederStationDataLayer.Orm.SelectCriteria.PersonCriteria { Role = (BreederStationDataLayer.RoleEnum)(criteria.Role.Value), WholeName = criteria.WholeName });
            }
            IList<Person> persons = new List<Person>();

            foreach(BreederStationDataLayer.Orm.Dto.Person dtoPerson in dtoPersons)
            {
                persons.Add(mapDtoToDomainObject(dtoPerson));
            }
            return persons;
        }

        public Person GetUserByLogin(string login)
        {
            BreederStationDataLayer.Orm.Dto.Person dtoPerson = personGateway.Select(login);

            if(dtoPerson == null) {
                return null;
            }

            return mapDtoToDomainObject(dtoPerson);
        }

        public PersonValidationObject RegisterUser(Person user)
        {
            BreederStationDataLayer.Orm.SelectCriteria.PersonCriteria criteria = new BreederStationDataLayer.Orm.SelectCriteria.PersonCriteria() { Role = BreederStationDataLayer.RoleEnum.REDITEL };
            PersonValidationObject validationObj = new PersonValidationObject();
            personGateway.Select(criteria);

            if(user.Role.Type == RoleEnum.REDITEL && personGateway.Select(criteria).Count > 0)
            {
                validationObj.DirectorDuplicity = true;
            }

            user.Active = true;
            user.LastActiveDate = null;
            user.Password = "changeit";

            if (personGateway.Select(user.Login) != null)
            {
                validationObj.LoginDuplicity = true;
            }

            if (!validationObj.LoginDuplicity && !validationObj.DirectorDuplicity)
            {
                personGateway.Insert(mapDomainToDtoObject(user));
            }

            return validationObj;
        }

        public bool RemovePerson(int id)
        {
            return personGateway.Delete(id) > 0 ? true : false;
        }

        public PersonValidationObject UpdateUserBaseInfo(Person user)
        {
            BreederStationDataLayer.Orm.Dto.Person oldUser = personGateway.Select(user.Id);
            PersonValidationObject validationObj = new PersonValidationObject();
            if (!oldUser.Login.Equals(user.Login) && personGateway.Select(user.Login) != null)
            {
                validationObj.LoginDuplicity = true;
            } else
            {
                user.Password = oldUser.Password;
                user.LastActiveDate = oldUser.LastActiveDate;
                user.Role = new Role { Type = (RoleEnum)((int)oldUser.Role.Type) };
                user.Id = oldUser.Id;
                user.Active = oldUser.Active;

                personGateway.Update(mapDomainToDtoObject(user));
            }

            return validationObj;
        }

        public bool UpdateUserPassword(string login, string password)
        {
            BreederStationDataLayer.Orm.Dto.Person user = personGateway.Select(login);
            PersonValidationObject validationObj = new PersonValidationObject();

            user.Password = password;
            return personGateway.Update(user) > 0;
        }

        public BreederStationDataLayer.Orm.Dto.Person mapDomainToDtoObject(Person user)
        {
            BreederStationDataLayer.Orm.Dto.Person dtoPerson = new BreederStationDataLayer.Orm.Dto.Person()
            {
                Active = user.Active,
                Password = user.Password,
                BirthDate = user.BirthDate,
                FirstName = user.FirstName,
                Id = user.Id,
                LastActiveDate = user.LastActiveDate,
                LastName = user.LastName,
                Login = user.Login,
                Phone = user.Phone,
                Role = new BreederStationDataLayer.Orm.Dto.Role { Type = (BreederStationDataLayer.RoleEnum)((int)user.Role.Type) }
            };

            if (user.GetType() == typeof(Cleaner))
            {
                BreederStationDataLayer.Orm.Dto.Cleaner cleaner = new BreederStationDataLayer.Orm.Dto.Cleaner(dtoPerson);
                cleaner.ChemicalQualification = ((Cleaner)user).ChemicalQualification;
                dtoPerson = cleaner;
            }
            else if (user.GetType() == typeof(Breeder))
            {
                BreederStationDataLayer.Orm.Dto.Breeder breeder = new BreederStationDataLayer.Orm.Dto.Breeder(dtoPerson);
                breeder.AnimalGroup = new BreederStationDataLayer.Orm.Dto.AnimalGroup()
                {
                    Id = ((Breeder)user).AnimalGroup.Id
                };
                dtoPerson = breeder;
            }
            return dtoPerson;
        }

        public Person mapDtoToDomainObject(BreederStationDataLayer.Orm.Dto.Person dtoPerson)
        {
            Person person = new Person
            {
                Active = dtoPerson.Active,
                BirthDate = dtoPerson.BirthDate,
                Password = dtoPerson.Password,
                FirstName = dtoPerson.FirstName,
                Id = dtoPerson.Id,
                LastActiveDate = dtoPerson.LastActiveDate,
                LastName = dtoPerson.LastName,
                Login = dtoPerson.Login,
                Phone = dtoPerson.Phone,
                Role = new Role()
                {
                    Id = dtoPerson.Role.Id,
                    Description = dtoPerson.Role.Description,
                    Type = (RoleEnum)dtoPerson.Role.Id
                }
            };

            if (dtoPerson.GetType() == typeof(BreederStationDataLayer.Orm.Dto.Cleaner))
            {
                Cleaner cleaner = new Cleaner(person);
                cleaner.ChemicalQualification = ((BreederStationDataLayer.Orm.Dto.Cleaner)dtoPerson).ChemicalQualification;
                person = cleaner;
            }
            else if (dtoPerson.GetType() == typeof(BreederStationDataLayer.Orm.Dto.Breeder))
            {
                Breeder breeder = new Breeder(person);
                breeder.AnimalGroup = new AnimalGroup()
                {
                    Id = ((BreederStationDataLayer.Orm.Dto.Breeder)dtoPerson).AnimalGroup.Id,
                    Description = ((BreederStationDataLayer.Orm.Dto.Breeder)dtoPerson).AnimalGroup.Description
                };
                person = breeder;
            }
            return person;
        }
    }
}
