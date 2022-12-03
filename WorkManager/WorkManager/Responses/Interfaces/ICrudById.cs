namespace WorkManager.Responses.Interfaces
{
    public interface ICrudById<TEntity> where TEntity : class
    {
        /// <summary>
        /// Создание сущности в ответ серверу
        /// </summary>
        void Register(TEntity entity);

        /// <summary>
        /// Передача сущности в ответ серверу (по id)
        /// </summary>
        TEntity GetById(int id);

        /// <summary>
        /// Обновление сущности в ответ серверу (по id, имени параметра и значению параметра)
        /// </summary>
        void UpdateById(int id, string reqColumnName, string value);

        /// <summary>
        /// Удаление сущности в ответ серверу (по id)
        /// </summary>
        void DeleteById(int id);
    }
}
