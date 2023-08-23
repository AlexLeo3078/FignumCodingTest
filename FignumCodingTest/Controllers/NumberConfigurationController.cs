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
            var result = await NumberConfigurationHelper.RemovePrimeNumbersAndSort(request.InputStringNumber);

            if (result.CheckAndSortedList != null)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}