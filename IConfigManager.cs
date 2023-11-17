

using System;
using System.Collections;
namespace ConfigManager
{
    public interface IConfigManager
    {

        public string ParameterSplitter
        {
            get;
            set;
        }

        /// <summary>
        ///  Creates new parameter in config
        /// </summary>
        /// <param name="name">parameter name</param>
        /// <param name="value">parameter value</param>
        public void AddDataToConfig(string name, string value);

        /// <summary>
        ///  Edit EXISTING parameter in config
        /// </summary>
        /// <param name="name">parameter name</param>
        /// <param name="value">parameter value</param>
        public void SetDataInConfig(string name, string value);

        /// <summary>
        /// Gets value from config by name
        /// </summary>
        /// <param name="name"> parameter name</param>
        /// <returns>null if parameter is not exists</returns>
        public string? GetDataFromConfig(string name);

        /// <summary>
        /// Creates new parameter in config that contains array
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void AddArrayDataToConfig(string name, IList value);

        public IList GetArrayFromConfig(string name);

        /// <summary>
        /// Tries get parameter in config.
        /// </summary>
        /// <param name="name"> parameter name</param>
        /// <returns>Returns true if parameter defined in config
        /// and false if not</returns>
        public bool IsParameterInConfig(string name);

        /// <summary>
        /// Tries get parameter with value 
        /// </summary>
        /// <param name="name"> parameter name</param>
        /// <param name="value"> parameter data</param>
        /// <returns>Returns true if parameter contains value</returns>
        public bool IsDataInParameter(string name, string value);

    }
}
