using System.Collections.Generic;
using Newtonsoft.Json;

namespace GameFrameX.Editor
{
    public sealed class PackagesManifest
    {
        [JsonProperty("dependencies")] public Dictionary<string, string> Dependencies = new Dictionary<string, string>();
        
        [JsonProperty("scopedRegistries")] public List<ScopedRegistry> ScopedRegistries = new List<ScopedRegistry>();

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
    
    public sealed class ScopedRegistry
    {
        [JsonProperty("name")] public string Name { get; set; }
        
        [JsonProperty("url")] public string Url { get; set; }
        
        [JsonProperty("scopes")] public List<string> Scopes = new List<string>();
    }
}