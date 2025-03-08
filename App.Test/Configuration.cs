using Shared.Libraries;
using System.IO;
using System;
using System.Text.Json;

namespace App.Test;

public class Configuration
{
    private static DirectoryManager directoryManager = new DirectoryManager();

    private static string ReadContentJson(string[] paths, int currentIndex, string content)
    {
        var currentPath = paths.Length == currentIndex ? null : paths[currentIndex];

        if (currentPath is null)
            return content;

        var json = JsonSerializer.Deserialize<Dictionary<string, object>>(content);

        if (!json.ContainsKey(currentPath))
            return string.Empty;

        return ReadContentJson(paths, (currentIndex + 1), JsonSerializer.Serialize(json.GetValueOrDefault(currentPath)));
    }

    private static string GetValue(params string[] paths)
    {
        if (!directoryManager.HasFile("appsettings.json"))
            throw new Exception("'appsettings.json' not exists");

        return ReadContentJson(paths, 0, directoryManager.ReadFile("appsettings.json"));
    }

    public static string AuthenticationServiceBaseUrl
    {
        get
        {
#if DEBUG
            return GetValue("services", "authentication");
#elif RELEASE
            return Environment.GetEnvironmentVariable("AUTHENTICATION_SERVICE_BASE_URL");
#endif
        }
    }
}
