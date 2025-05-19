using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Npgsql;
using System.Text;
using System.Threading.Tasks;

namespace Database
{

    public static class Registration
    {
        public static bool CheckingUser(string login)
        {
            using var myCon = new NpgsqlConnection(Globaldata.connect);
            myCon.Open();
            var cmd = new NpgsqlCommand("SELECT \"Login\" FROM \"Users\" WHERE \"Login\" = @log", myCon)
            {
                Parameters =
                {
                     new("@log", login),
                }
            };

            return cmd.ExecuteReader().HasRows;
        }
        public static (int id, string email, string Name, string Surname) Registrate(string login, string password, string name, string surname, string salt)
        {
            using var myCon = new NpgsqlConnection(Globaldata.connect);
            myCon.Open();

            if (CheckingUser(login))
                return (-1, "", "", "");

            var cmd = new NpgsqlCommand("INSERT INTO \"Users\" (\"Login\", \"Password\", \"Name\", \"Surname\", \"Salt\") VALUES (@log, @pass, @name, @surname, @salt)", myCon)
            {
                Parameters =
        {
            new("@log", login),
            new("@pass", password),
            new("@name", name),
            new("@surname", surname),
            new("@salt", salt)
        }
            };
            cmd.ExecuteNonQuery();

            cmd = new NpgsqlCommand("SELECT \"id\", \"Login\", \"Name\", \"Surname\" FROM \"Users\" WHERE \"Login\" = @log", myCon)
            {
                Parameters =
        {
            new("@log", login)
        }
            };

            using (var reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int UserId = Convert.ToInt32(reader[0]);
                        string email = reader[1].ToString(), Name = reader[2].ToString(), Surname = reader[3].ToString();
                        return (UserId, email, Name, Surname);
                    }
                }
            }

            return (-1, "", "", "");
        }

        public static bool ChangeNameSurname(int id, string name, string surname)
        {
            using var myCon = new NpgsqlConnection(Globaldata.connect);
            myCon.Open();

            var cmd = new NpgsqlCommand("UPDATE \"Users\" SET \"Name\" = @name, \"Surname\" = @surname WHERE \"id\" = @id", myCon)
            {
                Parameters =
                {
                    new("@name", name),
                    new("@surname", surname),
                    new("@id", id)
                }
            };
            cmd.ExecuteNonQuery();
            return true;
        }
        public static bool ChangeLogin(int id, string login)
        {
            using var myCon = new NpgsqlConnection(Globaldata.connect);
            myCon.Open();

            var cmd = new NpgsqlCommand("UPDATE \"Users\" SET \"Login\" = @login WHERE \"id\" = @id", myCon)
            {
                Parameters =
                {
                    new("@login", login),
                    new("@id", id)
                }
            };
            cmd.ExecuteNonQuery();
            return true;
        }
        public static bool ChangePassword(int id, string hashedPassword, string salt)
        {
            using var myCon = new NpgsqlConnection(Globaldata.connect);
            myCon.Open();

            var cmd = new NpgsqlCommand("UPDATE \"Users\" SET \"Password\" = @password, \"Salt\" = @salt WHERE \"id\" = @id", myCon)
            {
                Parameters = {
            new("@password", hashedPassword),
            new("@salt", salt),
            new("@id", id)
        }
            };

            int affectedRows = cmd.ExecuteNonQuery();
            return affectedRows > 0;
        }

    }
}