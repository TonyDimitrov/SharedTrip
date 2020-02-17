using SIS.MvcFramework;
using System;
using System.Collections.Generic;

namespace SharedTrip.Models
{
    public class User : IdentityUser<string>
    {
        public User()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Trips = new HashSet<UserTrip>();
        }
        public virtual IEnumerable<UserTrip> Trips { get; set; }
    }
}
//•	Has an Id – a string, Primary Key
//•	Has a Username – a string with min length 5 and max length 20 (required)
//•	Has an Email - a string (required)
//•	Has a Password – a string with min length 6 and max length 20  - hashed in the database(required)
//•	Has UserTrips collection – a UserTrip type
