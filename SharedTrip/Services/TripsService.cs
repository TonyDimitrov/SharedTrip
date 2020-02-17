using SharedTrip.Models;
using SharedTrip.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace SharedTrip.Services
{
    public class TripsService : ITripsService
    {
        private readonly ApplicationDbContext db;

        public TripsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<InfoTripVM> All()
        {
            return this.db.Trips.Select(t => new InfoTripVM
            {
                Id = t.Id,
                Seats = t.Seats,
                DepartureTime = t.DepartureTime,
                EndPoint = t.EndPoint,
                StartPoint = t.StartPoint
            }).ToArray();
        }

        public void Create(CreateTripVM model)
        {
            var formatedTime = DateTime.ParseExact(model.DepartureTime, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture);
            this.db.Trips.Add(new Trip
            {
                Description = model.Description,
                DepartureTime = formatedTime,
                EndPoint = model.EndPoint,
                ImagePath = model.ImagePath,
                Seats = model.Seats,
                StartPoint = model.StartPoint
            });

            this.db.SaveChanges();
        }

        public DetailTripVM Detail(string id)
        {
            return this.db.Trips.Where(t => t.Id == id).Select(t => new DetailTripVM
            {
                Id = t.Id,
                DepartureTime = t.DepartureTime.ToString("dd.MM.yyyy HH:mm"),
                Description = t.Description,
                EndPoint = t.EndPoint,
                ImagePath = t.ImagePath,
                Seats = t.Seats,
                StartPoint = t.StartPoint

            }).FirstOrDefault();
        }

        public void Join(string tripId, string userId)
        {
            var alreadyJoined = this.db.UserTrips
                .Any(tr => tr.TripId == tripId && tr.UserId == userId);

            if (alreadyJoined)
            {
                return;
            }

            var currentTrip = this.db.Trips
                .Where(tr => tr.Id == tripId)
                .FirstOrDefault();

            if (currentTrip.Seats > 0)
            {
                currentTrip.Seats -= 1;
            }
            else
            {
                return;
            }
            this.db.UserTrips.Add(new UserTrip
            {
                TripId = tripId,
                UserId = userId
            });

            this.db.SaveChanges();
        }
    }
}
