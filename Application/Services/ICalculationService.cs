using Application.Models;

namespace Application.Services;

public interface ICalculationService
{
    Task<int> CalculateNewsScore(List<Measurement> measurements);
}