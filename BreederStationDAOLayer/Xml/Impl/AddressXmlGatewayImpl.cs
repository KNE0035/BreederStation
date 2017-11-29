using System.Collections.Generic;
using BreederStationDataLayer.Orm.Dto;
using System.Xml.Serialization;
using BreederStationDataLayer.Orm.Dao;
using BreederStationDAOLayer.Database;
using System.IO;

namespace BreederStationDataLayer.Xml.Impl
{
    public class AddressXmlGatewayImpl : AddressXmlGateway
    {

        private AddressGateway addressGateway = RepositoryRegister.getInstance().Get<AddressGateway>();
        private string defaultAddressXmlName;

        public AddressXmlGatewayImpl()
        {
            defaultAddressXmlName = ApplicationProperties.defaultAddressXmlWholeName;
        }

        public void exportAddresses(string xmlFile = null)
        {
            IList<Address> addresses = addressGateway.Select();

            XmlSerializer serializer = new XmlSerializer(typeof(List<Address>));
            using (TextWriter writer = new StreamWriter(xmlFile == null ? defaultAddressXmlName : xmlFile))
            {
                serializer.Serialize(writer, addresses);
            }
        }

        public IList<Address> getAddressesFromXml(string xmlFile = null)
        {
            IList<Address> addresses = new List<Address>();

            XmlSerializer serializer = new XmlSerializer(typeof(List<Address>));
            using (StreamReader reader = new StreamReader(xmlFile == null ? defaultAddressXmlName : xmlFile))
            {
                addresses = (IList<Address>)serializer.Deserialize(reader);
            }
            return addresses;
        }
    }
}
