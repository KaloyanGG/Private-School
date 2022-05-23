using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrivateSchool.Data
{
    public static class StaticData
    {
        public static Dictionary<int, string> Roles = new Dictionary<int, string>() { { 0, "Student" }, { 1, "Teacher" } };
    }
}
