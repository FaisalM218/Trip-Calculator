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
        [StringLength(30, ErrorMessage = "Do not enter more than 30 characters")]
        [Display(Name = "User Name")]
        [Required]
        public string UserName { get; set; }

        [Index(IsUnique = true)]
        [StringLength(30, ErrorMessage = "Do not enter more than 30 characters")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [StringLength(30, ErrorMessage = "Do not enter more than 30 characters")]
        [Display(Name = "First Name")]
        [Required]
        public string FirstName { get; set; }

        [StringLength(30, ErrorMessage = "Do not enter more than 30 characters")]
        [Display(Name = "Last Name")]
        [Required]
        public string LastName { get; set; }
    }
}