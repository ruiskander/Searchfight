using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Searchfight.Competition;
using Searchfight.Competition.Specific;
using Searchfight.Configuration;
using Searchfight.Configuration.Specific;
using Searchfight.Configuration.Specific.Json;
using Searchfight.SearchTotalResult;
using Searchfight.SearchTotalResult.Specific;
using Searchfight.SearchTotalResult.Specific.Bing;
using Searchfight.SearchTotalResult.Specific.Google;
using Searchfight.Specific;
using Searchfight.WebSearch;
using Searchfight.WebSearch.Configuration;

namespace Searchfight.Utils
{
    public static class ServiceProvider
    {
        public static IServiceProvider Provider
        {
            get
            {
                IServiceCollection serviceCollection = new ServiceCollection();
                ConfigureServices(serviceCollection);
                return serviceCollection.BuildServiceProvider();
            }
        }

        static void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<ISearchEnginesConfiguration, SearchEnginesConfiguration>();
            serviceCollection.AddSingleton<ISearchEngineClient, WebSearchEngineClient>();
            serviceCollection.AddSingleton(provider => provider.GetWebSearchEngines());
            serviceCollection.AddSingleton<ICommandLineParser, CommandLineParser>();
            serviceCollection.AddSingleton<ICompetitionResult, CompetitionResult>();
            serviceCollection.AddSingleton<IWriteOutResult, WriteOutResult>();
            serviceCollection.AddSingleton<ICompetition, Competition.Specific.Competition>();
        }

        private static T GetService<T>(this IServiceProvider provider, WebSearchEngines webSearchEngine)
            where T : class
        {
            if (provider == null)
                throw new ArgumentNullException(nameof(provider));

            if (typeof(T) == typeof(ISearchEngineConfiguration))
                return (T)(object)(new SearchEngineConfiguration(webSearchEngine, provider.GetService<ISearchEnginesConfiguration>()));

            if (typeof(T) == typeof(ISearchResponseParser))
            {
                return webSearchEngine switch
                {
                    WebSearchEngines.Google => (T)(object)(new GoogleSearchResponseParser()),
                    WebSearchEngines.Bing => (T)(object)(new BingSearchResponseParser()),
                    _ => throw new ArgumentOutOfRangeException(nameof(webSearchEngine), webSearchEngine, null)
                };
            }

            throw new NotSupportedException();
        }

        private static IDictionary<WebSearchEngines, IWebSearchTotalResults> GetWebSearchEngines(this IServiceProvider provider)
        {
            var searchEngineClient = provider.GetService<ISearchEngineClient>();

            IDictionary<WebSearchEngines, IWebSearchTotalResults> engines = new Dictionary<WebSearchEngines, IWebSearchTotalResults>();
            foreach (WebSearchEngines engine in (WebSearchEngines[])Enum.GetValues(typeof(WebSearchEngines)))
            {
                var searchEngineConfiguration = provider.GetService<ISearchEngineConfiguration>(engine);
                var responseParser = provider.GetService<ISearchResponseParser>(engine);
                engines.Add(engine, new WebSearchTotalResults(searchEngineConfiguration, searchEngineClient, responseParser));
            }

            return engines;
        }
    }
}
