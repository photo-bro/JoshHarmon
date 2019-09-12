using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace JoshHarmon.Site.Filters
{
    public class LocalHostFilterAttribute : Attribute, IResourceFilter
    {
        private readonly IList<string> _validHosts = new[] { "127.0.0.1", "::1", "localhost" };

        public void OnResourceExecuted(ResourceExecutedContext context)
        { }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            var requestHost = context.HttpContext.Request.Host.Host;
            if (!_validHosts.Contains(requestHost))
            {
                context.Result = new ContentResult
                {
                    Content = "Not authorized to access resource",
                    StatusCode = 401
                };
            }
        }
    }
}
