using System.Collections.Generic;

namespace WorkManager.MySQLsettings
{
    /// <summary> Ключи имен рядов </summary>
    public enum UsersColumns
    {
        Id,
        Login,
        PasswordSalt,
        PasswordHash,
        RefreshToken,
        IsDeleted
    }

    /// <summary>Класс для хранения настроек базы данных</summary>
    internal sealed class SqlUsersTable : SqlTables, IMySqlSettings<MyTables, UsersColumns>
    {
        /// <summary>Словарь для хранения имен рядов в таблицах</summary>
        private readonly Dictionary<UsersColumns, string> _rowsNames = new Dictionary<UsersColumns, string>
        {
            {UsersColumns.Id, "Id" },
            {UsersColumns.Login, "Login" },
            {UsersColumns.PasswordSalt, "PasswordSalt" },
            {UsersColumns.PasswordHash, "PasswordHash" },
            {UsersColumns.RefreshToken, "RefreshToken" },
            {UsersColumns.IsDeleted, "IsDeleted" },
        };

        public string this[UsersColumns key]
        {
            get
            {
                return _rowsNames[key];
            }
        }

    }
}
