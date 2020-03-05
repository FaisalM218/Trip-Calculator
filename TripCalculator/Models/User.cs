using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TripCalculator.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Index(IsUnique = true)]
        [StringLength(50)]
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}