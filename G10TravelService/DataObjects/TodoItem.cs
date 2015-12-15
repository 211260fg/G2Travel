using Microsoft.WindowsAzure.Mobile.Service;

namespace G10TravelService.DataObjects
{
    public class TodoItem : EntityData
    {
        public string Name { get; set; }

        public bool Complete { get; set; }
    }
}