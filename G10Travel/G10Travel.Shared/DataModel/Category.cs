using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace G10Travel.DataModel
{
    public class Category
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        public override string ToString()
        {
            return System.String.Format(Name);
        }

    }
}
