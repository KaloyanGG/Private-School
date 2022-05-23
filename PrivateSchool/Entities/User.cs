using Microsoft.AspNetCore.Identity;
using System;

namespace PrivateSchool.Entities
{
    public class User : Base<int>
    {
        public string PasswordHash { get; set; }
        public string UserName { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }


    }
}
