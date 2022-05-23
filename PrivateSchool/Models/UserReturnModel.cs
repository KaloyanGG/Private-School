using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrivateSchool.Models
{
    public class UserReturnModel
    {
        public string Username { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }
}
