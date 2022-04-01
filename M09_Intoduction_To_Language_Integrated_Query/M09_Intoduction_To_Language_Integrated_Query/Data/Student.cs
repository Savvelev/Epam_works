using Newtonsoft.Json;
using System;


namespace M09_Intoduction_To_Language_Integrated_Query
{
    public class Student
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("test")]
        public string Test { get; set; }

        
        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("mark")]
        public int Mark { get; set; }
    }
}

