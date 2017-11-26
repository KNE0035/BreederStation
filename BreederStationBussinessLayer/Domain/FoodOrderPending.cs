using BreederStationBussinessLayer.Domain.Enums;
using System;
using System.Text;

namespace BreederStationBussinessLayer.Domain
{
    public class FoodOrderPending
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? ResolvedDate { get; set; }
        public FoodOrderPriorityEnum Priority { get; set; }
        public Food Food { get; set; }


        public override string ToString()
        {
            StringBuilder ret = new StringBuilder();
            ret.Append("id cekajici objednavky: " + Id + Environment.NewLine);
            ret.Append("zacatek: " + StartDate + Environment.NewLine);
            if (ResolvedDate != null)
            {
                ret.Append("datum vyrizeni:  " + ResolvedDate + Environment.NewLine);
            }

            ret.Append("priorita:  " + Priority.ToString() + Environment.NewLine);

            if (Food != null)
            {
                ret.Append(Food.ToString());
            }
            return ret.ToString();
        }
    }
}
