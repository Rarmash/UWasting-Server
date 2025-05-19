using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace Server
{
    /// <summary>
    /// Класс <c>Program</c> является точкой входа в серверное приложение UWasting.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Главный метод. Запускает веб-сервер.
        /// </summary>
        /// <param name="args">Аргументы командной строки</param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Создаёт и настраивает объект <see cref="IHostBuilder"/>.
        /// Устанавливает <c>Startup</c> как стартовый класс и URL из переменной окружения PORT.
        /// </summary>
        /// <param name="args">Аргументы командной строки</param>
        /// <returns>Готовый <see cref="IHostBuilder"/> экземпляр</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    var port = Environment.GetEnvironmentVariable("PORT");

                    webBuilder.UseStartup<Startup>()
                              .UseUrls("http://*:" + port);
                });
    }
}
