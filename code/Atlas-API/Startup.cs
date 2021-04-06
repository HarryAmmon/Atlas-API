using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Azure.Core;
using Atlas_API.Repositories;
using Atlas_API.Services;
using Atlas_API.Factories;
using Atlas_API.Entities;

namespace Atlas_API
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
            services.AddCors(options =>
            {
                options.AddPolicy("AllowMyOrigin", builder => builder.WithOrigins("http://localhost:3000", "https://red-rock-0949f4403.azurestaticapps.net").AllowAnyMethod().AllowAnyHeader());
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Atlas_API", Version = "v1" });
                });

            SecretClientOptions options = new SecretClientOptions()
            {
                Retry = {
                    Delay = TimeSpan.FromSeconds(2),
                    MaxDelay = TimeSpan.FromSeconds(16),
                    MaxRetries = 5,
                    Mode = RetryMode.Exponential
                }
            };

            services.AddSingleton(new SecretClient(new Uri("https://ci601-atlas.vault.azure.net/"), new DefaultAzureCredential(), options));
            services.AddSingleton<IAzureKeyVaultAccess, AzureKeyVaultAccess>();
            services.AddSingleton<IMongoClientFactory, MongoClientFactory>();
            services.AddScoped<IMongoDBContext, MongoDBContext>();
            services.AddScoped<IBaseRepository<UserStory>, UserStoryRepository>();
            services.AddScoped<IBaseRepository<ColumnGroup>, ColumnGroupRepository>();
            services.AddScoped<IBaseRepository<KanBanColumn>, KanBanColumnRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Atlas_API v1"));
            }

            app.UseRouting();
            app.UseCors("AllowMyOrigin");

            app.UseAuthorization();
            app.UseAuthentication();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
