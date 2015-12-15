using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace G10Travel
{
    public class TodoItem
    {
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "complete")]
        public bool Complete { get; set; }
    }
}
