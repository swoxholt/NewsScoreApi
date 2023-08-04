using System.Text.Json.Serialization;
using Api.AutoMapper;
using Api.Models;
using Api.Validator;
using Application.Models;
using Application.Services;
using Application.Services.Score;
using FluentValidation;
using FluentValidation.AspNetCore;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Application configuration
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddScoped<IValidator<MeasurementsRequest>, MeasurementsRequestValidator>();

builder.Services.AddAutoMapper(typeof(MeasurementProfile));
builder.Services.AddScoped<HrScoringService>();
builder.Services.AddScoped<RrScoringService>();
builder.Services.AddScoped<TempScoringService>();
builder.Services.AddScoped<Func<MeasurementType, IScoringService>>(serviceProvider => key =>
{
    return key switch
    {
        MeasurementType.HR => serviceProvider.GetService<HrScoringService>()!,
        MeasurementType.RR => serviceProvider.GetService<RrScoringService>()!,
        MeasurementType.TEMP => serviceProvider.GetService<TempScoringService>()!,
        _ => throw new NotImplementedException($"{key} calculation service is not implemented")
    };
});


builder.Services.AddScoped<ICalculationService, NewsCalculationService>();
//

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
