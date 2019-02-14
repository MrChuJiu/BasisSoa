using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BasisSoa.Common.AppSettingsHelper;
using BasisSoa.Core;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BasisSoa.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {

            //Console.WriteLine(Directory.GetCurrentDirectory());
            //Console.WriteLine(AppContext.BaseDirectory);
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            ConfigManager.SetConfiguration(config);
           


            //CreateWebHostBuilder(args).Build().Run();


            // 生成承载 web 应用程序的 Microsoft.AspNetCore.Hosting.IWebHost。Build是WebHostBuilder最终的目的，将返回一个构造的WebHost。
            var host = CreateWebHostBuilder(args).Build();
            // 创建可用于解析作用域服务的新 Microsoft.Extensions.DependencyInjection.IServiceScope。
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                try
                {
                    // 从 system.IServicec提供程序获取 T 类型的服务。
                    BaseDbContext.Init(ConfigManager.Configuration["ConnectionStrings:DefaultConnection"]);
                    var DbContext = services.GetRequiredService<BaseDbContext>();
                    BaseDbInit.SeedAsync(DbContext).Wait();
                }
                catch (Exception e)
                {
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(e, "Error occured seeding the Database.");
                }
            }
            // 运行 web 应用程序并阻止调用线程, 直到主机关闭。
            // 创建完 WebHost 之后，便调用它的 Run 方法，而 Run 方法会去调用 WebHost 的 StartAsync 方法
            // 将Initialize方法创建的Application管道传入以供处理消息
            // 执行HostedServiceExecutor.StartAsync方法
            host.Run();

        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
