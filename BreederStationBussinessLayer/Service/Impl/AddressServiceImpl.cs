using System;
using System.Collections.Generic;
using BreederStationBussinessLayer.Domain;
using BreederStationDataLayer.Orm.Dao;
using BreederStationDAOLayer.Database;
using BreederStationDataLayer.Xml;

namespace BreederStationBussinessLayer.Service.Impl
{

    public class AddressServiceImpl : AddressService
    {
        private AddressGateway addressGateway = RepositoryRegister.getInstance().Get<AddressGateway>();
        private AddressXmlGateway addressXmlGateway = RepositoryRegister.getInstance().Get<AddressXmlGateway>();

        public bool AddAddress(Address address)
        {
            return addressGateway.Insert(mapDomainToDtoObject(address)) > 0;
        }

        public bool RemoveAddress(int id)
        {
            return addressGateway.Delete(id) > 0;
        }

        public bool UpdateAddress(Domain.Address address)
        {
            return addressGateway.Update(mapDomainToDtoObject(address)) > 0;
        }

        public BreederStationDataLayer.Orm.Dto.Address mapDomainToDtoObject(Address address)
        {
            BreederStationDataLayer.Orm.Dto.Address dtoAddress = new BreederStationDataLayer.Orm.Dto.Address
            {
                City = address.City,
                Id = address.Id,
                Street = address.Street,
                Zipcode = address.Zipcode
            };
            return dtoAddress;
        }

        public Domain.Address mapDtoToDomainObject(BreederStationDataLayer.Orm.Dto.Address dtoAddress)
        {
            Address address = new Address
            {
                City = dtoAddress.City,
                Id = dtoAddress.Id,
                Street = dtoAddress.Street,
                Zipcode = dtoAddress.Zipcode
            };
            return address;
        }

        public void ImportAddressesToDbFromXml(string xmlFile)
        {
            IList<BreederStationDataLayer.Orm.Dto.Address> addressList = addressXmlGateway.getAddressesFromXml(xmlFile);
            foreach(BreederStationDataLayer.Orm.Dto.Address dtoAddress in addressList)
            {
                addressGateway.Insert(dtoAddress);
            }
        }

        public void ExportAddressesFromDbToXml(string xmlFile)
        {
            addressXmlGateway.exportAddresses(xmlFile);
        }
    }
}
