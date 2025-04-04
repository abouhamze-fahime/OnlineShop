using Infrastructure.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

namespace API
{
    public static class DIRegister
    {

        public static IServiceCollection AddJWT(this IServiceCollection services)
        {
            var sp = services.BuildServiceProvider();
            AppConfigurations configs = sp.GetService<IOptions<AppConfigurations>>().Value;
            var key = Encoding.UTF8.GetBytes(configs.TokenKey);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ClockSkew = TimeSpan.FromMinutes(configs.TokenTimeOut),
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience=false
                };
            });
            return services;
        }



        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = "Online Shop",
                    Description = "Online Shop ******** Version01",
                    TermsOfService = new Uri("https://devtube.ir"),
                    License = new Microsoft.OpenApi.Models.OpenApiLicense
                    {
                        Name = "DevTube",
                        Url = new Uri("https://devtube.ir/"),
                    }
                });

                //var security = new Dictionary<string, IEnumerable<string>>
                //{
                //    {"Bearer" , new string[] { } },
                //};

                c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                   Reference = new Microsoft.OpenApi.Models.OpenApiReference
                   {
                       Type= Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                       Id="Bearer"
                   },
                   Scheme="Bearer",
                   Name="Authorization",
                   In=Microsoft.OpenApi.Models.ParameterLocation.Header,
                });

                c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement { 
                         {
                             new OpenApiSecurityScheme
                             {
                                 Reference = new OpenApiReference
                                 {
                                     Type = ReferenceType.SecurityScheme,
                                     Id = "Bearer"
                                 }
                              },
                              new string[] { }
                         }
                });
                c.EnableAnnotations();
                //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile) ;
                //c.IncludeXmlComments(xmlPath) ;
           });
            return services;
        }
    }
}
