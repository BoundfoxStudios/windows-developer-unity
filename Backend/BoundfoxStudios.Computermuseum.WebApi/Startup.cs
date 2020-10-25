using BoundfoxStudios.Computermuseum.WebApi.Data;
using BoundfoxStudios.Computermuseum.WebApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace BoundfoxStudios.Computermuseum.WebApi
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddDbContext<MuseumDbContext>(config => config.UseInMemoryDatabase("MuseumDb"));
      services.AddControllers()
        .AddJsonOptions(jsonOptions =>
        {
          jsonOptions.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
          jsonOptions.JsonSerializerOptions.PropertyNamingPolicy = null;
        });

      services.AddTransient<DataInitializer>();
      services.AddTransient<InformationService>();

      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Museum API", Version = "v1" });
        c.IncludeXmlComments("./BoundfoxStudios.Computermuseum.WebApi.xml");
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseCors(corsBuilder => corsBuilder
        .AllowAnyHeader()
        .AllowAnyMethod()
        .SetIsOriginAllowed(_ => true)
      );

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseAuthorization();

      app.UseSwagger();
      app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Museum API v1"));

      app.UseRewriter(new RewriteOptions().AddRedirect("^$", "swagger"));

      app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
  }
}
