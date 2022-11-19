using System.Collections.Generic;

namespace WorkManager.MySQLsettings
{
    /// <summary> Ключи имен рядов </summary>
    public enum ClientContractsColumns
    {
        Id,
        Title,
        FullTime,
        IsDeleted
    }

    /// <summary>Класс для хранения настроек базы данных</summary>
    internal sealed class SqlClientContractsTable : SqlTables, IMySqlSettings<MyTables, ClientContractsColumns>
    {
        /// <summary>Словарь для хранения имен рядов в таблицах</summary>
        private readonly Dictionary<ClientContractsColumns, string> _rowsNames = new Dictionary<ClientContractsColumns, string>
        {
            {ClientContractsColumns.Id, "Id" },
            {ClientContractsColumns.Title, "Title" },
            {ClientContractsColumns.FullTime, "FullTime" },
            {ClientContractsColumns.IsDeleted, "IsDeleted" },
        };

        public string this[ClientContractsColumns key]
        {
            get
            {
                return _rowsNames[key];
            }
        }

    }
}
