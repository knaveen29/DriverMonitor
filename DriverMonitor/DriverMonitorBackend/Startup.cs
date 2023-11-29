using DriverMonitorBackend.Repository;
using DriverMonitorBackend.Services;
using Microsoft.EntityFrameworkCore;

namespace DriverMonitorBackend
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
            services.AddCors();
            services.AddMvc();
            services.AddScoped<IDriverActivityRepository, DriverActivityRepository>();
            services.AddScoped<IDriverActivityService, DriverActivityService>();

            services.AddControllers();

            var connectionString = Configuration.GetConnectionString("DbContext");
            services.AddDbContext<ApiDBContext>(options =>
                options.UseNpgsql(
                    connectionString
                )
            );

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000"));

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Web API V1");
            });
        }
    }
}
