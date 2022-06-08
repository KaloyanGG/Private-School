using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrivateSchool.Models.BindingModels
{
    public class UpdateTeacherBindingModel : UpdateUserBindingModel
    {
        [Range(1,5)]
        public int Level { get; set; }

    }
}
