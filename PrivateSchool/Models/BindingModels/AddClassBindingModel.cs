using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrivateSchool.Models.BindingModels
{
    public class AddClassBindingModel
    {
        public int TeacherId { get; set; }
        public int SubjectId { get; set; }
        public string Name { get; set; }

    }
}
