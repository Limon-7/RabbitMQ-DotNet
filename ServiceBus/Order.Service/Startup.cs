using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Order.Service.Consumers;

namespace Order.Service
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            services.AddMassTransit(x =>
            {
                x.AddConsumer<OrderPlacedConsumer>();
                x.AddConsumer<CheckOrderStatusConsumer>();
                    // .Endpoint(e => e.Name = "check-order-status");
                x.AddRequestClient<CheckOrderStatus>();
                    // (new Uri("exchange:check-order-status"));
                
                x.UsingRabbitMq((context,config) =>
                {
                    config.Host("localhost","/", c =>
                    {
                        c.Username("guest");
                        c.Password("guest");
                    });
                    // config.ReceiveEndpoint("queue:order-placed", e =>
                    // {
                    //     e.Consumer<OrderPlacedConsumer>(context);
                    // });
                    //
                    // config.ReceiveEndpoint("check-order-status", e =>
                    // {
                    //     e.Consumer<CheckOrderStatusConsumer>(context);
                    // });
                    //
                    config.ConfigureEndpoints(context);
                });
                
            });

            services.AddMassTransitHostedService();
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Order.Service", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Order.Service v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
