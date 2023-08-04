using Application.Models;
using System.ComponentModel.DataAnnotations;

namespace Api.Models;

public class MeasurementRequest
{
    public MeasurementType Type { get; set; }
    
    public int? Value { get; set; }
}