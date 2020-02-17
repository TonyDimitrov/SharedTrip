using SharedTrip.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedTrip.Services
{
   public interface ITripsService
    {
        void Create(CreateTripVM model);

        DetailTripVM Detail(string id);

        void Join(string tripId, string userId);

       IEnumerable<InfoTripVM> All();

    }
}
