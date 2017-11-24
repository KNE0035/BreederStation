using BreederStationBussinessLayer.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreederStationBussinessLayer.Service.Impl
{
    public interface AnimalGroupService
    {
        bool AddAnimalGroup(AnimalGroup animalGroup);
        IList<AnimalGroup> GetAllAnimalGroups(bool allIfo);
        bool RemoveAnimalGroup(int id);
        Person UpdateAnimalGroup(AnimalGroup animalGroup);

    }
}
