using BreederStationBussinessLayer.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreederStationDesktopView
{
    public class UserSession
    {
        private static UserSession instance;

        private UserSession()
        {

        }

        public int Id { get; set; }
        public string Login { get; set; }
        public Role Role { get; set; }

        public static UserSession getInstance()
        {
            if(instance == null)
            {
                instance = new UserSession();
            }
            return instance;
        }
    }
}
