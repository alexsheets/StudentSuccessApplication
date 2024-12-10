using System.ComponentModel.DataAnnotations;

namespace EPA.Models.ViewModels
{
    public class InitDashboardChoicesViewModel
    {
        [Required]
        public string StudentOrSelfComparison { get; set; }
        [Required]
        public string EpaChoiceId { get; set; }
    }
}
