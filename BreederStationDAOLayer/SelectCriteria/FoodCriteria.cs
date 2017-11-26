using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreederStationDataLayer.Orm.SelectCriteria
{
    public class FoodCriteria
    {
        public int? MinimunProteins { get; set; }
        public int? MinimumFat { get; set; }
        public int? MinimumCarbohydrates { get; set; }
    }
}
