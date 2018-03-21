using MySql.Data.MySqlClient;

using System;
using System.Collections.Generic;

namespace DotnetCoreServer.Models
{
    public interface IUserDao{
        User FindUserByFUID(string FacebookID);
        User GetUser(long UserID);
        User InsertUser(User user);
        bool UpdateUser(User user);
    }

    public class UserDao : IUserDao
    {
        public IDB db {get;}

        public UserDao(IDB db){
            this.db = db;
        }

        public User FindUserByFUID(string FacebookID){
            User user = new User();
            using(MySqlConnection conn = db.GetConnection())
            {   
                string query = String.Format(
                    "SELECT user_id, facebook_id, facebook_name, facebook_photo_url, created_at, access_token FROM tb_user WHERE facebook_id = '{0}'",
                     FacebookID);

                Console.WriteLine(query);

                using(MySqlCommand cmd = (MySqlCommand)conn.CreateCommand())
                {
                    cmd.CommandText = query;
                    using (MySqlDataReader reader = (MySqlDataReader)cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user.UserID = reader.GetInt64(0);
                            user.FacebookID = reader.GetString(1);
                            user.FacebookName = reader.GetString(2);
                            user.FacebookPhotoURL = reader.GetString(3);
                            user.CreatedAt = reader.GetDateTime(4);
                            user.AccessToken = reader.GetString(5);
                            return user;
                        }
                    }
                }
                conn.Close();
            }
            return null;
        }
        
        public User GetUser(long UserID){
            User user = new User();
            using(MySqlConnection conn = db.GetConnection())
            {   
                string query = String.Format(
                    @"
                    SELECT 
                        user_id, facebook_id, facebook_name, 
                        facebook_photo_url, created_at, 
                        access_token, statpoint, str, wis, con, dex, level, experience, expmax, money
                    FROM tb_user 
                    WHERE user_id = {0}",
                     UserID);

                Console.WriteLine(query);

                using(MySqlCommand cmd = (MySqlCommand)conn.CreateCommand())
                {
                    cmd.CommandText = query;
                    using (MySqlDataReader reader = (MySqlDataReader)cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user.UserID = reader.GetInt64(0);
                            user.FacebookID = reader.GetString(1);
                            user.FacebookName = reader.GetString(2);
                            user.FacebookPhotoURL = reader.GetString(3);
                            user.CreatedAt = reader.GetDateTime(4);
                            user.AccessToken = reader.GetString(5);

                            user.StatPoint = reader.GetInt32(6);
                            user.Str = reader.GetInt32(7);
                            user.Int = reader.GetInt32(8);
                            user.Con = reader.GetInt32(9);
                            user.Dex = reader.GetInt32(10);
                            user.Level = reader.GetInt32(11);
                            user.Experience = reader.GetInt32(12);
                            user.MaxExperience = reader.GetInt32(13);
                            user.Money = reader.GetInt32(14);

                        }
                    }
                }
                
                conn.Close();
            }
            return user;
        }

        public User InsertUser(User user){
            
            string query = String.Format(
                "INSERT INTO tb_user (facebook_id, facebook_name, facebook_photo_url, access_token, created_at) VALUES ('{0}','{1}','{2}','{3}', now())",
                    user.FacebookID, user.FacebookName, user.FacebookPhotoURL, user.AccessToken);

            Console.WriteLine(query);

            using(MySqlConnection conn = db.GetConnection())
            using(MySqlCommand cmd = (MySqlCommand)conn.CreateCommand())
            {

                cmd.CommandText = query;
                cmd.ExecuteNonQuery();

                conn.Close();
            }

        
            return user;
        }

        public bool UpdateUser(User user){
            using(MySqlConnection conn = db.GetConnection())
            {
                string query = String.Format(
                    @"
                    UPDATE tb_user SET 
                        statpoint = {0}, str= {1}, wis = {2}, con = {3}, dex = {4}, level = {5}, experience = {6}, expmax = {7}, money = {8}
                    WHERE user_id = {9}
                    ",
                user.StatPoint, user.Str, user.Int, user.Con, user.Dex, user.Level, user.Experience, user.MaxExperience, user.Money, user.UserID
                     );

                Console.WriteLine(query);
                
                using(MySqlCommand cmd = (MySqlCommand)conn.CreateCommand())
                {
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                    
                }

                conn.Close();
            }
            return true;
        }



    }
}
