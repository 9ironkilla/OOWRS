using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OOWRS.Models;
using System.Data.SqlClient;
using System.Configuration;

namespace OOWRS.Repository
{
    public interface IUserRepository
    {
        UserModel FindUser(UserModel user);

    }

    public class UserRepository : IUserRepository
    {


        private SqlConnection _UserConnection;
        private string _FindUserQuery = "  select * from [Users] where [Name] = @name and [Password] = @password";



        public UserRepository()
        {

            _UserConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionStringName"].ConnectionString);
        }


        public UserModel FindUser(UserModel user)
        {
            using (_UserConnection)
            {
                SqlCommand command = new SqlCommand(_FindUserQuery, _UserConnection);
                command.Parameters.AddWithValue("@name", user.Name);
                command.Parameters.AddWithValue("@password", user.Password);

                try
                {
                    _UserConnection.Open();
                    var reader = command.ExecuteReader();

                    if (reader != null) return user;

                }
                catch (SqlException e)
                {
                    Console.Out.Write($"SQL Exception {e.Message}");
                    return new UserModel();
                }
                finally
                {
                    _UserConnection.Close();
                }


            }
            return new UserModel();

        
        }

    }
        
    
}