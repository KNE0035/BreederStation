using BreederStationBussinessLayer.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreederStationBussinessLayer.Service
{
    public interface CageService
    {
        bool AddCage(Cage cage);
        IList<Cage> GetAllCages();
        bool RemoveCage(int id);
        bool UpdateCage(Cage cage);

    }
}
