using Application.Services.Score;
using NUnit.Framework;

namespace Tests.Services;

public class TempScoringServiceTests
{
    private IScoringService _service;

    [SetUp]
    public void Setup()
    {
        _service = new TempScoringService();
    }

    [Test]
    public async Task GivenThatTempValueIsWithinRange_ServiceShouldReturnValue()
    {
        var value = 37;
        var expectedScore = 0;

        var result = await _service.CalculateNewsScore(value);
        Assert.AreEqual(expectedScore, result);
    }

    [TestCase(31)]
    [TestCase(43)]
    public async Task GivenThatTempValueIsOutsideRange_ServiceShouldThrowArgumentException(int value)
    {
        var result = Assert.ThrowsAsync<ArgumentException>(async () => await _service.CalculateNewsScore(value));
        Assert.NotNull(result);
        Assert.IsTrue(result.Message.Contains("outside range"));
    }
}