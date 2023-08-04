using Application.Services.Score;
using NUnit.Framework;

namespace Tests.Services;

public class HrScoringServiceTests
{

    private IScoringService _service;

    [SetUp]
    public async Task Setup()
    {
        _service = new HrScoringService();
    }

    [Test]
    public async Task GivenThatHrValueIsWithinRange_ServiceShouldReturnValue()
    {
        var value = 37;
        var expectedScore = 3;

        var result = await _service.CalculateNewsScore(value);
        Assert.AreEqual(expectedScore, result);
    }

    [TestCase(25)]
    [TestCase(221)]
    public async Task GivenThatHrValueIsOutsideRange_ServiceShouldThrowArgumentException(int value)
    {
        var result = Assert.ThrowsAsync<ArgumentException>(async () => await _service.CalculateNewsScore(value));
        Assert.NotNull(result);
        Assert.IsTrue(result.Message.Contains("outside range"));
    }

}