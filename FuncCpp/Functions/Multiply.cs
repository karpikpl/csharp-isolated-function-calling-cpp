using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using function_isolated_cpp;

namespace FuncCpp
{
    public class Multiply
    {
        private readonly ILogger<Multiply> _logger;

        public Multiply(ILogger<Multiply> logger)
        {
            _logger = logger;
        }

        [Function("Multiply")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req, int a, int b)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            try
            {
                _logger.LogInformation("Attempting to call into C++ code");

                MathLibrary mathLibrary = new MathLibrary();
                int product = mathLibrary.CallMultiplyNumbers(a, b);
                return new OkObjectResult($"The product of {a} and {b} is {product}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while calling into C++ code");
                return new BadRequestObjectResult("An error occurred while calling into C++ code: " + ex.Message);
            }
        }

    }
}
