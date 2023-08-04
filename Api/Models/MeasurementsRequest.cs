using System.ComponentModel.DataAnnotations;

namespace Api.Models;

public class MeasurementsRequest
{
    public List<MeasurementRequest> Measurements { get; set; }
}