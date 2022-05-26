using Microsoft.AspNetCore.Identity;
using System;

namespace PrivateSchool.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string EGN { get; set; }
    }
}
