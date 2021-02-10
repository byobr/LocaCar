using BLL;
using Fronteiras.BLL;
using Fronteiras.Repositorios;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Repositorios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using System;

namespace LocaCar
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            //Repositorios
            services.AddScoped<IMarcasRepositorio, MarcasRepositorio>();
            services.AddScoped<IModelosRepositorio, ModelosRepositorio>();
            services.AddScoped<IOperadoresRepositorio, OperadoresRepositorio>();
            services.AddScoped<IClientesRepositorio, ClientesRepositorio>();
            services.AddScoped<ICarrosRepositorio, CarrosRepositorio>();
            services.AddScoped<IAlugueisRepositorio, AlugueisRepositorio>();

            //BLLs
            services.AddTransient<IMarcasBLL, MarcasBLL>();
            services.AddTransient<IModelosBLL, ModelosBLL>();
            services.AddTransient<IOperadoresBLL, OperadoresBLL>();
            services.AddTransient<IClientesBLL, ClientesBLL>();
            services.AddTransient<ICarrosBLL, CarrosBLL>();
            services.AddTransient<IAlugueisBLL, AlugueisBLL>();

            //Servicos
            services.AddTransient<IAutorizacaoBLL, AutorizacaoBLL>();

            services.AddDbContext<Contexto>(
            options =>
                options.UseSqlServer(Environment.GetEnvironmentVariable("cs")));

            byte[] Secret = Encoding.ASCII.GetBytes(_configuration.GetSection("Config").GetSection("secret_auth").Value);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Secret),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "LocaCar",
                    Version = "v1"
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Por favor, insira o Token com o Bearer abaixo.",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                                        {
                                          new OpenApiSecurityScheme {
                                            Reference = new OpenApiReference {
                                              Type = ReferenceType.SecurityScheme,
                                                Id = "Bearer"
                                            }
                                          },
                                          new string[] {}
                                        }
                                    });
            });
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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "LocaCar webApi");

            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
