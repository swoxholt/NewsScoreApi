using Application.Models;

namespace Application.Services.Score;

public class RrScoringService : IScoringService
{
    private readonly List<ScoringRange> _scoringRanges = new List<ScoringRange>
    {
        new(3, 8, 3),
        new(8, 11, 1),
        new(11, 20, 0),
        new(20, 24, 2),
        new(24, 60, 3),
    };

    public async Task<int> CalculateNewsScore(int value)
    {
        var score = _scoringRanges.FirstOrDefault(x => x.IsInRange(value));
        if (score == null)
        {
            throw new ArgumentException($"Respiratory rate value {value} is outside range");
        }

        return score.Score;
    }
}