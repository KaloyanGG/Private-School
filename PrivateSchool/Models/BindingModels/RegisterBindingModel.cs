using System;
using System.ComponentModel.DataAnnotations;

namespace PrivateSchool.Models.BindingModels
{
    public class RegisterBindingModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [Range(0,1)]
        public int Type { get; set; }

        [Required]
        [MinLength(3, ErrorMessage ="Too short")]
        //[DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match.")]
        public string RepeatPassword { get; set; }
    }
}
