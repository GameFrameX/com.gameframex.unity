using System.Collections.Generic;
using System.Xml;
using GameFrameX.LitJSON.Runtime;

namespace GameFrameX.Editor
{
    public sealed class PackagesManifest
    {
        [JsonProperty("dependencies")] public Dictionary<string, string> Dependencies = new Dictionary<string, string>();

        [JsonProperty("scopedRegistries")] public List<ScopedRegistry> ScopedRegistries = new List<ScopedRegistry>();

        public string ToString(bool indented)
        {
            return LitJSON.Runtime.JsonMapper.ToJson(this, indented);
        }
    }

    public sealed class ScopedRegistry
    {
        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("url")] public string Url { get; set; }

        [JsonProperty("scopes")] public List<string> Scopes = new List<string>();
    }
}