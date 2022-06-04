using System.ComponentModel.DataAnnotations;

namespace PrivateSchool.Models.BindingModels
{
    public class AddClassBindingModel
    {
        [Required]
        public int TeacherId { get; set; }

        [Required]
        public string Name { get; set; }

    }
}
