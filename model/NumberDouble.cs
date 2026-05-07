using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace cat.itb.M6UF3EA2_ManzanoMiquel.model
{
    public class NumberDouble
    {
        [JsonProperty("$numberDouble")]
        public double _NumberDouble { get; set; }
    }
}
