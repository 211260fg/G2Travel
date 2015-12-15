using Microsoft.WindowsAzure.Mobile.Service;

namespace G10TravelService.DataObjects
{
    public class TodoItem : EntityData
    {
        public string UserId { get; set; }
        public string Name { get; set; }

        public bool Complete { get; set; }
    }
}