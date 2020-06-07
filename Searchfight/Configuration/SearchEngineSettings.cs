using System.Collections.Generic;

namespace Searchfight.Configuration
{
    public class SearchEngineSettings
    {
        public string Name { get; set; }
        public string BaseUri { get; set; }
        public IEnumerable<Parameter> Parameters { get; set; }
        public IEnumerable<Header> Headers { get; set; }
    }
}