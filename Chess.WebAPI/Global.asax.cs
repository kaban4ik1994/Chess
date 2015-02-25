﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Routing;
using AutoMapper;
using Chess.WebAPI.App_Start;
using Chess.WebAPI.Mappings;
using Microsoft.Practices.Unity.Configuration;
using Microsoft.Practices.Unity.WebApi;

namespace Chess.WebAPI
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(UnityConfig.GetConfiguredContainer());
            GlobalConfiguration.Configure(WebApiConfig.Register);
            InitilizeMapper();
        }

        private static void InitilizeMapper()
        {
            Mapper.Initialize(
                config =>
                {
                    config.AddProfile<UserModelProfile>();
                });
            Mapper.AssertConfigurationIsValid();
        }
    }
}
