using System;
using System.ComponentModel.DataAnnotations;

namespace PrivateSchool.Models.BindingModels
{
    public class UpdateUserBindingModel
    {
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$", ErrorMessage = "The field DateOfBirth date is not a valid format.")]
        public string DateOfBirth { get; set; }

        [Required]
        public string EGN { get; set; }
    }
}
