using System.Collections.Generic;
namespace PrivateSchool.Entities
{
    public class Class : Base<int>
    {
        public string Name { get; set; }

        public int TeacherId { get; set; }

        public Teacher Teacher { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}
