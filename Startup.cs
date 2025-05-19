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
    /// ����� <c>Startup</c> ������������� ������� � middleware ��� ���-����������.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// ����������� Startup �������� ������ ������������ ����������.
        /// </summary>
        /// <param name="configuration">������������ ����������</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// ������ ������������ ����������.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// ����� ���������� �� ����� ����������. ������������ ��� ����������� �������� � ���������� ������������.
        /// </summary>
        /// <param name="services">��������� ��������</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // ���������� ������������ MVC
            services.AddControllers();

            // ����������� Swagger-������������
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "UWasting Server API",
                    Version = "v1",
                    Description = "API-������ ��� ���������� ���������� ��������� UWasting"
                });

                // ����������� XML-������������, ���� ��� �������� � �������
                var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = System.IO.Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        /// <summary>
        /// ����� ���������� �� ����� ����������. ����������� middleware �������� HTTP-��������.
        /// </summary>
        /// <param name="app">�������� ��������� ��������</param>
        /// <param name="env">����� ���������� (����������, ���� � �.�.)</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // ����������� �������� ������ � Swagger � ������ ����������
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "UWasting Server API v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                // ������������� ������������
                endpoints.MapControllers();
            });
        }
    }
}
