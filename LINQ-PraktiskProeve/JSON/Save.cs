namespace LINQ_PraktiskProeve.JSON;

public class Save
{
    public static void SaveJsonToFile(string json)
    {
        string folderPath = @"C:\Users\jonas\Desktop\Skole\H3\LINQ-PraktiskProeve\LINQ-PraktiskProeve";
        string filePath = Path.Combine(folderPath, "weather_data.json");

        try
        {
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            File.WriteAllText(filePath, json.ToString());
            Console.WriteLine($"JSON gemt i: {filePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fejl ved gemning af JSON: {ex.Message}");
        }
    }
}