using System.ComponentModel.DataAnnotations;

namespace TechJobs6Persistent.ViewModels
{
    public class AddEmployerViewModel
    {
        [Required(ErrorMessage = "Please enter an employer name")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Please enter the employer's location")]
        public string? Location { get; set; }
    }
}
