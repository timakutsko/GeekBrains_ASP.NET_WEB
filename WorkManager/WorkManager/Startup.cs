using FluentMigrator.Runner;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkManager.DAL.Models;
using WorkManager.DAL.Repositories;
using WorkManager.DAL.Repositories.Contexts;
using WorkManager.MySQLsettings;
using WorkManager.Repositories.Interfaces;
using WorkManager.Responses;

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
            services.AddControllersWithViews();

            // Добавление исходных клиентов
            services.AddSingleton<CreateDefaultClients>();
            
            // Работа с клиентами
            services.AddSingleton<ClientResponse>();
            services.AddSingleton<ClientDbContext>();
            services.AddSingleton<IRepository<int, Client>, ClientsRepository>();

            // Работа с контрактами
            services.AddSingleton<ClientContractResponse>();
            services.AddSingleton<ClientContractDbContext>();
            services.AddSingleton<IRepository<int, ClientContract>, ClientContractsRepository>();

            // Работа с отчетами
            services.AddSingleton<InvoiceResponse>();
            services.AddSingleton<InvoiceDbContext>();
            services.AddSingleton<IRepository<int, Invoice>, InvoicesRepository>();

            // Работа с сотрудниками
            services.AddSingleton<EmployeeResponse>();
            services.AddSingleton<EmployeeDbContext>();
            services.AddSingleton<IRepository<int, Employee>, EmployeesRepository>();

            // Настриваю миграцию БД
            services.AddSingleton<IMySqlSettings<Tables, ClientsColumns>, MySqlClients>();
            services.AddSingleton<IMySqlSettings<Tables, ClientContractsColumns>, MySqlClientContracts>();
            services.AddSingleton<IMySqlSettings<Tables, InvoicesColumns>, MySqlInvoices>();
            services.AddSingleton<IMySqlSettings<Tables, EmployeesColumns>, MySqlEmployees>();
            services.AddFluentMigratorCore()
                .ConfigureRunner(rb => rb.AddSQLite() // Добавляем поддержку SQLite
                    .WithGlobalConnectionString(MySqlTables.ConnectionString) // Устанавливаем строку подключения
                    .ScanIn(typeof(Startup).Assembly).For.Migrations()) // Подсказываем, где искать класс с миграциями
                .AddLogging(lb => lb.AddFluentMigratorConsole());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMigrationRunner migrationRunner)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            // Создаю БД запуском миграции
            migrationRunner.MigrateUp();
        }
    }
}
