namespace EPA.Models.ViewModels
{
    public class ChartScoresViewModel
    {
        public ChartScoresViewModel(double score, DateTime date)
        {
            AgScore = score;
            DateOfEval = date;
        }

        public double AgScore { get; set; }
        public DateTime DateOfEval { get; set; }
    }
}
