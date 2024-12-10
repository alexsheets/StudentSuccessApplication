namespace EPA.Models.ViewModels
{
    public class ExternalRegistrationViewModel
    {
        public int ExternshipId { get; set; }
        public string? PracticeType { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? NumWeeks { get; set; }
        public string? Blocks { get; set; }
        public string? NameOfPractice { get; set; }
        public string? MailingAddress { get; set; }
        public string? City { get; set; }
        public string? State {  get; set; }
        public string? ZipCode { get; set; }

        public string? TelephoneNum { get; set; }
        public string? EvaluatorName { get; set; }
        public string? EvaluatorTitle { get; set; }
        public string? EvaluatorEmail { get; set; }
        
    }
}
