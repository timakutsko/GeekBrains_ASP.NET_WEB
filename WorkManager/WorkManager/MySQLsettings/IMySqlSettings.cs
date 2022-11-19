namespace WorkManager.MySQLsettings
{

    /// <summary>
    /// Интерфейс для класса настроек базы данных
    /// </summary>
    public interface IMySqlSettings<TablesEntity, ColumnsEntity>
    {
        /// <summary> Индексатор для имен таблиц </summary>
        /// <param name="key">Ключ для имени таблицы</param>
        /// <returns>Имя таблицы по ключу</returns>
        public string this[TablesEntity key] { get; }

        /// <summary> Индексатор для имен рядов </summary>
        /// <param name="key">Ключ для имени ряда</param>
        /// <returns>Имя ряда по ключу</returns>
        public string this[ColumnsEntity key] { get; }
    }
}
