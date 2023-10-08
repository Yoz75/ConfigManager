
using System.IO;
using System;
using System.Runtime.ExceptionServices;

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
                File.Create(ConfigPath).Close();
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

        public string? GetDataFromConfig(string name)
        {


            var config = File.ReadAllText(ConfigPath);
            var parameters = config.Split('\n', '\r');

            foreach (var parameter in parameters)
            {
                if (parameter.Contains(name) && !parameter.Contains($":{name}"))
                {
                    var parameterParts = parameter.Split(':');
                    return parameterParts[1];
                }
            }
            return null;

        }

        public bool IsParameterInConfig(string name)
        {
            var config = File.ReadAllText(ConfigPath);
            var parameters = config.Split('\n', '\r');
            foreach (var parameter in parameters)
            {
                if (parameter.Contains(name) && !parameter.Contains($":{name}"))
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsDataInParameter(string name, string value)
        {
            var config = File.ReadAllText(ConfigPath);
            var parameters = config.Split('\n', '\r');
            foreach (var parameter in parameters)
            {
                if (parameter.Contains(name) && !parameter.Contains($":{name}"))
                {
                    var parameterParts = parameter.Split(':');
                    foreach (var part in parameterParts)
                    {
                        if (part == value)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}
