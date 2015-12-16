using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.Mobile.Service.Security;
using System.Text.RegularExpressions;
using G10TravelService.DataObjects;
using G10TravelService.Models;

namespace G10TravelService.Requests
{
    public class RegistrationRequest
    {
        public String username { get; set; }
        public String password { get; set; }

    }
}