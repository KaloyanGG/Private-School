using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrivateSchool.Models.BindingModels
{
    public class UpdateStudentBindingModel : UpdateUserBindingModel
    {
        [Range(2,6)]
        public double AverageGrade { get; set; }
    }
}
