﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Trip
    {
        public int TripId { get; set; }

        [Required]
        public string Description { get; set; }

        public ICollection<Booking> Bookings { get; set; }

        public Trip()
        {
            this.Bookings = new HashSet<Booking>();
        }
    }
}
