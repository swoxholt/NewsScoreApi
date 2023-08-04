using Api.Models;
using Application.Models;
using AutoMapper;

namespace Api.AutoMapper;

public class MeasurementProfile : Profile
{
    public MeasurementProfile()
    {
        CreateMap<MeasurementRequest, Measurement>();
    }
    
}