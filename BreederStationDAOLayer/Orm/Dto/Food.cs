using System;
using System.Text;

namespace BreederStationDataLayer.Orm.Dto
{
    public class Food
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Proteins { get; set; }
        public int Carbohydrates { get; set; }
        public int Fat { get; set; }
        public bool FoodRunningOut { get; set; }
        public Company Company { get; set; }
    }
}
