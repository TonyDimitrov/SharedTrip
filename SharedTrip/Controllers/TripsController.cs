using SharedTrip.Services;
using SharedTrip.ViewModels;
using SIS.HTTP;
using SIS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedTrip.Controllers
{
   public class TripsController : Controller
    {
        private readonly ITripsService tripsService;

        public TripsController(ITripsService tripsService)
        {
            this.tripsService = tripsService;
        }

        [HttpGet]
        public HttpResponse Add()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/users/login");
            }
            return this.View();
        }

        [HttpPost]
        public HttpResponse Add(CreateTripVM model)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/users/login");
            }

            if (string.IsNullOrEmpty(model.StartPoint)
                || string.IsNullOrEmpty(model.EndPoint)
                || model.Description.Length > 80
                || model.Seats < 2
                || model.Seats > 6)
            {
                return this.View();
            }

            this.tripsService.Create(model);
            return this.Redirect("/");
        }

        [HttpGet]
        public HttpResponse All()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/users/login");
            }

            var trips = this.tripsService.All();
            return this.View(trips);
        }

        [HttpGet]
        public HttpResponse Details(string tripId)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/users/login");
            }
            var trip = this.tripsService.Detail(tripId);

            return this.View(trip);
        }

        [HttpGet]
        public HttpResponse AddUserToTrip(string tripId)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/users/login");
            }

            var userId = this.User;
            this.tripsService.Join(tripId, userId);

            return this.All();
        }
    }
}
