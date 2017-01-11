using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace LinkedInConnectionBump
{
    public class Config
    {

        public static string LinkedInUserName
        {
            get { return ConfigurationManager.AppSettings["LinkedInUserName"]; }
        }
        public static string LinkedInPassword
        {
            get { return ConfigurationManager.AppSettings["LinkedInPassword"]; }
        }

        public static int AddConnectionLimit
        {
            get { return int.Parse(ConfigurationManager.AppSettings["AddConnectionLimit"]); }
        }
        public static int AddedConnections
        {
            get { return int.Parse(ConfigurationManager.AppSettings["AddedConnections"]); }
        }
        public static int ConnectionBatchSize
        {
            get { return int.Parse(ConfigurationManager.AppSettings["ConnectionBatchSize"]); }
        }
        
        
      
        public static void AddUpdateAppSettings(string key, string value)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
                //ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error writing app settings");
                throw;
            }
        }


    }
}
