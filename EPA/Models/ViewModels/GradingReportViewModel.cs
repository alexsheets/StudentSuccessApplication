namespace EPA.Models.ViewModels
{
    public class GradingReportViewModel
    {
        public int Id { get; set; }
        public string? Semester { get; set; }
        public string? Year { get; set; }
        public string? Name { get; set; }
        public string? PawsId { get; set; }
        public int CountOfSelfEvals { get; set; }
        public DateTime? LastSelfEvalDate { get; set; }
        public DateTime? ReflectionSubmissionDate { get; set; }

        public string? Class {  get; set; }

    }
}
