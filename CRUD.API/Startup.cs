using CRUD.API.BL.Container;
using CRUD.API.BL.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UoN.ExpressiveAnnotations.NetCore.DependencyInjection;

namespace CRUD.API
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
            byte[] jwtKey = Encoding.ASCII.GetBytes(Configuration.GetValue<string>(Constants.CONFIG_CRUD_JWT_KEY));

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(jwtKey),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddCors(options =>
            {
                options.AddPolicy(Configuration.GetValue<string>(Constants.CONFIG_CRUD_NAME_APPLICATION),
                    builder =>
                    {
                        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                    });
            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddControllers()
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.SuppressModelStateInvalidFilter = true;
                });

            services.AddExpressiveAnnotations();

            services.AddSwaggerGen(options =>
            {
                var apiVersion = "v1";

                options.SwaggerDoc(apiVersion, new OpenApiInfo
                {
                    Title = Configuration.GetValue<string>(Constants.CONFIG_CRUD_NAME_APPLICATION),
                    Version = apiVersion
                });

                options.ExampleFilters();
                options.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();

                options.OperationFilter<SecurityRequirementsOperationFilter>(false);
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                // var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //options.IncludeXmlComments(xmlPath);
            });
            services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());

            // services.AddSingleton(new BlobServiceClient(Configuration.GetConnectionString(Constants.CONFIG_CONNECTION_AZURE_STORAGE)));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", Configuration.GetValue<string>(Constants.CONFIG_CRUD_NAME_APPLICATION));
                c.RoutePrefix = string.Empty;
                c.DefaultModelsExpandDepth(-1);
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(Configuration.GetValue<string>(Constants.CONFIG_CRUD_NAME_APPLICATION));

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            BLContainer.Load(Configuration);
            //Is the connection with azure
            //Task.Run(() => AzureStorageHelper.VerifyAzureContainersAsync(Configuration.GetConnectionString(Constants.CONFIG_CONNECTION_AZURE_STORAGE))).Wait();
        }
    }

}
