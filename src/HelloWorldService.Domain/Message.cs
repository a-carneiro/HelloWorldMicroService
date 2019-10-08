using System;
using System.Collections.Generic;
using System.Text;

namespace HelloWorldService.Domain
{
    public class Message
    {
        public Guid Id { get; set; }
        public string MicroserviceId { get; set; }
        public string Content { get; set; }
        public string Timestamp { get; set; }
    }
}
