using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FuncCpp
{
    public class PathFunc
    {
        private readonly ILogger<PathFunc> _logger;

        public PathFunc(ILogger<PathFunc> logger)
        {
            _logger = logger;
        }

        [Function("Path")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            var path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var directory = Path.GetDirectoryName(path);
            return new OkObjectResult($"Path is {directory}");
        }
    }
}
