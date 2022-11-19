using System.Collections.Generic;

namespace WorkManager.DAL.Repositories.Interfaces
{
    public interface IRepository<TIdentity, TEntity> where TEntity : class where TIdentity : struct
    {
        /// <summary>
		/// Записывает элемент в БД
		/// </summary>
		/// <param name="entity">Элемент для записи</param>
        bool Create(TEntity entity);

        /// <summary>
        /// Возвращает все элементы из БД
        /// </summary>
        /// <returns>Коллекция элементов</returns>
        IReadOnlyDictionary<TIdentity, TEntity> Get();

        /// <summary>
        /// Возвращает элемент из БД
        /// </summary>
        /// <param name="id">Идентификатор элемента</param>
        /// <returns>Элемент</returns>
        TEntity GetById(TIdentity id);

        /// <summary>
        /// Обновляет элемент из БД
        /// </summary>
        /// <param name="id">Идентификатор элемента</param>
        /// <returns>Элемент</returns>
        bool UpdateById(TIdentity id, string reqColumnName, string value);

        /// <summary>
        /// Удаляет элемент из БД (точнее - архивирует)
        /// </summary>
        /// <param name="id">Идентификатор элемента</param>
        bool DeleteById(TIdentity id);
    }
}
