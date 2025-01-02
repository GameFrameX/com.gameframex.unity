using System.Collections.Generic;
using Newtonsoft.Json;

namespace GameFrameX.Editor
{
    public sealed class PackagesManifest
    {
        [JsonProperty("dependencies")] public Dictionary<string, string> Dependencies = new Dictionary<string, string>();

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}