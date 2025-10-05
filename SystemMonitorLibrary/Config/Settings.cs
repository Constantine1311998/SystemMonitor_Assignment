using System.Text.Json;

namespace SystemMonitorLibrary.Config
{
    public class AppSettings
    {
        public int ScanInterval { get; set; } = 2000;         // Default: 2 sec
        public int DecimalPrecision { get; set; } = 2;      // Default: 2 decimal places
    }
    public class SettingsManager
    {
        private const string ConfigFilePath = @"Config\appsettings.config";

        private static AppSettings instace;
        public static AppSettings Settings
        {
            get
            {
                if (instace == null)
                {
                    instace = LoadSettings();
                }
                return instace;
            }
        }
        public static AppSettings LoadSettings()
        {
            if (!File.Exists(ConfigFilePath))
            {
                var defaultSettings = new AppSettings();
                SaveSettings(defaultSettings);
                return defaultSettings;
            }

            string json = File.ReadAllText(ConfigFilePath);
            return JsonSerializer.Deserialize<AppSettings>(json);
        }

        public static void SaveSettings(AppSettings settings)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(settings, options);
            Directory.CreateDirectory(Path.GetDirectoryName(ConfigFilePath));
            File.WriteAllText(ConfigFilePath, json);
        }
    }
}
