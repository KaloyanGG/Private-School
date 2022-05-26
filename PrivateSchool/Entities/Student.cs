using System.Collections.Generic;

namespace PrivateSchool.Entities
{
    public class Student : Base<int>
    {
        public string UserId { get; set; }

        public User User { get; set; }

        public double? AverageGrade { get; set; }

        public virtual ICollection<Class> Classes { get; set; }
    }
}
