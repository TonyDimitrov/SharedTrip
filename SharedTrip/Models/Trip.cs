using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SharedTrip.Models
{
    public class Trip
    {
        public Trip()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Users = new HashSet<UserTrip>();            
        }

        public string Id { get; set; }
        [Required]
        public string StartPoint { get; set; }
        [Required]
        public string EndPoint { get; set; }
        public DateTime DepartureTime { get; set; }
        public int Seats { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }

        public virtual IEnumerable<UserTrip> Users { get; set; }

    }
}


