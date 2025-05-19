using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace Server
{
    /// <summary>
    /// ����� <c>Program</c> �������� ������ ����� � ��������� ���������� UWasting.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// ������� �����. ��������� ���-������.
        /// </summary>
        /// <param name="args">��������� ��������� ������</param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// ������ � ����������� ������ <see cref="IHostBuilder"/>.
        /// ������������� <c>Startup</c> ��� ��������� ����� � URL �� ���������� ��������� PORT.
        /// </summary>
        /// <param name="args">��������� ��������� ������</param>
        /// <returns>������� <see cref="IHostBuilder"/> ���������</returns>
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
