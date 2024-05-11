using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using function_isolated_cpp;

namespace FuncCpp
{
    public class LoadTest
    {
        private readonly ILogger<LoadTest> _logger;

        public LoadTest(ILogger<LoadTest> logger)
        {
            _logger = logger;
        }

        [Function("LoadTest")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            try
            {
                _logger.LogInformation("Attempting to call into C++ code");

                MathLibrary mathLibrary = new MathLibrary();
                mathLibrary.LoadLibraryUsingKernel();
                return new OkObjectResult($"OK");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while calling into C++ code");
                return new BadRequestObjectResult("An error occurred while calling into C++ code: " + ex.Message);

            }
        }
    }
}
