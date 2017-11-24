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
            throw new NotImplementedException();
        }

        public IList<AnimalGroup> GetAllAnimalGroups(bool allInfo)
        {
            IList<BreederStationDataLayer.Orm.Dto.AnimalGroup> dtoGroups = animalGroupGateway.Select(allInfo);
            IList<AnimalGroup> animalGroups = new List<AnimalGroup>();

            foreach (BreederStationDataLayer.Orm.Dto.AnimalGroup dtoGroup in dtoGroups)
            {
                AnimalGroup aninalGroup;

                aninalGroup = new AnimalGroup
                {
                    Id = dtoGroup.Id,
                    Description = dtoGroup.Description,
                    AnimalsInfo = dtoGroup.AnimalsInfo,
                    BreedersInfo = dtoGroup.BreedersInfo
                };

                animalGroups.Add(aninalGroup);
            }
            return animalGroups;
        }

        public bool RemoveAnimalGroup(int id)
        {
            throw new NotImplementedException();
        }

        public Person UpdateAnimalGroup(AnimalGroup animalGroup)
        {
            throw new NotImplementedException();
        }
    }
}
