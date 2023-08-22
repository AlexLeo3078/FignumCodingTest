using Microsoft.AspNetCore.Mvc;

namespace FignumCodingTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NumberConfigurationController : ControllerBase
    {

        private readonly ILogger<NumberConfigurationController> _logger;

        public NumberConfigurationController(ILogger<NumberConfigurationController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<List<int>>> Post(string numbers)
        {
            var result = await NumberConfigurationHelper.CheckAndSort(numbers);

            if (result != null)
            {
                return Ok(result.OrderBy(x => x).ToList());
            }

            return BadRequest($"The numbers string provided is not in a correct format {numbers} - eg: 1,2,3,4,5");
        }
    }
}