using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;

namespace Laboratorio8.Web.SignalR.Hubs
{
    public class BaseHub<T, L> : Hub<T> where T : class
    {
        protected readonly ILogger _logger;

        public BaseHub(ILogger<L> logger)
        {
            _logger = logger;
        }

        protected virtual void OnException(Exception ex)
        {
            _logger.LogError(ex.ToString());
        }
    }
}
