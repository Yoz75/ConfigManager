
using System;
using System.Collections;
using System.IO;


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

            if (File.ReadAllText(ConfigPath).Contains($"{name}{ParameterSplitter}{value}"))
            {
                throw new Exception($"{name} already exists!");
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
        public void AddCollectionDataToConfig(string name, IList value)
        {
            for (int i = 0; i < value.Count; i++)
            {
                if (value[i] is string collectionElement)
                {
                    if (collectionElement.Contains(ParameterSplitter))
                    {
                        throw new ArgumentException($"Name or value contains \"{ParameterSplitter}\"");
                    }
                }
                else if (name.Contains(ParameterSplitter))
                {
                    throw new ArgumentException($"Name or value contains \"{ParameterSplitter}\"");
                }
            }
            var config = File.AppendText(ConfigPath);
            string allValues = "[";
            for (int i = 0; i < value.Count; i++)
            {
                if (i != value.Count - 1)
                {
                    allValues += $"\"{value[i].ToString()}\",";
                }
                else
                {
                    allValues += $"\"{value[i]}\"]";
                }
            }
            config.WriteLine($"{name}:{allValues}");
            config.Close();
        }

        public IList GetCollectionFromConfig(string name)
        {
            var config = File.ReadAllText(ConfigPath);
            var parameters = config.Split('\n', '\r');

            string[] arrayParts;
            foreach (var parameter in parameters)
            {
                if (parameter.Contains(name) && !parameter.Contains($"{ParameterSplitter}{name}"))
                {
                    var parameterParts = parameter.Split(ParameterSplitter);
                    foreach (var item in parameterParts)
                    {
                        if (item.Contains("[\""))
                        {
                            arrayParts = item.Split(',');
                            string arrayPartsString = CollectionToString(arrayParts);
                            return new string[]
                            {
                                CutCharInString(arrayPartsString,'\"')
                            }
                            ;
                        }
                    }
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
        private string CollectionToString(IList collection)
        {
            string result = string.Empty;
            foreach (var item in collection)
            {
                result += " " + item;
            }
            return result;
        }

        private string CutCharInString(string original, char charToCut)
        {
            string[] originalSplitted = new string[1];
            foreach (var item in original)
            {
                originalSplitted = original.Split(charToCut);
            }
            return CollectionToString(originalSplitted);
        }
    }
}
