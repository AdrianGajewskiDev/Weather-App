using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.Net;
using WeatherApp.API.Data;
using WeatherApp.API.Extensions;

namespace WeatherApp.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            Log.Information("Configuring services....", this);
            services.Configure<ApplicationData>(conf => 
            {
                conf.OWMApiKey = Configuration["OpenWeatherApi:ApiKey"];
                conf.OWMUrl = Configuration["OpenWeatherApi:Url"];
            });

            services.AddCors(conf => conf.AddPolicy("DevCorsPolicy", conf => conf.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
            services.AddHttpClient();
            services.AddApplicationServices();
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, Microsoft.Extensions.Logging.ILoggerFactory loggerFactory)
        {
            Log.Information("Configuring....", this);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors("DevCorsPolicy");
            }

            app.UseExceptionHandler(errors =>
            {
                errors.Run(async ctx =>
                {
                    Log.Error($"Internal server error while trying to complete request from {ctx.Request.GetEncodedUrl()}");

                    ctx.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                    ctx.Response.ContentType = "application/json";

                    var contextFeature = ctx.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        await ctx.Response.WriteAsync(new ExceptionInfo()
                        {
                            StatusCode = ctx.Response.StatusCode,
                            Message = "Internal Server Error."
                        }.ToString());
                    }
                });
            });
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
