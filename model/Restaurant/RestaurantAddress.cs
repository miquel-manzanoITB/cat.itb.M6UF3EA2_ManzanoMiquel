using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace cat.itb.M6UF3EA2_ManzanoMiquel.model.Restaurant
{
    [Serializable]
    public class RestaurantAddress
    {
        [JsonProperty("building")]
        public string Building { get; set; }

        [JsonProperty("coord")]
        public List<double> Coord { get; set; }

        [JsonProperty("street")]
        public string Street { get; set; }

        [JsonProperty("zipcode")]
        public string Zipcode { get; set; }
    }
}
