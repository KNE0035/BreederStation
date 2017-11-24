using System;
using System.Text;

namespace BreederStationBussinessLayer.Domain
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

        public override string ToString()
        {
            StringBuilder ret = new StringBuilder();
            ret.Append("id jidla: " + Id + Environment.NewLine);
            ret.Append("jmeno jidla: " + Name + Environment.NewLine);
            ret.Append("cena jidla: " + Price + Environment.NewLine);
            ret.Append("bilkoviny: " + Proteins + Environment.NewLine);
            ret.Append("cukry: " + Carbohydrates + Environment.NewLine);
            ret.Append("tuky:  " + Fat + Environment.NewLine);
            ret.Append("dochazi jidlo?:  " + FoodRunningOut + Environment.NewLine);

            if (Company != null)
            {
                ret.Append(Company.ToString());
            }
            return ret.ToString();
        }
    }
}
