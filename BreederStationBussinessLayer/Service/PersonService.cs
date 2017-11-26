using BreederStationBussinessLayer.Domain;
using BreederStationBussinessLayer.ValidationObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreederStationBussinessLayer.Service
{
    public interface PersonService
    {
        Person Authorize(string login, string password);
        PersonValidationObject RegisterUser(Person user);
        IList<Person> GetAllUsers();
        Person GetUserByLogin(string login);
        bool RemovePerson(int id);
        PersonValidationObject UpdateUserBaseInfo(Person user);
        bool UpdateUserPassword(string login, string password);


    }
}
