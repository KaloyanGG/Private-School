using System.Collections.Generic;

namespace PrivateSchool.Entities
{
    public class Teacher : Base<int>
    {
        public string UserId { get; set; }

        public User User { get; set; }

        public int? Level { get; set; }

        public int? SubjectId { get; set; }
        public virtual Subject Subject { get; set; }

        //public ICollection<Class> Classes { get; set; }
    }
}
