
using System.IO;
using System;


namespace ConfigManager
{
    public class TextConfigManager : IConfigManager
    {

        private readonly string ParameterSplitter_;
        public string ParameterSplitter
        { 
            get
            {
                return ParameterSplitter_;
            }
            set
            {
                throw new ArgumentException("ParameterSplitter is readonly, but you trying to put here value!");
            }
        }

        private string ConfigPath;
        public TextConfigManager(string configPath, string splitter = ":")
        {
        ParameterSplitter_ = splitter;
            ConfigPath = configPath;
            if (!File.Exists(ConfigPath))
            {
                File.Create(ConfigPath).Close();
            }
        }

        public void AddDataToConfig(string name, string value)
        {
            if (name.Contains(ParameterSplitter) || value.Contains(ParameterSplitter))
            {
                throw new ArgumentException($"Name or value contains \"{ParameterSplitter}\"");
            }


            var config = File.AppendText(ConfigPath);
            config.WriteLine($"{name}{ParameterSplitter}{value}");
            config.Close();
        }

        public void SetDataInConfig(string name, string value)
        {
            if (name.Contains(ParameterSplitter) || value.Contains(ParameterSplitter))
            {
                throw new ArgumentException($"Name or value contains \'{ParameterSplitter}\'");
            }

            var config = File.ReadAllText(ConfigPath);
            var parameters = config.Split("\n");
            int i = 0;
            foreach (var parameter in parameters)
            {
                if (parameter.Contains($"{name}{ParameterSplitter}"))
                {
                    parameters[i] = $"{name}{ParameterSplitter}{value}";
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
                if (parameter.Contains(name) && !parameter.Contains($"{ParameterSplitter}{name}"))
                {
                    var parameterParts = parameter.Split(ParameterSplitter);
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
                if (parameter.Contains(name) && !parameter.Contains($"{ParameterSplitter}{name}"))
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
                if (parameter.Contains(name) && !parameter.Contains($"{ParameterSplitter}{name}"))
                {
                    var parameterParts = parameter.Split(ParameterSplitter);
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
