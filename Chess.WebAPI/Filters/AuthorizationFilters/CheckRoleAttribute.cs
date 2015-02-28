using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Controllers;
using Chess.Services;
using Chess.Services.Interfaces;
using Chess.WebAPI.App_Start;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.WebApi;

namespace Chess.WebAPI.Filters.AuthorizationFilters
{
    public class CheckRoleAttribute : AuthorizeAttribute
    {
        private readonly List<string> _roles;

        [Dependency]
        public IUserService UserService { get; set; }

        public CheckRoleAttribute(string roles)
        {

            UserService = (UserService)new UnityDependencyResolver(UnityConfig.GetConfiguredContainer()).GetService(typeof(IUserService));

            _roles = roles.Split(',').ToList();
        }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            Guid guid;
            if (actionContext.Request.Headers.Authorization == null) return false;
            var data = actionContext.Request.Headers.Authorization.ToString();

            if (!Guid.TryParse(data, out guid))
            {
                return false;
            }

            return UserService.GetUserAccessByTokenQuery(guid, _roles).Result;
        }
    }
}