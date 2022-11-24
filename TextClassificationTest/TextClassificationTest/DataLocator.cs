using System.Net;

namespace TextClassificationTest;

public static class DataLocator
{
    public static string EnsureDataSetDownloaded(string fileName)
    {

        // This is the path if the repo has been checked out.
        var filePath = Path.Combine(Directory.GetCurrentDirectory(),"data", fileName);

        if (!File.Exists(filePath))
        {
            // This is the path if the file has already been downloaded.
            filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
        }

        if (!File.Exists(filePath))
        {
            using (var client = new WebClient())
            {
                client.DownloadFile($"https://raw.githubusercontent.com/dotnet/csharp-notebooks/main/machine-learning/data/{fileName}", filePath);
            }
            Console.WriteLine($"Downloaded {fileName}  to : {filePath}");
        }
        else
        {
            Console.WriteLine($"{fileName} found here: {filePath}");
        }

        return filePath;
    }
}