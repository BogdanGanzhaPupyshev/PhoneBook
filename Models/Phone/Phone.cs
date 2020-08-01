using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AdressBook.Models
{
    public class Phone
    {
        public int PhoneId { get; set; }    

        public int PersonId { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Phone status is required.")]
        public string PhoneStatus { get; set; }
    }
}