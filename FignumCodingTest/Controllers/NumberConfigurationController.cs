using FignumCodingTest.Helper;
using FignumCodingTest.Models;
using Microsoft.AspNetCore.Mvc;

namespace FignumCodingTest.Controllers
{
    [ApiController]
    [Route("fignum/[controller]")]
    public class NumberConfigurationController : ControllerBase
    {

        private readonly ILogger<NumberConfigurationController> _logger;

        public NumberConfigurationController(ILogger<NumberConfigurationController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<Request>> Execute(Request request)
        {
            try
            {
                var result = await NumberConfigurationHelper.RemovePrimeNumbersFilter(request.InputStringNumber);

                result = await NumberConfigurationHelper.SortFilter(result);

                if (result.CheckAndSortedList != null)
                {
                    _logger.LogInformation($"OK {result}");
                    return Ok(result);
                }

                _logger.LogWarning($"Bad request {result}");
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, $"Something went wrong - {ex.Message}");
            }
        }
    }
}