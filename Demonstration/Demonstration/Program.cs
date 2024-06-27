using Demonstration.Api.Controllers;
using Xdd.HttpHelpers.HttpClientGenerator;

ApiClientGenerator.Generate<UsersController>(
    options => options.ProjectPath = Path.Join("..", "..", "..", "..", "Demonstration.Api.Client")
);