

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProjectWebAPI.Models;
using ProjectWebAPI.Services;
using Swashbuckle.AspNetCore.Swagger;
using AutoMapper;
using Amazon.DynamoDBv2;
using System.Threading.Tasks;
using Amazon.Runtime;
using Amazon;

namespace ProjectWebAPI
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

       

        // Configure AutoMapper
            services.AddAutoMapper(typeof(Startup));

            

            // Register DynamoDB client as a singleton
            
            services.AddSingleton<IAmazonDynamoDB>(sp =>
            {
                var awsCredentials = new BasicAWSCredentials("AKIA4MTWJJUZJGSEAV5B", "JbPs7w0r8StMpGwOL5VOH1DvSRLRgH7RxW6iwycQ");
                return new AmazonDynamoDBClient(awsCredentials, RegionEndpoint.USEast1);
            });

            // Register repositories
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();

            // Register DynamoDBInitializer
            services.AddSingleton<DynamoDBInitializer>();

            // Configure Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "ProjectApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DynamoDBInitializer dynamoDBInitializer)
        {
            // Initialize DynamoDB tables
            Task.Run(async () =>
            {
                await dynamoDBInitializer.CreateBooksTableAsync();
                await dynamoDBInitializer.CreateReviewsTableAsync();
            }).Wait();

            // Configure Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProjectApi");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
