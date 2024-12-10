using System.ComponentModel.DataAnnotations;

namespace EPA.Models.ViewModels
{
    public class DashboardChoicesViewModel
    {
        [Required]
        public string SelfOrAllResults { get; set; }
        [Required]
        public string EpaChoiceId { get; set; }
    }
}
