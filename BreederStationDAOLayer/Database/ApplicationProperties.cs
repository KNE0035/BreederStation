using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreederStationDAOLayer.Database
{
    class ApplicationProperties
    {
        public static string OracleConnString = @"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=srvfeia01.msad.vsb.cz)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=oracle.dbsys.cs.vsb.cz)));User Id=kne0035;Password=fgwwpUyhji;Connection Timeout=45;";
        public static string SqlServerConnString = @"Server = dbsys.cs.vsb.cz\STUDENT; Database = kne0035; User Id = kne0035; Password = AnGzFMLkN4;";
        public static string defaultAddressXmlWholeName = @"..\..\..\Address.xml";
    }
}
