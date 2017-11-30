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
    }
}
