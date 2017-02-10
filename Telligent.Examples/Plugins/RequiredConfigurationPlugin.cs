using Telligent.DynamicConfiguration.Components;
using Telligent.Evolution.Extensibility.Version1;

namespace ConfigurablePlugins
{
    public class RequiredConfigurationPlugin : IRequiredConfigurationPlugin
    {
        public string Name => "Demo Required Configuration Plugin";

        public string Description => "Shows how to create a plugin with required configuration.";

        // Field used storing plugin configuration for easy access to properties.
        public static IPluginConfiguration Configuration;

        // Declares the properties that can be configured.
        public PropertyGroup[] ConfigurationOptions
        {
            get
            {
                var groups = new[] { new PropertyGroup("options", "Options", 0) };

                groups[0].Properties.Add(new Property("instrumentationKey", "Instrumentation Key", PropertyType.String, 0, string.Empty));
                groups[0].Properties.Add(new Property("logEvents", "Log Events", PropertyType.Bool, 1, false.ToString()));

                return groups;
            }
        }

        public bool IsConfigured => string.IsNullOrEmpty(Configuration.GetString("instrumentationKey"));

        // Called when the plugin is saved.
        public void Update(IPluginConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void Initialize()
        {
            // Load the values from the configuration
            var instrumentationKey = Configuration.GetString("instrumentationKey");
            var logEvents = Configuration.GetBool("logEvents");
        }
    }
}
