using Newtonsoft.Json;
using System.Collections.Generic;

namespace G10Travel.DataModel
{
    public class Item
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "itemname")]
        public string ItemName { get; set; }

        [JsonProperty(PropertyName = "listitemid")]
        public string ListItemId { get; set; }

        [JsonProperty(PropertyName = "itemchecked")]
        public bool ItemChecked { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "categoryid")]
        public string CategoryId { get; set; }

        public override string ToString()
        {
            return System.String.Format(ItemName);
        }
    }
}