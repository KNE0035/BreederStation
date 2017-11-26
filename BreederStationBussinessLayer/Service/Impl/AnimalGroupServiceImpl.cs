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
    public class AnimalGroupServiceImpl : AnimalGroupService
    {
        AnimalGroupGateway animalGroupGateway = RepositoryRegister.getInstance().Get<AnimalGroupGateway>();
        public bool AddAnimalGroup(AnimalGroup animalGroup)
        {
            return animalGroupGateway.Insert(mapDomainToDtoObject(animalGroup)) > 0;
        }

        public IList<AnimalGroup> GetAllAnimalGroups(bool allInfo)
        {
            IList<BreederStationDataLayer.Orm.Dto.AnimalGroup> dtoGroups = animalGroupGateway.Select(allInfo);
            IList<AnimalGroup> animalGroups = new List<AnimalGroup>();

            foreach (BreederStationDataLayer.Orm.Dto.AnimalGroup dtoGroup in dtoGroups)
            {
                animalGroups.Add(mapDtoToDomainObject(dtoGroup));
            }
            return animalGroups;
        }

        public bool RemoveAnimalGroup(int id)
        {
            return animalGroupGateway.Delete(id) > 0;
        }

        public bool UpdateAnimalGroup(AnimalGroup animalGroup)
        {
            return animalGroupGateway.Update(mapDomainToDtoObject(animalGroup)) > 0;
        }

        private BreederStationDataLayer.Orm.Dto.AnimalGroup mapDomainToDtoObject(AnimalGroup animalGroup)
        {
            BreederStationDataLayer.Orm.Dto.AnimalGroup dtoAnimalGroup = new BreederStationDataLayer.Orm.Dto.AnimalGroup
            {
                Id = animalGroup.Id,
                Description = animalGroup.Description,
                AnimalsInfo = animalGroup.AnimalsInfo,
                BreedersInfo = animalGroup.BreedersInfo
            };
            return dtoAnimalGroup;
        }

        private AnimalGroup mapDtoToDomainObject(BreederStationDataLayer.Orm.Dto.AnimalGroup dtoAnimalGroup)
        {
            AnimalGroup aninalGroup = new AnimalGroup
            {
                Id = dtoAnimalGroup.Id,
                Description = dtoAnimalGroup.Description,
                AnimalsInfo = dtoAnimalGroup.AnimalsInfo,
                BreedersInfo = dtoAnimalGroup.BreedersInfo
            };
            return aninalGroup;
        }
    }
}
