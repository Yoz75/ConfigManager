﻿
using System.Text;
namespace ConfigManager
{
    public class TextConfigManager : IConfigManager
    {
        private string ConfigPath;
        public TextConfigManager(string configPath)
        {
            ConfigPath = configPath;
            if (!File.Exists(ConfigPath))
            {
                File.Create(ConfigPath);
                using (FileStream config = new FileStream(ConfigPath, FileMode.Append))
                {
                    config.WriteByte(0xAA); //0xAA - показатель, что конфиг байтовый,
                                            //и ByteConfigManager не сможет его прочитать
                }
            }
        }

        public void AddDataToConfig(string name, string value)
        {
            if (name.Contains(':') || value.Contains(':'))
            {
                throw new ArgumentException("Name or value contains \':\'");
            }


            var config = File.AppendText(ConfigPath);
            config.WriteLine($"{name}:{value}");
            config.Close();
        }

        public void SetDataInConfig(string name, string value)
        {
            if (name.Contains(':') || value.Contains(':'))
            {
                throw new ArgumentException("Name or value contains \':\'");
            }

            var config = File.ReadAllText(ConfigPath);
            var parameters = config.Split("\n");
            int i = 0;
            foreach (var parameter in parameters)
            {
                if (parameter.Contains($"{name}:"))
                {
                    parameters[i] = $"{name}:{value}";
                    config = string.Concat<string>(parameters);
                }
                i++;
            }
            File.WriteAllText(ConfigPath, config);
        }

        public string? GetValueFromConfig(string name)
        {


            var config = File.ReadAllText(ConfigPath);
            var parameters = config.Split('\n', '\r');

            foreach (var parameter in parameters)
            {
                if (parameter.Contains(name))
                {
                    var values = parameter.Split(':');
                    return values[1];
                }
            }
            return null;

        }
    }
}
