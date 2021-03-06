﻿using G10TravelService.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace G10TravelService.Requests
{
    public class ListItemRequest
    {
        public String name { get; set; }
        public String location { get; set; }
        public String startdate { get; set; }
        public String enddate { get; set; }
        public virtual ICollection<Item> itemstobring { get; set; }
    }
}