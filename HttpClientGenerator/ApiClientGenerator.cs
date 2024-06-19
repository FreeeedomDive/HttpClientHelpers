using Xdd.HttpHelpers.HttpClientGenerator.ApiAnalyzer;
using Xdd.HttpHelpers.HttpClientGenerator.CodeGenerator;
using Xdd.HttpHelpers.HttpClientGenerator.FilesCreator;
using Xdd.HttpHelpers.HttpClientGenerator.Options;

namespace Xdd.HttpHelpers.HttpClientGenerator;

public static class ApiClientGenerator
{
    public static void Generate<TController>(Action<GeneratorOptions> configureOptions)
    {
        var apiProjectName = typeof(TController).Assembly.GetName().Name ?? string.Empty;
        var options = CreateDefaultOptions(apiProjectName);
        configureOptions(options);

        IControllerMethodsExtractor controllerMethodsExtractor = new ControllerMethodsExtractor();
        IControllersExtractor controllersExtractor = new ControllersExtractor(controllerMethodsExtractor);
        var controllersInfo = controllersExtractor.ExtractAllFromType<TController>();
        if (controllersInfo.Length == 0)
        {
            Console.WriteLine("No controllers were detected");
            return;
        }

        ICommonInterfaceGenerator commonInterfaceGenerator = new CommonInterfaceGenerator();
        var commonInterfaceFileContent = commonInterfaceGenerator.Generate(controllersInfo, options);

        ICommonClientGenerator commonClientGenerator = new CommonClientGenerator();
        var commonClientFileContent = commonClientGenerator.Generate(controllersInfo, options);

        IInterfaceGenerator interfaceGenerator = new InterfaceGenerator();
        IClientGenerator clientGenerator = new ClientGenerator();
        var clientsFilesContent = controllersInfo.SelectMany(
            x => new[]
            {
                interfaceGenerator.Generate(x, options),
                clientGenerator.Generate(x, options),
            }
        ).ToArray();

        IFilesWriter filesWriter = new FilesWriter();
        filesWriter.WriteFiles(options.ProjectPath, clientsFilesContent.Concat(new[] { commonInterfaceFileContent, commonClientFileContent }).ToArray());

        Console.WriteLine($"Generated {controllersInfo.Length} clients in namespace {options.ClientNamespace}");
        Console.WriteLine("Make sure to install following NuGet packages to your project with generated clients: RestSharp, Xdd.HttpHelpers.HttpClientGenerator");
    }

    private static GeneratorOptions CreateDefaultOptions(string apiProjectName)
    {
        return new GeneratorOptions
        {
            ProjectPath = ".",
            ClientNamespace = $"{apiProjectName}.Client",
            ClientType = ClientType.RestSharp,
        };
    }
}