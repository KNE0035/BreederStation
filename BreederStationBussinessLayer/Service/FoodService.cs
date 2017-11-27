using BreederStationBussinessLayer.Domain;
using BreederStationBussinessLayer.SelectCriteria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreederStationBussinessLayer.Service
{
    public interface FoodService
    {
        bool AddFood(Food food);
        IList<Food> GetFoods(FoodCriteria criteria);
        bool RemoveFood(int id);
        bool UpdateFood(Food food);

    }
}
