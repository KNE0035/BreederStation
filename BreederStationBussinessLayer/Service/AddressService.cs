using BreederStationBussinessLayer.Domain;
using System.Collections.Generic;

namespace BreederStationBussinessLayer.Service
{
    public interface AddressService
    {
        bool AddAddress(Address address);
        void ImportAddressesToDbFromXml(string xmlFile);
        void ExportAddressesFromDbToXml(string xmlFile);
        bool RemoveAddress(int id);
        bool UpdateAddress(Address address);

        BreederStationDataLayer.Orm.Dto.Address mapDomainToDtoObject(Address address);
        Address mapDtoToDomainObject(BreederStationDataLayer.Orm.Dto.Address dtoAddress);
    }
}
