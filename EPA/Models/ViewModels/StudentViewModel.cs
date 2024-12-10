using System.ComponentModel.DataAnnotations;

namespace EPA.Models.ViewModels
{
    public class StudentViewModel
    {
        [Required]
        public long StudentId { get; set; }
        [Required]
        public string? PawsId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public long? ConcentrationId { get; set; }
        public string? ConcentrationStr { get; set; }
        public DateTime? UpdateDt { get; set; }

        public long? ClassId { get; set; }
        public string? ClassStr {  get; set; }

        public bool? Active { get; set; }

    }
}
