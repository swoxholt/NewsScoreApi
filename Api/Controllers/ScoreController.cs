using Api.Models;
using Application.Models;
using Application.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("score")]
    public class ScoreController : ControllerBase
    {
        private ICalculationService _calculationService;
        private IMapper _mapper;
        private readonly ILogger<ScoreController> _logger;

        public ScoreController(ICalculationService calculationService, IMapper mapper, ILogger<ScoreController> logger)
        {
            _calculationService = calculationService;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Calculate the NEWS score based on temp, hr and rr measurements 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("calculate")]
        [ProducesResponseType(typeof(int), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CalculateNewScore(MeasurementsRequest request)
        {
            try
            {
                return Ok(await _calculationService.CalculateNewsScore(
                    _mapper.Map<List<Measurement>>(request.Measurements)));
            }
            catch (ArgumentException e)
            {
                return ValidationProblem(e.Message, statusCode: StatusCodes.Status400BadRequest);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"An internal server error occurred : {e.Message}");
            }
        }
    }
}
