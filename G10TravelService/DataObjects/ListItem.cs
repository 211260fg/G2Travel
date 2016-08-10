using Microsoft.WindowsAzure.Mobile.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace G10TravelService.DataObjects
{
    public class ListItem : EntityData
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public virtual ICollection<Item> itemsToBring { get; set; }
        public bool Done { get; set; }
    }
}