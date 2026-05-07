using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace cat.itb.M6UF3EA2_ManzanoMiquel.model.Restaurant
{
    [Serializable]
    public class Restaurant
    {
        [JsonProperty("restaurant_id")]
        public string RestaurantId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("cuisine")]
        public string Cuisine { get; set; }

        [JsonProperty("borough")]
        public string Borough { get; set; }

        [JsonProperty("address")]
        public RestaurantAddress Address { get; set; }

        [JsonProperty("grades")]
        public List<RestaurantGrade> Grades { get; set; }
    }
}