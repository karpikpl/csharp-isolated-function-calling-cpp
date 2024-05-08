using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using function_isolated_cpp;

namespace FuncCpp
{
    public class Add
    {
        private readonly ILogger<Add> _logger;

        public Add(ILogger<Add> logger)
        {
            _logger = logger;
        }

        [Function("Add")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req, int num1, int num2)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            try
            {
                _logger.LogInformation("Attempting to call into C++ code");

                MathLibrary mathLibrary = new MathLibrary();
                int sum = mathLibrary.CallAddNumbers(num1, num2);
                return new OkObjectResult($"The sum of {num1} and {num2} is {sum}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while calling into C++ code");
                return new BadRequestObjectResult("An error occurred while calling into C++ code: " + ex.Message);
            }
        }
    }
}
