namespace Application.Models;

public class ScoringRange
{
    public ScoringRange(int from, int to, int score)
    {
        From = from;
        To = to;
        Score = score;
    }

    public int From { get; set; }
    public int To { get; set; }
    public int Score { get; set; }

    public bool IsInRange(int value)
    {
        return value > From && value <= To;
    }
}