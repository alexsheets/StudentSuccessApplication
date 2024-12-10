using System.ComponentModel.DataAnnotations;

namespace EPA.Models
{
    public partial class StudentSpecificGradingReport
    {
        [Key]
        public int Id { get; set; }
        public string? Semester { get; set; }
        public string? Year { get; set; }
        public string? Name { get; set; }
        public string PawsId { get; set; }
        public string? Class {  get; set; }

        public DateTime? DateOfLastEval { get; set; }
        
        // epa 1
        public string? Epa1Epa {  get; set; }
        public float? Epa1Grade {  get; set; }
        public string? Epa1Strengths { get; set; }
        public string? Epa1Improve { get; set; }
        public string? Epa1Plan { get; set; }
        // epa 2
        public string? Epa2Epa { get; set; }
        public float? Epa2Grade { get; set; }
        public string? Epa2Strengths { get; set; }
        public string? Epa2Improve { get; set; }
        public string? Epa2Plan { get; set; }
        // epa 3
        public string? Epa3Epa { get; set; }
        public float? Epa3Grade { get; set; }
        public string? Epa3Strengths { get; set; }
        public string? Epa3Improve { get; set; }
        public string? Epa3Plan { get; set; }
        // epa 4
        public string? Epa4Epa { get; set; }
        public float? Epa4Grade { get; set; }
        public string? Epa4Strengths { get; set; }
        public string? Epa4Improve { get; set; }
        public string? Epa4Plan { get; set; }
        // epa 5
        public string? Epa5Epa { get; set; }
        public float? Epa5Grade { get; set; }
        public string? Epa5Strengths { get; set; }
        public string? Epa5Improve { get; set; }
        public string? Epa5Plan { get; set; }

        // reflection
        public string? Reflection1 {  get; set; }
        public string? Reflection2 { get; set; }
        public string? Reflection3 { get; set; }
    }
}
