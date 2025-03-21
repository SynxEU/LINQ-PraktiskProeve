using System;
using System.IO;

namespace LINQ_PraktiskProeve.JSON;

public class Save
{
    /// <summary>
    /// Gemmer en JSON-streng til en fil i et specifikt bibliotek.
    /// </summary>
    /// <param name="json">Den JSON-streng, der skal gemmes til filen.</param>
    public static void SaveJsonToFile(string json)
    {
        // Angiver mappen, hvor filen skal gemmes, ved at kombinere brugerens profilmappe med et specifikt projektmappestier
        string folderPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
            "RiderProjects",
            "LINQ-PraktiskProeve",
            "LINQ-PraktiskProeve"
        );

        // Angiver filnavnet og stien til JSON-filen
        string filePath = Path.Combine(folderPath, "weather_data.json");

        try
        {
            // Hvis mappen ikke eksisterer, oprettes den
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // Skriver JSON-strengen til filen
            File.WriteAllText(filePath, json);
        }
        catch (Exception ex)
        {
            // Hvis en fejl opstår under skrivning, udskrives fejlmeddelelsen
            Console.WriteLine($"Fejl mens JSON blev gemt: {ex.Message}");
        }
    }

}