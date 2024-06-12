using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ContactApplication.Models
{
    public class User
    {
         public User()
        {
            Name = string.Empty; 
            Email = string.Empty;
        }

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Email { get; set; }

    }
}