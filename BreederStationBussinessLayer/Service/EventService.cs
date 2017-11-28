using BreederStationBussinessLayer.Domain;
using System.Collections.Generic;

namespace BreederStationBussinessLayer.Service
{
    public interface EventService
    {
        bool AddAnimalEvent(Event animalEvent);
        IList<Event> GetAllAnimalEvents();
        bool RemoveAnimalEvent(int id);
        bool UpdateAnimalEvent(Event animalEvent);
        BreederStationDataLayer.Orm.Dto.Event mapDomainToDtoObject(Event animalEvent);
        Event mapDtoToDomainObject(BreederStationDataLayer.Orm.Dto.Event dtoAnimalEvent);
    }
}
