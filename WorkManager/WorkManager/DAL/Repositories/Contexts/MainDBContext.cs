using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using WorkManager.DAL.Models.Archive;
using WorkManager.MySQLsettings;

namespace WorkManager.DAL.Repositories.Contexts
{
    /// <summary>
    /// Основной контекст для БД, который должны наследовать репозитории
    /// </summary>
    /// <typeparam name="TEntity">Класс-сущность</typeparam>
    /// <typeparam name="TTables">Enum-таблицы из БД</typeparam>
    /// <typeparam name="TColumns">Enum-колонки из таблицы БД</typeparam>
    internal class MainDBContext<TEntity, TTables, TColumns> : DbContext 
        where TEntity : class
        where TTables : struct
        where TColumns : struct
    {
        // Инжектируем DI провайдер
        protected IServiceProvider _provider;
        protected IMySqlSettings<TTables, TColumns> _sqlSettings;

        public IMySqlSettings<TTables, TColumns> MySqlSettings { get { return _sqlSettings; } }

        /// <summary>
        /// Все элементы из БД
        /// </summary>
        public DbSet<TEntity> DBEntities { get; set; }
    }
}
