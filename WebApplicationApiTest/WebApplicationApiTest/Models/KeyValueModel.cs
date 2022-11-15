using Newtonsoft.Json;

namespace WebApplicationApiTest.Models
{
    public class KeyValueModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
