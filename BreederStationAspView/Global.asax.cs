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
            DataServiceInitializer.initializeDataAndServices();
        }
    }
}
