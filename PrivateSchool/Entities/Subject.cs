using System.Collections.Generic;
namespace PrivateSchool.Entities
{
    public class Subject : Base<int>
    {
        public int MaxCapacity { get; set; }
        public string Name { get; set; }
      //  public ICollection<Teacher> Teachers { get; set; }
    }
}
