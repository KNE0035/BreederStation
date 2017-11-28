using BreederStationBussinessLayer.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreederStationBussinessLayer.Service
{
    public interface AnimalGroupService
    {
        bool AddAnimalGroup(AnimalGroup animalGroup);
        IList<AnimalGroup> GetAllAnimalGroups(bool allIfo);
        bool RemoveAnimalGroup(int id);
        bool UpdateAnimalGroup(AnimalGroup animalGroup);

        BreederStationDataLayer.Orm.Dto.AnimalGroup mapDomainToDtoObject(AnimalGroup animalGroup);
        AnimalGroup mapDtoToDomainObject(BreederStationDataLayer.Orm.Dto.AnimalGroup dtoAnimalGroup);
        

    }
}
