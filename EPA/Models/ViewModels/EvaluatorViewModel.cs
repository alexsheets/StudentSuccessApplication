using System.ComponentModel.DataAnnotations;

namespace EPA.Models.ViewModels
{
    public class EvaluatorViewModel
    {
        public long EvaluatorId { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }

        public string? Password { get; set; }

        public bool? Lsuind { get; set; }

        public string? Clinic { get; set; }

        public DateTime? UpdateDt { get; set; }

        public string FullName
        {
            get
            {
                return LastName + " " + FirstName;
            }
        }
    }
}
