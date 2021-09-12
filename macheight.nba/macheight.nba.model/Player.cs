using Newtonsoft.Json;

namespace macheight.nba.model
{
    public class Player
    {
        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("h_in")]
        public string HeightInches { get; set; }

        [JsonProperty("h_meters")]
        public string HeightMeters { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }
    }
}
