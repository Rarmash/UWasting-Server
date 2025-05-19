using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server
{
    /// <summary>
    /// Класс <c>User</c> представляет пользователя приложения UWasting.
    /// </summary>
    [Serializable]
    public class User
    {
        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Фамилия пользователя.
        /// </summary>
        public string surname { get; set; }

        /// <summary>
        /// Электронная почта пользователя (логин).
        /// </summary>
        public string email { get; set; }

        /// <summary>
        /// Уникальный идентификатор пользователя.
        /// </summary>
        public int id { get; set; }
    }
}
