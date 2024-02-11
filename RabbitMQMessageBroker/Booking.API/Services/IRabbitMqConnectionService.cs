using System;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace Booking.API.Services
{
    public interface IRabbitMqConnectionService: IDisposable
    {
        public IModel CreateChannel();
    }
}