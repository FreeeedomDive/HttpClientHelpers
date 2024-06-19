using Xdd.HttpHelpers.HttpClientGenerator.CodeGenerator;

namespace Xdd.HttpHelpers.HttpClientGenerator.FilesCreator;

internal class FilesWriter : IFilesWriter
{
    public void WriteFiles(string projectPath, GeneratedFileContent[] fileContents)
    {
        foreach (var fileContent in fileContents)
        {
            var directoryPath = Path.Join(projectPath, fileContent.FolderName);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            var fileName = Path.Join(directoryPath, fileContent.FileName);
            File.WriteAllText(fileName, fileContent.FileContent);
        }
    }
}