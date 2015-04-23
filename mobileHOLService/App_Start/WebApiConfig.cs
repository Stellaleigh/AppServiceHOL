﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Http;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.AppService.Config;
using mobileHOLService.DataObjects;
using mobileHOLService.Models;
using System.Web.Http.Cors;

namespace mobileHOLService
{
    public static class WebApiConfig
    {
        public static void Register()
        {
            AppServiceExtensionConfig.Initialize();

            // Use this class to set configuration options for your mobile service
            ConfigOptions options = new ConfigOptions();

            // Use this class to set WebAPI configuration options
            HttpConfiguration config = ServiceConfig.Initialize(new ConfigBuilder(options));

            // Enable CORS
            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));

            // To display errors in the browser during development, uncomment the following
            // line. Comment it out again when you deploy your service for production use.
            // config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

            Database.SetInitializer(new mobileHOLInitializer());
        }
    }

    public class mobileHOLInitializer : ClearDatabaseSchemaIfModelChanges<mobileHOLContext>
    {
        protected override void Seed(mobileHOLContext context)
        {
            List<TodoItem> todoItems = new List<TodoItem>
            {
                new TodoItem { Id = Guid.NewGuid().ToString(), Text = "First item", Complete = false, PhoneNumber="4259999561", Processed=false},
                new TodoItem { Id = Guid.NewGuid().ToString(), Text = "Second item", Complete = false,PhoneNumber="4259999561", Processed=false },
            };

            foreach (TodoItem todoItem in todoItems)
            {
                context.Set<TodoItem>().Add(todoItem);
            }

            base.Seed(context);
        }
    }
}

