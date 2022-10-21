using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using StudentAdminPortal.API.DataModels;
using StudentAdminPortal.API.Repositories;
using Microsoft.OpenApi.Models;

namespace StudentAdminPortal.API
{
    public class Startup
	{
		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public Startup(IConfiguration configuration)
        {
			this.configuration = configuration;
        }
		public IConfiguration configuration { get; }
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();
						
           services.AddDbContext<StudentAdminContext>(Options => Options.UseSqlServer(configuration.GetConnectionString("StudentAdminPortalDB")));
			services.AddScoped<IStudentRepository, SqlStudentRepository>();
			services.AddSwaggerGen();
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "StudentAdminPortal.API", Version = "v1" });
			});
			services.AddAutoMapper(typeof(Startup).Assembly);

		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseRouting();
			app.UseSwagger();
			app.UseSwaggerUI(c => {
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "Showing API V1");
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "StudentAdminPortal.API v1"));
			});
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});

			//app.UseEndpoints(endpoints =>
			//{
			//	endpoints.MapGet("/", async context =>
			//	{
			//		await context.Response.WriteAsync("Hello World!");
			//	});
			//});
		}
	}
}
