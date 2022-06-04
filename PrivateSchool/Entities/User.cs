using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace PrivateSchool.Entities
{
    public class User : IdentityUser
    {
        [MaxLength(15)]
        public string FirstName { get; set; }

        [MaxLength(15)]
        public string? LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        [StringLength(10)]
        public string EGN { get; set; }
    }
}
