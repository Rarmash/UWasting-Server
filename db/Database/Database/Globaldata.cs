using System;

namespace Database
{
    /// <summary>
    /// Статический класс <c>Globaldata</c> содержит строку подключения к базе данных PostgreSQL.
    /// </summary>
    static class Globaldata
    {
        /// <summary>
        /// Строка подключения к базе данных PostgreSQL.
        /// Используется для установки соединения с базой данных в других модулях.
        /// </summary>
        public static string connect = "Host=db;Port=5432;User ID=hdjiailzervpbz;Password=80518c42ccbd56573a6cf762e956804336da9831fc026ac79c66c767010d11ac;Database=d3454hmu58tfpn;Pooling=true;TrustServerCertificate=True;SSL Mode=Disable;";
    }
}
