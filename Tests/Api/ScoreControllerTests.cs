using Api.Controllers;
using Api.Models;
using Application.Models;
using Application.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using NUnit.Framework;

namespace Tests.Api;

public class ScoreControllerTests
{
    private ScoreController _scoreController;
    private Mock<ICalculationService> _calculationServiceMock;
    private Mock<IMapper> _mapperMock;

    [SetUp]
    public void Setup()
    {
        _calculationServiceMock = new Mock<ICalculationService>();
        _mapperMock = new Mock<IMapper>();
        _scoreController = new ScoreController(_calculationServiceMock.Object, _mapperMock.Object, new NullLogger<ScoreController>());
    }

    [Test]
    public async Task CalculateNewScore_ValidRequest_Returns200WithScore()
    {
        var request = new MeasurementsRequest();

        var expectedScore = 42;
        _calculationServiceMock.Setup(service => service.CalculateNewsScore(It.IsAny<List<Measurement>>())).ReturnsAsync(expectedScore);
        
        var result = await _scoreController.CalculateNewScore(request);
        
        Assert.IsInstanceOf<OkObjectResult>(result);

        var okResult = (OkObjectResult)result;
        var newsScoreResponse = (NewsScoreResponse)okResult.Value!;
        Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
        Assert.AreEqual(expectedScore, newsScoreResponse.Score);

        _calculationServiceMock.Verify(service => service.CalculateNewsScore(It.IsAny<List<Measurement>>()), Times.Once());
    }

    [Test]
    public async Task CalculateNewScore_InvalidRequest_ReturnsBadRequest()
    {
        var request = new MeasurementsRequest();

        _calculationServiceMock.Setup(service => service.CalculateNewsScore(It.IsAny<List<Measurement>>())).ThrowsAsync(new ArgumentException("Invalid request"));
        
        var result = await _scoreController.CalculateNewScore(request);
        
        Assert.IsInstanceOf<ObjectResult>(result);
        
        var badRequestResult = (ObjectResult)result;
        Assert.AreEqual(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);

        _calculationServiceMock.Verify(service => service.CalculateNewsScore(It.IsAny<List<Measurement>>()), Times.Once());
    }

    [Test]
    public async Task CalculateNewScore_InternalServerError_ReturnsInternalServerError()
    {
        var request = new MeasurementsRequest();

        _calculationServiceMock.Setup(service => service.CalculateNewsScore(It.IsAny<List<Measurement>>())).ThrowsAsync(new Exception("Invalid request"));

        var result = await _scoreController.CalculateNewScore(request);

        Assert.IsInstanceOf<ObjectResult>(result);

        var badRequestResult = (ObjectResult)result;
        Assert.AreEqual(StatusCodes.Status500InternalServerError, badRequestResult.StatusCode);

        _calculationServiceMock.Verify(service => service.CalculateNewsScore(It.IsAny<List<Measurement>>()), Times.Once());
    }
}