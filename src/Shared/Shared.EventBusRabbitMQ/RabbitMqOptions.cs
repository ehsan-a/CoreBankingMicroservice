using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.EventBusRabbitMQ
{
    public class RabbitMqOptions
    {
        public string HostName { get; set; } = default!;
        public int Port { get; set; } = 5672;
        public string UserName { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string VirtualHost { get; set; } = "/";
        public string ClientProvidedName { get; set; } = "EventBus";
    }
}
