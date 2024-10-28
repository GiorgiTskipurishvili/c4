using ClinicAPI.DTOs;
using ClinicAPI.Models;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Numerics;

namespace ClinicAPI.Package
{
    public class PKG_USERS : PKG_BASE
    {
        public void add_user(UserRequest userRequest)
        {
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = ConnStr;
            conn.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "olerning.PKG_GIORGITSK_USERS.add_user";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("p_first_name", OracleDbType.Varchar2).Value = userRequest.FirstName;
            cmd.Parameters.Add("p_last_name", OracleDbType.Varchar2).Value = userRequest.LastName;
            cmd.Parameters.Add("p_email", OracleDbType.Varchar2).Value = userRequest.Email;
            cmd.Parameters.Add("p_password", OracleDbType.Varchar2).Value = userRequest.Password;
            cmd.Parameters.Add("p_personal_id", OracleDbType.Int64).Value = userRequest.PersonalId;
            cmd.Parameters.Add("p_role", OracleDbType.Int64).Value = userRequest.Role;

            cmd.ExecuteNonQuery();

            conn.Close();
        }

        public List<UserResponse> get_users()
        {
            List<UserResponse> users = new List<UserResponse>();

            using (OracleConnection conn = new OracleConnection(ConnStr))
            {
                conn.Open();

                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "olerning.PKG_GIORGITSK_USERS.get_users";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_result", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            UserResponse userResponse = new UserResponse
                            {
                                Id = int.Parse(reader["id"].ToString()),
                                FirstName = reader["first_name"].ToString(),
                                LastName = reader["last_name"].ToString(),
                                Email = reader["email"].ToString(),
                                // Omit password for security
                                PersonalId = long.Parse(reader["personal_id"].ToString())
                            };

                            users.Add(userResponse);
                        }
                    }
                }
            }

            return users;
        }


        public void delete_user(User user)
        {
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = ConnStr;

            conn.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "olerning.PKG_GIORGITSK_USERS.delete_user";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("p_id", OracleDbType.Int64).Value = user.Id;

            cmd.ExecuteNonQuery();

            conn.Close();
        }


        public void update_user(User user)
        {
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = ConnStr;

            conn.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "olerning.PKG_GIORGITSK_USERS.update_user";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("p_id", OracleDbType.Int64).Value = user.Id;
            cmd.Parameters.Add("p_first_name", OracleDbType.Varchar2).Value = user.FirstName;
            cmd.Parameters.Add("p_last_name", OracleDbType.Varchar2).Value = user.LastName;
            cmd.Parameters.Add("p_email", OracleDbType.Varchar2).Value = user.Email;
            cmd.Parameters.Add("p_password", OracleDbType.Varchar2).Value = user.Password;
            cmd.Parameters.Add("p_personal_id", OracleDbType.Int32).Value = user.PersonalId;
            cmd.Parameters.Add("p_role", OracleDbType.Int64).Value = user.Role;

            cmd.ExecuteNonQuery();
            conn.Close();

        }


        public UserResponse get_user_by_id(int userId)
        {
            UserResponse userResponse = null;

            using (OracleConnection conn = new OracleConnection(ConnStr))
            {
                conn.Open();

                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "olerning.PKG_GIORGITSK_USERS.get_user_by_id";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("p_id", OracleDbType.Int32).Value = userId;
                    cmd.Parameters.Add("p_result", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            userResponse = new UserResponse
                            {
                                Id = int.Parse(reader["id"].ToString()),
                                FirstName = reader["first_name"].ToString(),
                                LastName = reader["last_name"].ToString(),
                                Email = reader["email"].ToString(),
                                // Omit password for security
                                PersonalId = long.Parse(reader["personal_id"].ToString()),
                                // Map role if necessary
                            };
                        }
                    }
                }
            }

            return userResponse;
        }

    }
}
