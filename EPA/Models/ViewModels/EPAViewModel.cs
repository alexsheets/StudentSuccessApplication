namespace EPA.Models.ViewModels
{
    public class EPAViewModel
    {
        // evaluator name, evaluator email, rotation, block, activity tag, date requested, status
        public long ResultId { get; set; }
        public string? EvaluatorName { get; set; }
        public string? EvaluatorEmail { get; set; }
        public string? Rotation { get; set; }
        public string? Block {  get; set; }
        public string? ActivityTag { get; set; }
        public string? Status { get; set; }
        public DateTime? DateRequested { get; set; }
    }
}
