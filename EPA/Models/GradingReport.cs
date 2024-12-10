using System.ComponentModel.DataAnnotations;

namespace EPA.Models
{
    public partial class GradingReport
    {
        [Key]
        public int Id { get; set; }
        public string? Semester { get; set; }
        public string? Year { get; set; }
        public string? Name { get; set; }
        public string PawsId { get; set; }
        public int CountOfSelfEvals { get; set; }
        public DateTime? LastSelfEvalDate { get; set; }
        public DateTime? ReflectionSubmissionDate { get; set; }

    }
}
