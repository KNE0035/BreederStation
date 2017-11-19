using System;
using System.Text;

namespace BreederStationDataLayer.Orm.Dto
{
    public class Role
    {

        public int Id { get; set; }
        public RoleEnum Type { get; set; }
        public string Description { get; set; }


        public override string ToString()
        {
            StringBuilder ret = new StringBuilder();
            ret.Append("typ role: " + Type.ToString() +Environment.NewLine);
            ret.Append("popis role: " + Description + Environment.NewLine);
            return ret.ToString();
        } 
    }
}
