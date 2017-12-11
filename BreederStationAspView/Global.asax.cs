using BreederStationBussinessLayer;
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
