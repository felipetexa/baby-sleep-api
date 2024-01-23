using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class Startup
{
  public Startup(IConfiguration configuration)
  {
    Configuration = configuration;
  }

  public IConfiguration Configuration { get; }

  public void ConfigureServices(IServiceCollection services)
  {
    services.AddDbContext<SleepContext>(options =>
        options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

    // services.AddCors(options =>
    // {
    //   options.AddPolicy("MyCorsPolicy",
    //           builder =>
    //           {
    //             builder
    //                     .AllowAnyOrigin()
    //                     .AllowAnyHeader()
    //                     .AllowAnyMethod()
    //                     .WithMethods("OPTIONS");
    //           });
    // });

    services.AddControllers();
  }

  public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
  {
    if (env.IsDevelopment())
    {
      app.UseDeveloperExceptionPage();
    }

    app.UseStaticFiles();
    // app.UseCors("MyCorsPolicy");
    app.UseRouting();


    logger.LogError("An error occurred.");

    app.UseEndpoints(endpoints =>
    {
      endpoints.MapGet("/echo",
              context => context.Response.WriteAsync("echo"))
              .RequireCors("MyCorsPolicy");

      endpoints.MapControllers()
                   .RequireCors("MyCorsPolicy");

      endpoints.MapGet("/echo2",
              context => context.Response.WriteAsync("echo2"));

      endpoints.MapRazorPages();
    });
    logger.LogInformation("Application configured.");
  }
}
