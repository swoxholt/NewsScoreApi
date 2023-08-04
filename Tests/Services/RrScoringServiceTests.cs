using Application.Services.Score;
using NUnit.Framework;

namespace Tests.Services;

public class RrScoringServiceTests
{
    private IScoringService _service;

    [SetUp]
    public async Task Setup()
    {
        _service = new RrScoringService();
    }

    [Test]
    public async Task GivenThatRrValueIsWithinRange_ServiceShouldReturnValue()
    {
        var value = 11;
        var expectedScore = 1;

        var result = await _service.CalculateNewsScore(value);
        Assert.AreEqual(expectedScore, result);
    }

    [TestCase(3)]
    [TestCase(61)]
    public async Task GivenThatRrValueIsOutsideRange_ServiceShouldThrowArgumentException(int value)
    {
        var result = Assert.ThrowsAsync<ArgumentException>(async () => await _service.CalculateNewsScore(value));
        Assert.NotNull(result);
        Assert.IsTrue(result.Message.Contains("outside range"));
    }
}