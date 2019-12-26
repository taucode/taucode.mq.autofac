﻿using System;

namespace TauCode.Mq.Autofac.Demo.All.Messages
{
    public class Greeting : IMessage
    {
        // For serialization/deserialization
        public Greeting()
        {
        }

        public Greeting(string from, string to, string message)
        {
            this.CorrelationId = Guid.NewGuid().ToString();
            this.CreatedAt = DateTime.UtcNow;

            this.From = from;
            this.To = to;
            this.Message = message;
        }

        public string From { get; set; }
        public string To { get; set; }
        public string Message { get; set; }

        public string CorrelationId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
