using Api.Models;
using FluentValidation;

namespace Api.Validator;

public class MeasurementsRequestValidator : AbstractValidator<MeasurementsRequest>
{
    public MeasurementsRequestValidator()
    {
        RuleFor(model => model.Measurements).NotEmpty();
        RuleForEach(model => model.Measurements).ChildRules(measurement =>
        {
            measurement.RuleFor(x => x.Value).NotEmpty().WithMessage(x => $"Measurement type {x.Type} requires a value");
            measurement.RuleFor(x => x.Value).GreaterThan(0).WithMessage(x => $"Measurement type {x.Type} must have a value greater then 0");
        });
    }
}