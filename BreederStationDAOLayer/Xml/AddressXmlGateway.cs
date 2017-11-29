using BreederStationDataLayer.Orm.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreederStationDataLayer.Xml
{
    public interface AddressXmlGateway
    {
        IList<Address> getAddressesFromXml(string xmlFile);
        void exportAddresses(string xmlFile);
    }
}
