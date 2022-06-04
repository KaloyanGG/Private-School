using System.ComponentModel.DataAnnotations;

namespace PrivateSchool.Models.BindingModels
{
    public class AddSubjectBindingModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Range(1, 100)]
        public int MaxCapacity { get; set; }
    }
}
