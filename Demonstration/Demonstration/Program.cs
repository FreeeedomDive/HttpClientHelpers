using Demonstration.Api.Controllers;
using Xdd.HttpHelpers.HttpClientGenerator;
using Xdd.HttpHelpers.HttpClientGenerator.Options;

ApiClientGenerator.Generate<UsersController>(
    options =>
    {
        options.ClientNamespace = "Demonstration.Api.Client.RestSharpExample";
        options.ClientName = "DemonstrationApiRestSharpClient";
        options.ProjectPath = Path.Join("..", "..", "..", "..", "Demonstration.Api.Client.RestSharpExample");
        options.ClientType = ClientType.RestSharp; // by default
    }
);