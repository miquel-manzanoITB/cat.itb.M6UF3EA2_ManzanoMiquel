using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace cat.itb.M6UF3EA2_ManzanoMiquel.model.Grade
{
    public class GradeId
    {
        [JsonProperty("$oid")]
        public string Oid { get; set; }
    }
}
