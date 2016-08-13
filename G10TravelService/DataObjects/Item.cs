using Microsoft.WindowsAzure.Mobile.Service;

namespace G10TravelService.DataObjects
{
    public class Item : EntityData
    {
        public string ItemName { get; set; }

        public string ListItemId { get; set; }
    }
}