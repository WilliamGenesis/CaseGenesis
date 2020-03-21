using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace CaseGenesis.Middleware
{
	// You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
	public class ErrorHandlingMiddleware
	{
		private readonly RequestDelegate _next;

		public ErrorHandlingMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext httpContext)
		{
			try
			{
				await _next(httpContext);
			}
			catch(Exception e)
			{
				httpContext.Response.StatusCode = 500;
				httpContext.Response.ContentType = "application/json";
				httpContext.Response.Headers.Add("exception", "messageException");
				var json = JsonConvert.SerializeObject(new { Message = e.Message });
				await httpContext.Response.WriteAsync(json);
			}
		}
	}

	// Extension method used to add the middleware to the HTTP request pipeline.
	public static class ErrorHandlingMiddlewareExtensions
	{
		public static IApplicationBuilder UseErrorHandlingMiddleware(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<ErrorHandlingMiddleware>();
		}
	}
}
