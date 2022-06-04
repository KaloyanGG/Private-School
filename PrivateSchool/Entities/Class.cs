using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PrivateSchool.Entities
{
    public class Class : Base<int>
    {
        [Required]
        public string Name { get; set; }

        public int? TeacherId { get; set; }

        public Teacher Teacher { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}
