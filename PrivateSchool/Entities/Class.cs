using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrivateSchool.Entities
{
    public class Class : Base<int>
    {
        public string Name { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        public int SubjectId { get; set; }
        public Subject Subject{ get; set; }

        public virtual ICollection<StudentClasses> StudentClasses { get; set; }

    }
}
