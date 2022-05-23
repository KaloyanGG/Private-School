using System.Collections.Generic;

namespace PrivateSchool.Entities
{
    public class Student : User
    {
        public double? AverageGrade { get; set; }

        public virtual ICollection<StudentClasses> StudentClasses{ get; set; }

    }
}
