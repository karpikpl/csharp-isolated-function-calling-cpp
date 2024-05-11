using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

var currentPath = System.Environment.GetEnvironmentVariable("PATH");
var functionDll = System.Reflection.Assembly.GetExecutingAssembly().Location;
var functionDirectory = Path.GetDirectoryName(functionDll);
Console.WriteLine($"Current directory: {functionDirectory}");
System.Environment.SetEnvironmentVariable("PATH", $"{functionDirectory};{currentPath}");

Console.WriteLine("Starting Function App");
Console.WriteLine($"Updated PATH: {System.Environment.GetEnvironmentVariable("PATH")}");

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
    })
    .Build();

host.Run();
