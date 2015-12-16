using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace G10Travel.DataModel
{
    public class ListItem
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "location")]
        public string Location { get; set; }

        [JsonProperty(PropertyName = "startdate")]
        public string startDate { get; set; }

        [JsonProperty(PropertyName = "enddate")]
        public string endDate { get; set; }

        [JsonProperty(PropertyName = "itemstobring")]
        public List<string> itemsToBring { get; set; }
    }
}
