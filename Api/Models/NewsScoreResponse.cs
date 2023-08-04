namespace Api.Models;

public class NewsScoreResponse
{
    public int Score { get; set; }

    public NewsScoreResponse(int score)
    {
        Score = score;
    }
}