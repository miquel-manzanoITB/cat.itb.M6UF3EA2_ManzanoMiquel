using System;
using Newtonsoft.Json;

namespace cat.itb.M6UF3EA2_ManzanoMiquel.model
{
    [Serializable]
    public class Student
    {
        [JsonProperty("firstname")]
        public string Firstname { get; set; }

        [JsonProperty("lastname1")]
        public string Lastname1 { get; set; }

        [JsonProperty("lastname2")]
        public string Lastname2 { get; set; }

        [JsonProperty("dni")]
        public string Dni { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("phone_aux")]
        public string PhoneAux { get; set; }

        [JsonProperty("birth_year")]
        public int BirthYear { get; set; }
    }
}