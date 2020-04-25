using LayeredArch.Core.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace LayeredArch.Api.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }
        }

        private static Task HandleException(HttpContext context, Exception ex)
        {
            HttpStatusCode code;
            string result;

            // Specify different custom exceptions here
            if (ex is ICoreException)
            {
                var coreException = (ICoreException)ex;
                code = (HttpStatusCode)coreException.StatusCode;
                result = JsonConvert.SerializeObject(new { message = coreException.ErrorMessgae });
            }
            else
            {
                code = HttpStatusCode.InternalServerError; // 500 if unexpected
                result = JsonConvert.SerializeObject(new { message = "Something went wrong, Please try agin later!" });
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(result);
        }
    }
}
