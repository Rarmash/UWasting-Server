using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;

namespace Server
{
    /// <summary>
    /// Класс <c>Startup</c> конфигурирует сервисы и middleware для веб-приложения.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Конструктор Startup получает объект конфигурации приложения.
        /// </summary>
        /// <param name="configuration">Конфигурация приложения</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Объект конфигурации приложения.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Метод вызывается во время выполнения. Используется для регистрации сервисов в контейнере зависимостей.
        /// </summary>
        /// <param name="services">Коллекция сервисов</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Добавление контроллеров MVC
            services.AddControllers();

            // Регистрация Swagger-документации
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "UWasting Server API",
                    Version = "v1",
                    Description = "API-сервер для приложения управления финансами UWasting"
                });

                // Подключение XML-документации, если она включена в проекте
                var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = System.IO.Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        /// <summary>
        /// Метод вызывается во время выполнения. Настраивает middleware пайплайн HTTP-запросов.
        /// </summary>
        /// <param name="app">Конвейер обработки запросов</param>
        /// <param name="env">Среда выполнения (разработка, прод и т.п.)</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // Подключение страницы ошибок и Swagger в режиме разработки
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "UWasting Server API v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                // Маршрутизация контроллеров
                endpoints.MapControllers();
            });
        }
    }
}
