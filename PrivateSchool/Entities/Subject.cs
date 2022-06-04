using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PrivateSchool.Entities
{
    public class Subject : Base<int>
    {
        public int MaxCapacity { get; set; }
        [Required]
        public string Name { get; set; }
      //  public ICollection<Teacher> Teachers { get; set; }
    }
}
