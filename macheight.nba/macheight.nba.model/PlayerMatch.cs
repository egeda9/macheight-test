using Newtonsoft.Json;
using System.Collections.Generic;

namespace macheight.nba.model
{
    public class PlayerMatch
    {
        [JsonProperty("player1")]
        public IList<Player> Players1 { get; set; }

        [JsonProperty("player2")]
        public IList<Player> Players2 { get; set; }
    }
}
