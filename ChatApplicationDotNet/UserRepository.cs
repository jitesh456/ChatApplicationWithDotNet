using ChatModelLayer;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;

namespace ChatApplicationDotNet
{

    public class UserRepository:IUserRepository
    {
        private readonly string connectionString;

        public IConfiguration Configuration { get; }
        private SqlConnection con;

        public UserRepository(IConfiguration configuration)
        {
            Configuration = configuration;
            con = null;
            connectionString = Configuration.GetSection("DBConnection").GetSection("ConnectionString").Value;
        }

        public Boolean AddUser(UserDetails userDetails)
        {            
            try
            {
                con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("spAddUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FirstName", userDetails.FirstName);
                cmd.Parameters.AddWithValue("@LastName", userDetails.LastName);
                cmd.Parameters.AddWithValue("@Email", userDetails.Email);
                cmd.Parameters.AddWithValue("@Password", userDetails.Password);
                cmd.Parameters.AddWithValue("@PhoneNumber", userDetails.PhoneNumber);
                con.Open();

                int result = cmd.ExecuteNonQuery();

                if (result > 0)
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                con.Close();
            }
            return false;
        }
    }
}
