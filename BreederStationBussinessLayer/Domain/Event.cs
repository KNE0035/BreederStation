using System;
using System.Collections.Generic;
using System.Text;

namespace BreederStationBussinessLayer.Domain
{
    public class Event
    {
        public int Id { get; set; }
        public String Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Breeder Breeder { get; set; }
        public IList<Animal> animals { get; set; }

        public string BreederLogin
        {
            get { return Breeder.Login; }
        }

        public string AnimalsNamesString {
            get {
                IList<string> animalNames = new List<string>();

                foreach(Animal animal in animals)
                {
                    animalNames.Add(animal.Name);
                }
                return string.Join<string>(", ", animalNames);
            }
        }
    }
}
