using AutoMapper;
using BusinessAccessLayer;
using BusinessAccessLayer.DragonService;
using BusinessAccessLayer.HeroService;
using BusinessAccessLayer.HitService;
using BusinessAccessLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Entity;
using HeroVsDragon.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Linq;
using System.Text;

namespace HeroVsDragon
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Settings = new CustomSettings();
            Configuration.Bind(Settings);
        }

        public IConfiguration Configuration { get; }
        private CustomSettings Settings { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(
                    Configuration["Data:ConnectionString"],
                    migrations => migrations.MigrationsAssembly("DataAccessLayer")
                );
            });
            services.AddControllersWithViews();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = Settings.JWTSettings.ValidateIssuer,
                    ValidateAudience = Settings.JWTSettings.ValidateAudience,
                    ValidateLifetime = Settings.JWTSettings.ValidateLifetime,
                    ValidIssuer = Settings.JWTSettings.Host,
                    ValidAudience = Settings.JWTSettings.Host,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Settings.JWTSettings.SecretKey))
                };
            });

            //Mapper
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            //DI
            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
            services.AddScoped<IHeroService, HeroService>();
            services.AddScoped<IDragonService, DragonService>();
            services.AddScoped<IHitService, HitService>();

            //Swagger
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo() { Title = "swagger api", Version = "v1" });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                });
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();
            /////////////////////////////JWT Authorization and Authentication
            app.UseAuthentication();
            app.UseAuthorization();
            ////////////////////////////
            SeedDefaultData(app);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }

        private void SeedDefaultData(IApplicationBuilder app)
        {
            var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();

            using (var scope = scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                if (dbContext.Heroes.FirstOrDefault(u => u.Nickname == "John Doe") == null)
                {
                    Hero hero1 = new Hero
                    {
                        Nickname = "John Doe",
                        Date_Time = DateTime.Now,
                        Gun = 4
                    };
                    Hero hero2 = new Hero
                    {
                        Nickname = "Vald Zalepa",
                        Date_Time = DateTime.Now,
                        Gun = 3
                    };
                    Hero hero3 = new Hero
                    {
                        Nickname = "Mark",
                        Date_Time = DateTime.Now,
                        Gun = 3
                    };

                    Dragon dragon1 = new Dragon
                    {
                        Name = "Nick",
                        HP = 85,
                        Date_Time = DateTime.Now 
                    };

                    Dragon dragon2 = new Dragon
                    {
                        Name = "Olafur",
                        HP = 93,
                        Date_Time = DateTime.Now 
                    };

                    Dragon dragon3 = new Dragon
                    {
                        Name = "Man",
                        HP = 81,
                        Date_Time = DateTime.Now 
                    };
                    Dragon dragon4 = new Dragon
                    {
                        Name = "Daniel",
                        HP = 99,
                        Date_Time = DateTime.Now 
                    };

                    dbContext.Heroes.AddRange(hero1, hero2, hero3);
                    dbContext.Dragons.AddRange(dragon1, dragon2,dragon3);
                    dbContext.SaveChanges();
                }
            }
        }
    }
}
