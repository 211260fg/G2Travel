﻿using Microsoft.WindowsAzure.Mobile.Service;
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
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public List<string> itemsToBring { get; set; }
    }
}