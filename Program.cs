using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Google.Apis.FirebaseCloudMessaging.v1;
using iread_notifications_ms.Web.Service;


namespace iread_notifications_ms
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var res = await new FirebaseMessagingService().sendMessage("title", "body", "");
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}