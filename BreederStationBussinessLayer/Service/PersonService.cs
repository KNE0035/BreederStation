using BreederStationBussinessLayer.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreederStationBussinessLayer.Service.Impl
{
    public interface PersonService
    {
        Person Authorize(string login, string password);
        bool RegisterUser(Person user);
        IList<Person> GetAllUsers();
        bool RemovePerson(int id);
        Person UpdateUser(Person user);

    }
}
