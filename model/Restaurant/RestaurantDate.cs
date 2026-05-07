using Newtonsoft.Json;

namespace cat.itb.M6UF3EA2_ManzanoMiquel.model.Restaurant
{
    [Serializable]
    public class RestaurantDate
    {
        [JsonProperty("$date")]
        public long Timestamp { get; set; }
    }
}
