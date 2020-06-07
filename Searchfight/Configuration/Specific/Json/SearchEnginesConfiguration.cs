using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace Searchfight.Configuration.Specific.Json
{
    public class SearchEnginesConfiguration: ISearchEnginesConfiguration
    {
        private static IEnumerable<SearchEngineSettings> _settings;
        public IEnumerable<SearchEngineSettings> GetSearchEngineSettings()
        {
            if (_settings != null)
                return _settings;

            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            _settings = configuration.GetSection("searchEngines").Get<IEnumerable<SearchEngineSettings>>();

            if (_settings == null)
                throw new SearchEngineSettingsNotFoundException();

            return _settings;
        }
    }
}
