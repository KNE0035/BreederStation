using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BreederStationBussinessLayer.Domain;
using BreederStationDataLayer.Orm.Dao;
using BreederStationDAOLayer.Database;
using BreederStationBussinessLayer.SelectCriteria;

namespace BreederStationBussinessLayer.Service.Impl
{

    public class FoodServiceImpl : FoodService
    {
        private FoodGateway foodGateway = RepositoryRegister.getInstance().Get<FoodGateway>();
        public bool AddFood(Food food)
        {
            return foodGateway.Insert(mapDomainToDtoObject(food)) > 0;
        }

        public IList<Food> GetFoods(FoodCriteria criteria)
        {
            IList<BreederStationDataLayer.Orm.Dto.Food> dtoFoods = foodGateway.Select(new BreederStationDataLayer.Orm.SelectCriteria.FoodCriteria { MinimumCarbohydrates = criteria.MinimumCarbohydrates,
                                                                                                                                                    MinimumFat = criteria.MinimumFat,
                                                                                                                                                    MinimunProteins = criteria.MinimunProteins });
            IList<Food> foods = new List<Food>();
            
            foreach(BreederStationDataLayer.Orm.Dto.Food dtoFood in dtoFoods)
            {
                foods.Add(mapDtoToDomainObject(dtoFood));
            }
            return foods;
        }

        public bool RemoveFood(int id)
        {
            return foodGateway.Delete(id) > 0;
        }

        public bool UpdateFood(Food food)
        {
            return foodGateway.Update(mapDomainToDtoObject(food)) > 0;
        }

        public BreederStationDataLayer.Orm.Dto.Food mapDomainToDtoObject(Food food)
        {
            BreederStationDataLayer.Orm.Dto.Food dtoFood = new BreederStationDataLayer.Orm.Dto.Food
            {
                Id = food.Id,
                Carbohydrates = food.Carbohydrates,
                Fat = food.Fat,
                Proteins = food.Proteins,
                Price = food.Price,
                Name = food.Name,
                FoodRunningOut = food.FoodRunningOut,
                Company = new BreederStationDataLayer.Orm.Dto.Company { Id = food.Company.Id, Trademark = food.Company.Trademark }
            };
            return dtoFood;
        }

        public Food mapDtoToDomainObject(BreederStationDataLayer.Orm.Dto.Food dtoFood)
        {
            Food food = new Food
            {
                Id = dtoFood.Id,
                Carbohydrates = dtoFood.Carbohydrates,
                Fat = dtoFood.Fat,
                Proteins = dtoFood.Proteins,
                Price = dtoFood.Price,
                Name = dtoFood.Name,
                FoodRunningOut = dtoFood.FoodRunningOut,
                Company = new Company { Id = dtoFood.Company.Id, Trademark = dtoFood.Company.Trademark }
            };
            return food;
        }
    }
}
