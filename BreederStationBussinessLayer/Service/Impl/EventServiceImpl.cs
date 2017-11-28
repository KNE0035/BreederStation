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
    public class EventServiceImpl : EventService
    {
        private EventGateway eventGateway = RepositoryRegister.getInstance().Get<EventGateway>();

        public bool AddAnimalEvent(Event animalEvent)
        {
            return eventGateway.Insert(mapDomainToDtoObject(animalEvent)) > 0;
        }

        public IList<Event> GetAllAnimalEvents()
        {
            IList<BreederStationDataLayer.Orm.Dto.Event> dtoAnimalEvents = eventGateway.Select();
            IList<Event> animalEvents = new List<Event>();

            foreach (BreederStationDataLayer.Orm.Dto.Event dtoEvent in dtoAnimalEvents)
            {
                animalEvents.Add(mapDtoToDomainObject(dtoEvent));
            }
            return animalEvents;
        }

        public bool RemoveAnimalEvent(int id)
        {
            return eventGateway.Delete(id) > 0;
        }

        public bool UpdateAnimalEvent(Event animalEvent)
        {
            return eventGateway.Update(mapDomainToDtoObject(animalEvent)) > 0;
        }

        public BreederStationDataLayer.Orm.Dto.Event mapDomainToDtoObject(Event animalEvent)
        {
            IList<BreederStationDataLayer.Orm.Dto.Animal> dtoBaseAnimals = new List<BreederStationDataLayer.Orm.Dto.Animal>();

            foreach(Animal animal in animalEvent.animals)
            {
                BreederStationDataLayer.Orm.Dto.Animal dtoBaseAnimal = new BreederStationDataLayer.Orm.Dto.Animal { Id = animal.Id, Name = animal.Name };
                dtoBaseAnimals.Add(dtoBaseAnimal);
            }

            BreederStationDataLayer.Orm.Dto.Event dtoAnimalEvent = new BreederStationDataLayer.Orm.Dto.Event
            {
                animals = dtoBaseAnimals,
                Breeder = new BreederStationDataLayer.Orm.Dto.Breeder { Id = animalEvent.Breeder.Id, Login = animalEvent.Breeder.Login },
                Description = animalEvent.Description,
                EndDate = animalEvent.EndDate,
                Id = animalEvent.Id,
                StartDate = animalEvent.StartDate
            };
            return dtoAnimalEvent;
        }

        public Event mapDtoToDomainObject(BreederStationDataLayer.Orm.Dto.Event dtoAnimalEvent)
        {
            IList<Animal> baseAnimals = new List<Animal>();

            foreach (BreederStationDataLayer.Orm.Dto.Animal dtoAnimal in dtoAnimalEvent.animals)
            {
                Animal baseAnimal = new Animal { Id = dtoAnimal.Id, Name = dtoAnimal.Name };
                baseAnimals.Add(baseAnimal);
            }

            Event animalEvent = new Event
            {
                animals = baseAnimals,
                Breeder = new Breeder { Id = dtoAnimalEvent.Breeder.Id, Login = dtoAnimalEvent.Breeder.Login },
                Description = dtoAnimalEvent.Description,
                EndDate = dtoAnimalEvent.EndDate,
                Id = dtoAnimalEvent.Id,
                StartDate = dtoAnimalEvent.StartDate
            };
            return animalEvent;
        }
    }
}
