using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace cat.itb.M6UF3EA2_ManzanoMiquel.model.Grade
{
    [Serializable]
    public class GradeScore
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("score")]
        public NumberDouble Score { get; set; }
    }
}
