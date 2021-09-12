using Newtonsoft.Json;
using System.Collections.Generic;

namespace macheight.nba.model
{
    public class Players
    {
        [JsonProperty("values")]
        public IList<Player> Values {  get; set; }
    }

    public class PlayerHeight
    {
        [JsonProperty("height")]
        public string Height { get; set; }

        [JsonProperty("players")]
        public IList<Player> Players { get; set; }
    }
}
