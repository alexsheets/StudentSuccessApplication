using System.ComponentModel.DataAnnotations;


namespace EPA.Models.ViewModels
{
    public class ResultInformationViewModel
    {
        [Key]
        public long? ResultId { get; set; }
        public string? EvaluatorOfEPALastName { get; set; }

        public DateTime? LastUpdatedDateTime { get; set; }

        public string? FullDesc
        {
            get
            {
                return EvaluatorOfEPALastName + ", " + LastUpdatedDateTime;
            }
        }
    }
}
