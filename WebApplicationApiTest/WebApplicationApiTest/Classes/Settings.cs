using System;
using System.IO;
using Newtonsoft.Json;

namespace WebApplicationApiTest
{
    public struct SettingsModel
    {
        public string Uris;
        public string Password;
    }

    public static class Settings
    {
        private static SettingsModel FromFile()
        {
            return JsonConvert
                    .DeserializeObject<SettingsModel>(
                        File.ReadAllText(
                            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json")
                            ));
        } 

        public static string DbFile
        {
            get
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "lite.db");
            }
        }

        public static string Uris
        {
            get
            {
                SettingsModel settings = FromFile();
                return settings.Uris;
            }
        }

        public static string Pwd
        {
            get
            {
                SettingsModel settings = FromFile();
                return settings.Password;
            }
        }
    }
}
