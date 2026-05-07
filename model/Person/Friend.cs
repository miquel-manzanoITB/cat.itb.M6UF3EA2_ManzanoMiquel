using System;
using Newtonsoft.Json;

namespace cat.itb.M6UF3EA2_ManzanoMiquel.model.Person

{
    [Serializable]
    public class Friend
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}

