using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace cat.itb.M6UF3EA2_ManzanoMiquel.model.Restaurant
{
    [Serializable]
    public class RestaurantGrade
    {
        [JsonProperty("date")]
        public RestaurantDate Date { get; set; }

        [JsonProperty("grade")]
        public string Grade { get; set; }

        [JsonProperty("score")]
        public int Score { get; set; }
    }
}
