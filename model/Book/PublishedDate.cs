using System;
using Newtonsoft.Json;

namespace cat.itb.M6UF3EA2_ManzanoMiquel.model.Book
{

    [Serializable]
    public class PublishedDate
    {
        [JsonProperty("$date")]
        public string Date { get; set; }

        public override string ToString()
        {
            return
                "PublishedDate{" +
                "$Date = '" + Date + '\'' +
                "}";
        }
    }
}
