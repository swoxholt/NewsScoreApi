using Application.Models;
using Application.Services.Score;

namespace Application.Services;

public class NewsCalculationService : ICalculationService
{
    private readonly Func<MeasurementType, IScoringService> _measurementServices;

    public NewsCalculationService(Func<MeasurementType, IScoringService> measurementServices)
    {
        _measurementServices = measurementServices;
    }

    public async Task<int> CalculateNewsScore(List<Measurement> measurements)
    {
        if (measurements.Count == 0)
        {
            throw new ArgumentException("List of measurements cannot be empty");
        }

        var newsScore = 0;
        foreach (var measurement in measurements)
        {
            newsScore += await _measurementServices(measurement.Type).CalculateNewsScore(measurement.Value);
        }

        return newsScore;
    }
}