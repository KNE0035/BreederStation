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
    }
}
