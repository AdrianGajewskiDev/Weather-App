using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors("DevCorsPolicy");
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
