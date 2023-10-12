using Microsoft.Extensions.DependencyInjection;
using Server.Classes;


namespace Server.Classes
{
    public static class WebSocketManagerExtensions
    {
        public static IServiceCollection AddWebSocketManager(this IServiceCollection services)
        {
            services.AddSingleton<WebSocketHandler>();
            return services;
        }

        
        public static IApplicationBuilder UseWebSocketManager(this IApplicationBuilder app, string path)
        {
            var socketHandler = app.ApplicationServices.GetService<WebSocketHandler>();

            app.Use(async (context, next) =>
            {
                if (context.WebSockets.IsWebSocketRequest)
                {
                    var socket = await context.WebSockets.AcceptWebSocketAsync();
                    await socketHandler.OnConnected(socket);
                }
                else
                {
                    await next();
                }
            });

            return app;
        }


    }
}

