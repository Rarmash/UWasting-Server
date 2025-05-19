using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Database;
namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        [Route("/RegistrateUser")]
        [HttpGet]
        public IActionResult RegistrateUser(string login, string password, string name, string surname)
        {
            string salt = PasswordHasher.GenerateSalt(); // генерируем соль
            string hashedPassword = PasswordHasher.HashPassword(password, salt); // хэшируем с солью

            var tmp = Database.Registration.Registrate(login, hashedPassword, name, surname, salt); // вызываем обновлённую функцию

            if (tmp.id == -1) return BadRequest();

            User user = new User
            {
                name = tmp.Name,
                surname = tmp.Surname,
                email = tmp.email,
                id = tmp.id
            };

            return Ok(user);
        }


        [Route("/GetByLoginAndPassword")]
        [HttpGet]
        public IActionResult GetByLoginAndPassword(string login, string password)
        {
            using var myCon = new Npgsql.NpgsqlConnection(Globaldata.connect);
            myCon.Open();

            var cmd = new Npgsql.NpgsqlCommand("SELECT \"id\", \"Login\", \"Name\", \"Surname\", \"Password\", \"Salt\" FROM \"Users\" WHERE \"Login\" = @login", myCon);
            cmd.Parameters.AddWithValue("@login", login);

            using var reader = cmd.ExecuteReader();

            if (!reader.Read())
                return BadRequest(); // Пользователь не найден

            int userId = reader.GetInt32(0);
            string email = reader.GetString(1);
            string name = reader.GetString(2);
            string surname = reader.GetString(3);
            string storedHashedPassword = reader.GetString(4);
            string storedSalt = reader.GetString(5);

            string hashedInputPassword = PasswordHasher.HashPassword(password, storedSalt);

            if (hashedInputPassword != storedHashedPassword)
                return BadRequest(); // Пароль неверный

            User user = new User
            {
                id = userId,
                email = email,
                name = name,
                surname = surname
            };

            return Ok(user);
        }

        [Route("/FindLoginInDB")]
        [HttpGet]
        public IActionResult FindLoginInDB(string login)
        {
            return Ok(Database.Registration.CheckingUser(login));
        }
        [Route("/СhangeNameSurname")]
        [HttpGet]
        public IActionResult СhangeNameSurname(int id, string name, string surname)
        {
            return Ok(Database.Registration.ChangeNameSurname(id, name, surname));
        }
        [Route("/ChangeLogin")]
        [HttpGet]
        public IActionResult ChangeLogin(int id, string login)
        {
            if (Database.Registration.CheckingUser(login)) return BadRequest();
            return Ok(Database.Registration.ChangeLogin(id, login));
        }

        [Route("/ChangePassword")]
        [HttpGet]
        public IActionResult ChangePassword(int id, string newPassword)
        {
            string newSalt = PasswordHasher.GenerateSalt();
            string hashedPassword = PasswordHasher.HashPassword(newPassword, newSalt);

            bool result = Database.Registration.ChangePassword(id, hashedPassword, newSalt);
            if (!result)
                return BadRequest();

            return Ok();
        }

    }
}
