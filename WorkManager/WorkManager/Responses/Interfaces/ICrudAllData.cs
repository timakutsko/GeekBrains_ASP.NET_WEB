using System.Collections.Generic;

namespace WorkManager.Responses.Interfaces
{
    public interface ICrudAllData<TEntity> where TEntity : class
    {
        /// <summary>
        /// Создание сущности в ответ серверу
        /// </summary>
        IReadOnlyDictionary<int, TEntity> GetAllData();
    }
}
