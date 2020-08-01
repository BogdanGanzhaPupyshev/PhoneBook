using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AdressBook.Models
{
    public class Person
    {
        public int PersonId { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Surnamee is required.")]
        public string SurName { get; set; }

        [Required(ErrorMessage = "Adressis required.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Age is required.")]
        public int PersonAge { get; set; }
    }
}