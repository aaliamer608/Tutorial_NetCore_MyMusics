using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MyMusic.Api.Extensions;
using MyMusic.Api.Settings;
using MyMusic.Core;
using MyMusic.Core.Models.Auth;
using MyMusic.Core.ServiceInterface;
using MyMusic.Data;
using MyMusic.Services.ServiceLogics;
using Swashbuckle.AspNetCore.Swagger;

namespace MyMusic.Api
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
            var jwtSettings = Configuration.GetSection("Jwt").Get<JwtSettings>();

            services.AddControllers();


            services.AddDbContext<MyMusicDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("Default"), x
            => x.MigrationsAssembly("MyMusic.Data")));
            
            
            
            services.AddIdentity<User, Role>().AddEntityFrameworkStores<MyMusicDbContext>().AddDefaultTokenProviders();


            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IMusicService, MusicService>();
            services.AddTransient<IArtistService, ArtistService>();


            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT containing userid claim",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                });
                var security =
                new OpenApiSecurityRequirement
                {
{
new OpenApiSecurityScheme
{
Reference = new OpenApiReference
{
Id = "Bearer",
Type = ReferenceType.SecurityScheme
},
UnresolvedReference = true
},
new List<string>()
}
                };
                c.AddSecurityRequirement(security);
            });

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "My Music",
                    Version = "v1"
                });

            });



            services.AddAutoMapper(typeof(Startup));


            services.Configure<JwtSettings>(Configuration.GetSection("Jwt"));

            services.AddAuth(jwtSettings);

        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseSwagger();
                //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyMusic.Api v1"));
            }
            else
            {
                //Default HSTS is 30 days https://aka.ms/aspnetcore-hsts 
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseAuthorization();

            app.UseAuth();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Music V1");
            });
        }
    }
}


