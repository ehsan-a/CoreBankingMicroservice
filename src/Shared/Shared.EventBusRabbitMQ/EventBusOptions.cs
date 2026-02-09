using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.EventBusRabbitMQ
{
    public class EventBusOptions
    {
        public string SubscriptionClientName { get; set; }
        public int RetryCount { get; set; } = 10;
    }
}
