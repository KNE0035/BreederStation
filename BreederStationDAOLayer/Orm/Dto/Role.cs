using System;
using System.Text;

namespace BreederStationDataLayer.Orm.Dto
{
    public class Role
    {
        public int Id { get; set; }
        public RoleEnum Type { get; set; }
        public string Description { get; set; }
    }
}
