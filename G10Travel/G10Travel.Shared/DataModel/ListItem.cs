using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace G10Travel.DataModel
{
    class ListItem
    {
        public string Id { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "location")]
        public string Location { get; set; }
        [JsonProperty(PropertyName = "startdate")]
        public DateTime startDate { get; set; }
        [JsonProperty(PropertyName = "enddate")]
        public DateTime endDate { get; set; }
        [JsonProperty(PropertyName = "itemstobring")]
        public List<TodoItem> itemsToBring { get; set; }
    }
}
