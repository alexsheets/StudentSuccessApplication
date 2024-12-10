using System.ComponentModel.DataAnnotations;

namespace EPA.Models
{
    public partial class ExternshipRegistration
    {
        [Key]
        public int ExternshipId { get; set; }
        public int StudentId { get; set; }
        public int Visibility { get; set; }
        [Required]
        public string PracticeType { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public string NumWeeks { get; set; }
        [Required]
        public string Blocks { get; set; }
        [Required]
        public string NameOfPractice { get; set; }
        public string? MailingAddress { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        public string? ZipCode { get; set; }

        public string? TelephoneNum { get; set; }
        [Required]
        public string EvaluatorName { get; set; }
        public string? EvaluatorTitle { get; set; }
        [Required]
        public string EvaluatorEmail { get; set; }
    }
}
