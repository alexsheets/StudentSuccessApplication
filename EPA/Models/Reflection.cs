using System.ComponentModel.DataAnnotations;

namespace EPA.Models
{
    public partial class Reflection
    {
        [Key]
        public int ReflectionId { get; set; }
        public string PawsId { get; set; }
        public int? ReflectionQuestionId { get; set; }
        public string ReflectionAnswer {  get; set; }

        public string? Season { get; set; }
        public string? Year { get; set; }

        public DateTime? DateSubmitted { get; set; }
    }
}
