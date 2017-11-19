using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreederStationDataLayer.Orm.SelectCriteria
{
    public class PersonCriteria
    {
        public RoleEnum? Role { get; set; }
        public string WholeName { get; set; }
    }
}
