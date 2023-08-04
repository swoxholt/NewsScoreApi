using Application.Models;

namespace Application.Services.Score;

public class HrScoringService : IScoringService
{
    private readonly List<ScoringRange> _scoringRanges = new List<ScoringRange>
    {
        new(25, 40, 3),
        new(40, 50, 1),
        new(50, 90, 0),
        new(90, 110, 1),
        new(110, 130, 2),
        new(130, 220, 3)
    };

    
    /// Calculate the HR score based on where in the scoring range the value falls
    public async Task<int> CalculateNewsScore(int value)
    {
        var score = _scoringRanges.FirstOrDefault(x => x.IsInRange(value));
        if (score == null)
        {
            throw new ArgumentException($"Heart rate value {value} is outside range");
        }

        return score.Score;
    }
}