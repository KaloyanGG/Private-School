using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrivateSchool.Entities
{
    public class StudentClasses
    {
        public int StudentId { get; set; }
        public User Student { get; set; }
        public int ClassId { get; set; }
        public Class Class{ get; set; }

    }
}
