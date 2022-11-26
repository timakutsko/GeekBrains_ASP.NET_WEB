using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using System;

namespace WorkManager
{
    public class Program
    {

        public static void Main(string[] args)
        {
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

            try
            {
                logger.Info("---- [INIT]----");
                CreateHostBuilder(args).Build().Run();
            }
            // Отлов всех исключений в рамках работы приложения
            catch (Exception exception)
            {
                //NLog: устанавливаем отлов исключений
                logger.Error(exception, "Stopped program because of exception");
            }
            finally
            {
                // Остановка логгера
                NLog.LogManager.Shutdown();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                    .ConfigureKestrel(options =>
                    {
                        options.Listen(System.Net.IPAddress.Any, 5001, listenOptions =>
                        {
                            listenOptions.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http2;
                            listenOptions.UseHttps(@"D:\MyStudy\GeekBrains_ASP.NET_WEB\cert.pfx", "12345");
                        });
                    })
                    .UseKestrel()
                    .UseStartup<Startup>();
                })
                .ConfigureLogging(logging =>
                {
                    logging.AddDebug();
                    logging.ClearProviders(); // Создание провайдеров логирования
                    logging.SetMinimumLevel(LogLevel.Trace); //Устанавливаем минимальный уровень логирования
                }).UseNLog(); // Добавляем библиотеку nlog
    }
}
