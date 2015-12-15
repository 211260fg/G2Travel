﻿using Microsoft.WindowsAzure.Mobile.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.Mobile.Service;

namespace G10TravelService.DataObjects
{
    public class Account : EntityData
    {
        public string Username { get; set; }
        public byte[] Salt { get; set; }
        public byte[] SaltedAndHashedPassword { get; set; }

    }
}