using Xdd.HttpHelpers.HttpClientGenerator.ApiAnalyzer;
using Xdd.HttpHelpers.HttpClientGenerator.CodeGenerator;
using Xdd.HttpHelpers.HttpClientGenerator.FilesCreator;

namespace HttpClientGenerator;

public static class ApiClientGenerator
{
    public static void Generate<TController>(string projectPath)
    {
        var controllersInfo = ControllersExtractor.ExtractAllFromType<TController>();
        if (controllersInfo.Length == 0)
        {
            Console.WriteLine("No controllers were detected");
            return;
        }

        var commonInterfaceFileContent = CommonInterfaceGenerator.Generate(controllersInfo);
        var commonClientFileContent = CommonClientGenerator.Generate(controllersInfo);
        var extensionsFileContent = ExtensionsFileGenerator.GenerateRestResponseExtensions(controllersInfo.First());
        var clientsFilesContent = controllersInfo.SelectMany(
            x => new[]
            {
                InterfaceGenerator.Generate(x),
                ClientGenerator.Generate(x),
            }
        ).ToArray();

        FilesWriter.WriteFiles(projectPath, clientsFilesContent.Concat(new []{commonInterfaceFileContent, commonClientFileContent, extensionsFileContent}).ToArray());
        Console.WriteLine($"Generated {controllersInfo.Length} clients for project {controllersInfo.First().Namespace}");
        Console.WriteLine("Make sure to install following NuGet packages to your project with generated clients: RestSharp, Xdd.HttpHelpers.HttpClientGenerator");
    }
}
