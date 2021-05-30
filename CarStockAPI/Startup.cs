using CarStockAPI.Auth;
using CarStockAPI.Data;
using CarStockAPI.Filters;
using CarStockAPI.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarStockAPI
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
            services.AddDbContext<CarStockContext>(opt => opt.UseInMemoryDatabase("InMemoryCarStockDb"));

            services.AddSingleton<ICustomTokenManager, JwtTokenManager>();
            services.AddSingleton<IDealerManager, DealerManager>();

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CarStockAPI", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Tokenheader",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter your valid token in the textbox below. To generate token, use /authenticate with clientId = dealerClientId, secretKey = dealerSecretKey",
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}

                    }
                });
            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<ICarRepo, InMemoryCarRepo>();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, CarStockContext context)
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CarStockAPI v1");
                c.RoutePrefix = string.Empty;  // Set Swagger UI at apps root
            });
            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Add test data into the in-memory database when application starts up
            AddTestData(context);
        }

        // Generate test data
        private static void AddTestData(CarStockContext context)
        {
            var cars = new List<Car>
            {
                new Car { Id = new Guid("81188fb9-678e-4f9a-9c09-7ee3784545ac"), Make = "Audi", Model = "A4", Year = 2014, CreatedTime =DateTime.Now, StockQuantity = 10, DealerClientId = "dealerClientId"},
                new Car { Id = new Guid("422e5db1-e7d7-4555-8293-04d0bf2f667c"), Make = "BMW", Model = "5 Series", Year = 2016, CreatedTime =DateTime.Now, StockQuantity = 22, DealerClientId = "dealerClientId"},
                new Car { Id = new Guid("7026201f-e66a-4704-87b7-720c41bc76fd"), Make = "Audi", Model = "A5", Year = 2015, CreatedTime =DateTime.Now, StockQuantity = 33, DealerClientId = "dealerClientId"},
                new Car { Id = new Guid("11017440-c13a-11eb-8529-0242ac130003"), Make = "Audi", Model = "A5", Year = 2017, CreatedTime =DateTime.Now, StockQuantity = 71, DealerClientId = "dealerClientId"},
                new Car { Id = new Guid("16682b86-c13a-11eb-8529-0242ac130003"), Make = "BMW", Model = "4 Series", Year = 2017, CreatedTime =DateTime.Now, StockQuantity = 17, DealerClientId = "anotherDealerClientId"},
                new Car { Id = new Guid("1eeb7a74-c13a-11eb-8529-0242ac130003"), Make = "BMW", Model = "5 Series", Year = 2016, CreatedTime =DateTime.Now, StockQuantity = 12, DealerClientId = "anotherDealerClientId"},
                new Car { Id = new Guid("22dea2fa-c13a-11eb-8529-0242ac130003"), Make = "Audi", Model = "A5", Year = 2015, CreatedTime =DateTime.Now, StockQuantity = 36, DealerClientId = "anotherDealerClientId"}
            };

            context.Cars.AddRange(cars);
            context.SaveChanges();
        }
    }
}
