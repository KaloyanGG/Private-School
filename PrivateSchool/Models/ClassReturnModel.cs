using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrivateSchool.Models
{
    public class ClassReturnModel
    {
        //TODO: Change to not null
        public int? Id { get; set; }
        public string Name { get; set; }
        public string TeacherName { get; set; }
        public string SubjectName { get; set; }



    }
}
