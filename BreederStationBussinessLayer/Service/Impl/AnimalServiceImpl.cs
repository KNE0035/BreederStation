using BreederStationDAOLayer.Database;
using BreederStationDataLayer.Orm.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BreederStationBussinessLayer.Domain;
using BreederStationBussinessLayer.SelectCriteria;
using BreederStationBussinessLayer.Domain.Enums;

namespace BreederStationBussinessLayer.Service.Impl
{
    public class AnimalServiceImpl : AnimalService
    {
        private AnimalGateway animalGateway = RepositoryRegister.getInstance().Get<AnimalGateway>();

        public bool AddAnimal(Animal animal)
        {
            animal.Active = true;
            return animalGateway.Insert(mapDomainToDtoObject(animal)) > 0;
        }

        public Animal GetAnimalById(int id)
        {
            return mapDtoToDomainObject(animalGateway.Select(id));
        }

        public IList<Animal> GetAnimals(AnimalCriteria criteria)
        {
            int? cageId = null;
            int? groupId = null;
            if(criteria.CageId > 0)
            {
                cageId = criteria.CageId;
            }

            if(criteria.AnimalGroupId > 0)
            {
                groupId = criteria.AnimalGroupId;
            }

            IList<BreederStationDataLayer.Orm.Dto.Animal> dtoAnimals =  animalGateway.Select(new BreederStationDataLayer.Orm.SelectCriteria.AnimalCriteria() { AnimalGroupId = groupId, CageId = cageId });
            IList<Animal> animals = new List<Animal>();

            foreach (BreederStationDataLayer.Orm.Dto.Animal dtoAnimal in dtoAnimals)
            {
                animals.Add(mapDtoToDomainObject(dtoAnimal));
            }

            animals = applyAnimalFilters(criteria, animals);
            return animals;
        }

        public bool RemoveAnimal(int id)
        {
            return animalGateway.Delete(id) > 0;
        }

        public bool UpdateAnimal(Animal animal)
        {
            return animalGateway.Update(mapDomainToDtoObject(animal)) > 0;
        }

        public BreederStationDataLayer.Orm.Dto.Animal mapDomainToDtoObject(Animal animal)
        {
            BreederStationDataLayer.Orm.Dto.Animal dtoAnimalGroup = new BreederStationDataLayer.Orm.Dto.Animal
            {
                Id = animal.Id,
                Active = animal.Active,
                AnimalGroup = new BreederStationDataLayer.Orm.Dto.AnimalGroup { Id = animal.AnimalGroup.Id, Description = animal.AnimalGroup.Description },
                BirthDate = animal.BirthDate,
                Cage = new BreederStationDataLayer.Orm.Dto.Cage { Id = animal.Cage.Id },
                Food = new BreederStationDataLayer.Orm.Dto.Food { Id = animal.Food.Id, Name = animal.Food.Name },
                LastActiveDate = animal.LastActiveDate,
                Name = animal.Name,
                Race = animal.Race,
                Sex = (BreederStationDataLayer.SexEnum)((int)animal.Sex)
            };
            return dtoAnimalGroup;
        }

        public Animal mapDtoToDomainObject(BreederStationDataLayer.Orm.Dto.Animal dtoAnimal)
        {
            Animal domainAnimalGroup = new Animal
            {
                Id = dtoAnimal.Id,
                Active = dtoAnimal.Active,
                AnimalGroup = new AnimalGroup { Id = dtoAnimal.AnimalGroup.Id, Description = dtoAnimal.AnimalGroup.Description },
                BirthDate = dtoAnimal.BirthDate,
                Cage = new Cage { Id = dtoAnimal.Cage.Id },
                Food = new Food { Id = dtoAnimal.Food.Id, Name = dtoAnimal.Food.Name },
                LastActiveDate = dtoAnimal.LastActiveDate,
                Name = dtoAnimal.Name,
                Race = dtoAnimal.Race,
                Sex = (SexEnum)(int)dtoAnimal.Sex
            };
            return domainAnimalGroup;
        }

        private IList<Animal> applyAnimalFilters(AnimalCriteria criteria, IList<Animal> animals)
        {
            if(criteria.Age > 0)
            {
                animals = animals.Where(animal => animal.GetAnimalAge() == criteria.Age).ToList();
            }

            if (!String.IsNullOrEmpty(criteria.Name))
            {
                animals = animals.Where(animal => animal.Name.ToLower().Contains(criteria.Name.ToLower())).ToList();
            }

            if (!String.IsNullOrEmpty(criteria.Race))
            {
                animals = animals.Where(animal => animal.Race.ToLower().Contains(criteria.Race.ToLower())).ToList();
            }

            if (criteria.Sex != null)
            {
                animals = animals.Where(animal => animal.Sex == criteria.Sex).ToList();
            }
            return animals;
        }
    }
}
