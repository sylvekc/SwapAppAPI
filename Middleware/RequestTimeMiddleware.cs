using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace SwapApp.Middleware
{
    public class RequestTimeMiddleware : IMiddleware
    {
        private readonly ILogger _logger;
        private readonly Stopwatch _stopWatch;
        public RequestTimeMiddleware(ILogger<RequestTimeMiddleware> logger)
        {
            _logger = logger;
            _stopWatch = new Stopwatch();
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            _stopWatch.Start();
            await next.Invoke(context);
            _stopWatch.Stop();

            if(_stopWatch.ElapsedMilliseconds > 4000)
            {
                var message = $"Request [{context.Request.Method}] at {context.Request.Path} took {_stopWatch.ElapsedMilliseconds}ms";
                _logger.LogInformation(message);
            }
        }
    }
}
