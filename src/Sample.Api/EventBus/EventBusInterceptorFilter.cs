using System;
using System.Collections.Generic;
using DotNetCore.CAP.Filter;
using Microsoft.Extensions.DependencyInjection;

namespace Sample.Api.EventBus
{
    public class EventBusInterceptorFilter : ISubscribeFilter
    {
        private readonly IEnumerable<EventBusInterceptor> _eventBusInterceptors;

        public EventBusInterceptorFilter(IServiceProvider serviceProvider)
        {
            _eventBusInterceptors = serviceProvider.GetServices<EventBusInterceptor>();
        }

        public void OnSubscribeException(ExceptionContext context)
        {
            foreach (EventBusInterceptor eventBusInterceptor in _eventBusInterceptors)
            {
                eventBusInterceptor.OnSubscribeException(context);
            }
        }

        public void OnSubscribeExecuted(ExecutedContext context)
        {
            foreach (EventBusInterceptor eventBusInterceptor in _eventBusInterceptors)
            {
                eventBusInterceptor.OnSubscribeExecuted(context);
            }
        }

        public void OnSubscribeExecuting(ExecutingContext context)
        {
            foreach (EventBusInterceptor eventBusInterceptor in _eventBusInterceptors)
            {
                eventBusInterceptor.OnSubscribeExecuting(context);
            }
        }
    }
}