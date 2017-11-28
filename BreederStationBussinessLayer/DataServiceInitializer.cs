using BreederStationBussinessLayer.Service;
using BreederStationBussinessLayer.Service.Impl;
using BreederStationDAOLayer.Database;
using BreederStationDataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreederStationBussinessLayer
{
    public class DataServiceInitializer
    {
        public static void initializeDataAndServices()
        {
            DatabaseTypeEnum databaseType = DatabaseTypeEnum.ORACLE_DATABASE;
            DatabaseTypeInitializer.InitializeDatabaseType(databaseType);
            ServiceRegister serviceRegister = ServiceRegister.getInstance();
            serviceRegister.Register(typeof(PersonService), new PersonServiceImpl());
            serviceRegister.Register(typeof(AnimalGroupService), new AnimalGroupServiceImpl());
            serviceRegister.Register(typeof(AnimalService), new AnimalServiceImpl());
            serviceRegister.Register(typeof(CageService), new CageServiceImpl());
            serviceRegister.Register(typeof(FoodService), new FoodServiceImpl());
            serviceRegister.Register(typeof(EventService), new EventServiceImpl());
        }

    }
}
