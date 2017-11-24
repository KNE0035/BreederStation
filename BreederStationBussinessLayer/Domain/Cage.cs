using System;
using System.Text;


namespace BreederStationBussinessLayer.Domain
{
    public class Cage
    {
        public int Id { get; set; }
        public int LengthM { get; set; }
        public int WidthM { get; set; }
        public string Animals { get; set; }
        public string Cleaners { get; set; }

        public override string ToString()
        {
            StringBuilder ret = new StringBuilder();
            ret.Append("id kotce: " + Id + Environment.NewLine);
            ret.Append("Delka v metrech: " + LengthM + Environment.NewLine);
            ret.Append("Sirka v metrech: " + WidthM + Environment.NewLine);

            if(Animals == " ")
            {
                ret.Append("Zvirata v kleci: " + "klec neobsahuje zadna zvirata" + Environment.NewLine);
            }
            else if(Animals != null)
            {
                ret.Append("Zvirata v kleci: " + Animals + Environment.NewLine);
            }

            if (Cleaners == " ")
            {
                ret.Append("Uklizeci starajici se o klec: " + "o klec se nidko nestara" + Environment.NewLine);
            }
            else if(Cleaners != null)
            {
                ret.Append("Zvirata v kleci: " + Cleaners + Environment.NewLine);
            }
            return ret.ToString();
        }
    }
}
