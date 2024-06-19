using Xdd.HttpHelpers.HttpClientGenerator.CodeGenerator;

namespace Xdd.HttpHelpers.HttpClientGenerator.FilesCreator;

internal interface IFilesWriter
{
    void WriteFiles(string projectPath, GeneratedFileContent[] fileContents);
}