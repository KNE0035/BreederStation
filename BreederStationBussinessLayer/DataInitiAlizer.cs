using BreederStationDAOLayer.Database;
using BreederStationDataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreederStationBussinessLayer
{
    public class DataInitializer
    {
        public static void initializeData()
        {
            DatabaseTypeEnum databaseType = DatabaseTypeEnum.ORACLE_DATABASE;
            DatabaseTypeInitializer.InitializeDatabaseType(databaseType);
        }

    }
}
