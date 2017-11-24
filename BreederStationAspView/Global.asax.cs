using BreederStationBussinessLayer;
using BreederStationBussinessLayer.Service;
using BreederStationBussinessLayer.Service.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BreederStationAspView
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            DataInitializer.initializeData();
            registerServices();

        }

        private void registerServices()
        {
            ServiceRegister serviceRegister = ServiceRegister.getInstance();
            serviceRegister.Register(typeof(PersonService), new PersonServiceImpl());
            serviceRegister.Register(typeof(AnimalGroupService), new AnimalGroupServiceImpl());
        }
    }
}
