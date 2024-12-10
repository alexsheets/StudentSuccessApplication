using System.ComponentModel.DataAnnotations;

namespace EPA.Models.ViewModels
{
    public class GradingReportChoicesViewModel
    {
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public string Class { get; set; }
    }
}
