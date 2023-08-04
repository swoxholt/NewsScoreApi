using Application.Models;
using Application.Services;
using Application.Services.Score;
using Moq;
using NUnit.Framework;

namespace Tests.Services;

public class NewsCalculationServiceTests
{
    private Mock<IScoringService> _scoringServiceMock;
    private Mock<Func<MeasurementType, IScoringService>> _scoringServiceFactoryMock;

    [SetUp]
    public async Task Setup()
    {
        _scoringServiceMock = new Mock<IScoringService>();
        _scoringServiceFactoryMock = new Mock<Func<MeasurementType, IScoringService>>();
        _scoringServiceFactoryMock.Setup(factory => factory(It.IsAny<MeasurementType>())).Returns(_scoringServiceMock.Object);
    }

    [Test]
    public async Task GivenThatListOfMeasurementsIsEmpty_AnExceptionShouldBeThrown()
    {
        var service = new NewsCalculationService(_scoringServiceFactoryMock.Object);
        var result = Assert.ThrowsAsync<ArgumentException>(async () => await service.CalculateNewsScore(new List<Measurement>()));
        Assert.NotNull(result);
    }

    [Test]
    public async Task GivenThatAListOfMeasurementsIsProvided_AScoreShouldBeCalculated()
    {
        var measurements = new List<Measurement>
            {
                new()
                {
                    Type = MeasurementType.TEMP,
                    Value = 37
                },
                new()
                {
                    Type = MeasurementType.HR,
                    Value = 120
                }
            };


        _scoringServiceMock.Setup(x => x.CalculateNewsScore(It.IsAny<int>())).ReturnsAsync(1);

        var service = new NewsCalculationService(_scoringServiceFactoryMock.Object);
        var result = await service.CalculateNewsScore(measurements);
        Assert.AreEqual(2, result);

        _scoringServiceMock.Verify(x => x.CalculateNewsScore(It.IsAny<int>()), Times.Exactly(2));
    }
}