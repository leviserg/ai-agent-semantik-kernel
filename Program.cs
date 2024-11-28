using Microsoft.SemanticKernel;
using Newtonsoft.Json;

string filePath = @"../../../../azure-open-ai-settings.json";

try
{
    string? jsonContent = File.ReadAllText(filePath);

    Dictionary<string, string> config = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonContent);

    if(config == null)
    {
        throw new FileNotFoundException("Error parsing config");
    }

    string apikey = config["primarykey"];
    string endpoint = config["endpoint"];
    string modelId = config["modelId"];
    string deploymentName = config["deploymentName"];

    var builder = Kernel.CreateBuilder();
    builder.AddAzureOpenAIChatCompletion(
        deploymentName: deploymentName,
        endpoint: endpoint,
        apiKey: apikey,
        modelId: modelId // optional
    );
    var kernel = builder.Build();

    var result = await kernel.InvokePromptAsync(
        "Give me a list of breakfast foods with eggs and cheese");
    Console.WriteLine(result);

}
catch(FileNotFoundException ex)
{
    Console.WriteLine("No configuration file found : " + ex.Message);
}


// See https://aka.ms/new-console-template for more information
Console.ReadLine();
