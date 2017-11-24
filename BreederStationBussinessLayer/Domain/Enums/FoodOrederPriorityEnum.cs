using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreederStationBussinessLayer.Domain.Enums
{
    public enum FoodOrderPriorityEnum { low, medium, high };
    public class FoodOrderPriorityEnumUtils
    {
        public static FoodOrderPriorityEnum getPriority(string type)
        {
            switch (type)
            {
                case "low":
                    return FoodOrderPriorityEnum.low;
                case "medium":
                    return FoodOrderPriorityEnum.medium;
                case "high":
                    return FoodOrderPriorityEnum.high;
            }
            throw new ArgumentException();
        }
    }
}
