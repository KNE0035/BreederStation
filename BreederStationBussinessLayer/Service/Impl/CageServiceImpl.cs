using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BreederStationBussinessLayer.Domain;
using BreederStationDataLayer.Orm.Dao;
using BreederStationDAOLayer.Database;

namespace BreederStationBussinessLayer.Service.Impl
{

    public class CageServiceImpl : CageService
    {

        private CageGateway cageGateway = RepositoryRegister.getInstance().Get<CageGateway>();
        public bool AddCage(Cage cage)
        {
            return cageGateway.Insert(mapDomainToDtoObject(cage)) > 0;
        }

        public IList<Cage> GetAllCages()
        {


            IList<BreederStationDataLayer.Orm.Dto.Cage> dtoCages = cageGateway.Select();
            IList<Cage> cages = new List<Cage>();

            foreach(BreederStationDataLayer.Orm.Dto.Cage dtoCage in dtoCages)
            {
                cages.Add(mapDtoToDomainObject(dtoCage));
            }
            return cages;
        }

        public bool RemoveCage(int id)
        {
            return cageGateway.Delete(id) > 0;
        }

        public bool UpdateCage(Cage cage)
        {
            return cageGateway.Update(mapDomainToDtoObject(cage)) > 0;
        }

        public BreederStationDataLayer.Orm.Dto.Cage mapDomainToDtoObject(Cage cage)
        {
            BreederStationDataLayer.Orm.Dto.Cage dtoCage = new BreederStationDataLayer.Orm.Dto.Cage
            {
                Id = cage.Id,
                LengthM = cage.LengthM,
                WidthM = cage.WidthM,
                Animals = cage.Animals,
                Cleaners = cage.Cleaners
            };
            return dtoCage;
        }

        public Cage mapDtoToDomainObject(BreederStationDataLayer.Orm.Dto.Cage dtoCage)
        {
            Cage cage = new Cage
            {
                Id = dtoCage.Id,
                LengthM = dtoCage.LengthM,
                WidthM = dtoCage.WidthM,
                Animals = dtoCage.Animals,
                Cleaners = dtoCage.Cleaners
            };
            return cage;
        }
    }
}
