using FluentMigrator.Runner;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;
using WorkManager.Repositories;
using WorkManager.Repositories.Interfaces;
using WorkManager.Data.Contexts;
using WorkManager.Data.Models;
using WorkManager.Responses;
using FluentValidation;
using WorkManager.Requests;
using WorkManager.Models.Validators;

namespace WorkManager
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

            services.AddControllersWithViews();
            
            // Настройка DB Context
            services.AddDbContext<WorkManagerDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("WorkManagerDbContext"));
            });

            // Настройка валидаторов
            services.AddScoped<IValidator<AuthenticateRequest>, AuthenticationRequestValidator>();
            services.AddScoped<IValidator<Client>, ClientValidator>();
            services.AddScoped<IValidator<ClientContract>, ClientContractValidator>();

            // Добавление исходных клиентов
            services.AddScoped<CreateDefaultClients>();

            // Работа с пользователями
            services.AddSingleton<AccountResponse>();
            services.AddSingleton<IUserRepository, AccountsRepository>();

            // Работа с клиентами
            services.AddScoped<ClientResponse>();
            services.AddScoped<IRepository<int, Client>, ClientsRepository>();

            // Работа с контрактами
            services.AddScoped<ClientContractResponse>();
            services.AddScoped<IRepository<int, ClientContract>, ClientContractsRepository>();

            // Работа с отчетами
            services.AddScoped<InvoiceResponse>();
            services.AddScoped<IRepository<int, Invoice>, InvoicesRepository>();

            // Работа с сотрудниками
            services.AddScoped<EmployeeResponse>();
            services.AddScoped<IRepository<int, Employee>, EmployeesRepository>();

            // Настриваю миграцию БД - работа с пользователями сервиса
            services.AddFluentMigratorCore()
                .ConfigureRunner(rb => rb.AddSQLite()) // Добавляем поддержку SQLite
                .AddLogging(lb => lb.AddFluentMigratorConsole());

            // Настройка аутентификации
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
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(AccountResponse.SecretCode)),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ClockSkew = TimeSpan.Zero
                    };
                });

            //Настройка swagger
            services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "WorkManager", Version = "v1" });
                    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer"
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
                            Array.Empty<string>()
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
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ASD v1");
                    c.RoutePrefix = string.Empty;
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors(x => x
                .SetIsOriginAllowed(origin => true)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            app.UseAuthentication();    // аутентификация
            app.UseAuthorization();     // авторизация

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
