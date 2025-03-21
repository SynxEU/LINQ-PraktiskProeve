using System;
using System.IO;

namespace LINQ_PraktiskProeve.JSON;

public class Save
{
    public static void SaveJsonToFile(string json)
    {
        string folderPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
            "RiderProjects",
            "LINQ-PraktiskProeve",
            "LINQ-PraktiskProeve"
        );
        string filePath = Path.Combine(folderPath, "weather_data.json");

        try
        {
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            File.WriteAllText(filePath, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fejl mens JSON blev gemt : {ex.Message}");
        }
    }
}