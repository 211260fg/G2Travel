using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Http;
using Microsoft.WindowsAzure.Mobile.Service;
using G10TravelService.DataObjects;
using G10TravelService.Models;
using G10TravelService.Providers;

namespace G10TravelService
{
    public static class WebApiConfig
    {
        public static void Register()
        {
            // Use this class to set configuration options for your mobile service
            ConfigOptions options = new ConfigOptions();
            options.LoginProviders.Add(typeof(CustomLoginProvider));
            // Use this class to set WebAPI configuration options
            HttpConfiguration config = ServiceConfig.Initialize(new ConfigBuilder(options));

            // To display errors in the browser during development, uncomment the following
            // line. Comment it out again when you deploy your service for production use.
            // config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
            
            // Set default and null value handling to "Include" for Json Serializer
            config.Formatters.JsonFormatter.SerializerSettings.DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Include;
            config.Formatters.JsonFormatter.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Include;
            
            Database.SetInitializer(new G10TravelInitializer());
        }
    }

    public class G10TravelInitializer : ClearDatabaseSchemaIfModelChanges<G10TravelContext>
    {
        protected override void Seed(G10TravelContext context)
        {
            
        }
    }
}

