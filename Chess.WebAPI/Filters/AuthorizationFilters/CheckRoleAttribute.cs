using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Controllers;
using Chess.Services.Interfaces;

namespace Chess.WebAPI.Filters.AuthorizationFilters
{
    public class CheckRoleAttribute : AuthorizeAttribute
    {
        private readonly List<string> _roles;

        public IUserService UserService { get; set; }

        public CheckRoleAttribute(string roles)
        {
            UserService = (IUserService)GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IUserService));

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

            return UserService.GetUserAccessByTokenQuery(guid, _roles);
        }
    }
}