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
        bool AddAnimal(Animal animal);
        IList<Animal> GetAnimals(AnimalCriteria criteria);
        Animal GetAnimalById(int id);
        bool RemoveAnimal(int id);
        bool UpdateAnimal(Animal animal);
    }
}
