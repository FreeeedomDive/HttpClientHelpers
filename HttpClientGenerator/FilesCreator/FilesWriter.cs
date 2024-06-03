using Xdd.HttpHelpers.HttpClientGenerator.CodeGenerator;

namespace Xdd.HttpHelpers.HttpClientGenerator.FilesCreator;

public static class FilesWriter
{
    public static void WriteFiles(string projectPath, GeneratedFileContent[] fileContents)
    {
        foreach (var fileContent in fileContents)
        {
            var directoryPath = Path.Join(projectPath, fileContent.FolderName);
            var directory = new DirectoryInfo(directoryPath);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            var fileName = Path.Join(directoryPath, fileContent.FileName);
            File.WriteAllText(fileName, fileContent.FileContent);
        }
    }
}