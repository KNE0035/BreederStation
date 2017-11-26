using BreederStationBussinessLayer.Domain;
using BreederStationBussinessLayer.SelectCriteria;
using BreederStationBussinessLayer.ValidationObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreederStationBussinessLayer.Service
{
    public interface AnimalService
    {
        bool addAnimal(Animal animal);
        IList<Animal> GetAnimals(AnimalCriteria criteria);
        Person GetAnimalById(int id);
        bool RemoveAnimal(int id);
        bool UpdateAnimal(Animal animal);
    }
}
