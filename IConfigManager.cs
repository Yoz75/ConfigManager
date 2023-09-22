

namespace ConfigManager
{
    internal interface IConfigManager
    {
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
        public string? GetValueFromConfig(string name);

    }
}
