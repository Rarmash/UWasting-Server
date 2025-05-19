using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server
{
    /// <summary>
    /// Класс <c>Operation</c> представляет одну финансовую операцию — доход или расход.
    /// </summary>
    public class Operation
    {
        /// <summary>
        /// Уникальный идентификатор операции.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название категории (например, "Продукты", "Зарплата").
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Сумма операции.
        /// Положительное значение — доход, отрицательное — расход.
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// Дата проведения операции.
        /// </summary>
        public DateTime date { get; set; }
    }
}
