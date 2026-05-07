using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace cat.itb.M6UF3EA2_ManzanoMiquel.model.Grade
{
    [Serializable]
    public class Grade
    {
        [JsonProperty("_id")]
        public GradeId Id { get; set; }
        [JsonProperty("student_id")]
        public NumberInt StudentId { get; set; }
        [JsonProperty("class_id")]
        public NumberInt ClassId { get; set; }

        [JsonProperty("scores")]
        public List<GradeScore> Scores { get; set; }
    }
}