using Application.Models;

namespace Application.Services.Score;

public class TempScoringService : IScoringService
{
    private readonly List<ScoringRange> _scoringRanges = new List<ScoringRange>
    {
        new(31, 35, 3),
        new(35, 36, 1),
        new(36, 38, 0),
        new(38, 39, 1),
        new(39, 42, 2),
    };

    public async Task<int> CalculateNewsScore(int value)
    {
        var score = _scoringRanges.FirstOrDefault(x => x.IsInRange(value));
        if (score == null)
        {
            throw new ArgumentException($"Body temperature value {value} is outside range");
        }

        return score.Score;
    }
}