namespace Application.Services.Score;

public interface IScoringService
{
    Task<int> CalculateNewsScore(int value);
}