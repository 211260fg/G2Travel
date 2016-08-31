using Microsoft.WindowsAzure.Mobile.Service;

namespace G10TravelService.DataObjects
{
    public class Item : EntityData
    {
        public string ItemName { get; set; }

        public string ListItemId { get; set; }

        public bool ItemChecked { get; set; }

        public string CategoryId { get; set; }

        public int Amount { get; set; }

        public string Type { get; set; }
    }
}