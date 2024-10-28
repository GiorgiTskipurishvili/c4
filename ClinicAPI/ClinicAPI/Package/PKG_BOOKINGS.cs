//using ClinicAPI.Models;
//using Oracle.ManagedDataAccess.Client;
//using System.Data;
//using System.Numerics;

//namespace ClinicAPI.Package
//{
//    public class PKG_BOOKINGS:PKG_BASE
//    {
//        public void add_booking(Booking booking)
//        {
//            OracleConnection conn = new OracleConnection();
//            conn.ConnectionString = ConnStr;
//            conn.Open();


//            OracleCommand cmd = new OracleCommand();
//            cmd.Connection = conn;
//            cmd.CommandText = "olerning.PKG_GIORGITSK_BOOKINGS.add_booking";
//            cmd.CommandType = CommandType.Text;

//            cmd.Parameters.Add("p_user_id", OracleDbType.Int64).Value = booking.UserId;
//            cmd.Parameters.Add("p_doctor_id", OracleDbType.Int64).Value = booking.DoctorId;
//            cmd.Parameters.Add("p_description", OracleDbType.Varchar2).Value = booking.Description;
//            cmd.Parameters.Add("p_booking_time", OracleDbType.Date).Value = booking.CreateBookingTime;

//            cmd.ExecuteNonQuery();
//            conn.Close();
//        }

//        public List<Booking> get_bookings()
//        {
//            List<Booking> bookings = new List<Booking>();

//            OracleConnection conn = new OracleConnection();
//            conn.ConnectionString = ConnStr;

//            conn.Open();

//            OracleCommand cmd = new OracleCommand();
//            cmd.Connection = conn;
//            cmd.CommandText = "olerning.PKG_GIORGITSK_BOOKINGS.get_bookings";
//            cmd.CommandType = CommandType.StoredProcedure;

//            cmd.Parameters.Add("p_result", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

//            OracleDataReader reader = cmd.ExecuteReader();

//            while (reader.Read())
//            {
//                Booking booking = new Booking();
//                booking.Id = int.Parse(reader["id"].ToString());
//                booking.UserId =int.Parse(reader["user_id"].ToString());
//                booking.DoctorId = int.Parse(reader["doctor_id"].ToString());
//                booking.Description = reader["description"].ToString();
//                booking.CreateBookingTime = reader.GetDateTime(reader.GetOrdinal("booking_time"));

//                bookings.Add(booking);
//            }

//            conn.Close();
//            return bookings;

//        }




//        public void delete_booking(Booking booking)
//        {
//            OracleConnection conn = new OracleConnection();
//            conn.ConnectionString = ConnStr;
//            conn.Open();


//            OracleCommand cmd = new OracleCommand();
//            cmd.Connection = conn;
//            cmd.CommandText = "olerning.PKG_GIORGITSK_BOOKINGS.delete_booking";
//            cmd.CommandType = CommandType.StoredProcedure;

//            cmd.Parameters.Add("p_id", OracleDbType.Int64).Value = booking.Id;

//            cmd.ExecuteNonQuery();

//            conn.Close();
//        }


//        public void update_booking(Booking booking)
//        {
//            OracleConnection conn = new OracleConnection();
//            conn.ConnectionString = ConnStr;
//            conn.Open();

//            OracleCommand cmd = new OracleCommand();
//            cmd.Connection = conn;
//            cmd.CommandText = "olerning.PKG_GIORGITSK_BOOKINGS.update_booking";
//            cmd.CommandType = CommandType.StoredProcedure;

//            cmd.Parameters.Add("p_id", OracleDbType.Int64).Value=booking.Id;
//            cmd.Parameters.Add("p_user_id", OracleDbType.Int64).Value = booking.UserId;
//            cmd.Parameters.Add("p_doctor_id", OracleDbType.Int64).Value = booking.DoctorId;
//            cmd.Parameters.Add("p_description", OracleDbType.Varchar2).Value = booking.Description;
//            cmd.Parameters.Add("p_booking_time", OracleDbType.Date).Value = booking.CreateBookingTime;

//            cmd.ExecuteNonQuery();
//            conn.Close();
//        }


//        public void get_booking_by_id(Booking booking)
//        {
//            OracleConnection conn = new OracleConnection();
//            conn.ConnectionString = ConnStr;
//            conn.Open();

//            OracleCommand cmd = new OracleCommand();
//            cmd.Connection = conn;
//            cmd.CommandText = "olerning.PKG_GIORGITSK_BOOKINGS.get_booking_by_id";
//            cmd.CommandType = CommandType.StoredProcedure;

//            cmd.Parameters.Add("p_id", OracleDbType.Int64).Value = booking.Id;
//            cmd.Parameters.Add("p_result", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

//            OracleDataReader reader = cmd.ExecuteReader();

//            if (reader.Read())
//            {
//                booking.Id = int.Parse(reader["id"].ToString());
//                booking.UserId = int.Parse(reader["user_id"].ToString());
//                booking.DoctorId = int.Parse(reader["doctor_id"].ToString());
//                booking.Description = reader["description"].ToString();
//                booking.CreateBookingTime = reader.GetDateTime(reader.GetOrdinal("booking_time"));
//            }

//        }




//    }
//}



using ClinicAPI.DTOs;
using ClinicAPI.Models;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace ClinicAPI.Package
{
    public class PKG_BOOKINGS : PKG_BASE
    {
        public void add_booking(BookingRequest bookingRequest)
        {
            using (OracleConnection conn = new OracleConnection(ConnStr))
            {
                conn.Open();

                using (OracleCommand cmd = new OracleCommand("olerning.PKG_GIORGITSK_BOOKINGS.add_booking", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("p_user_id", OracleDbType.Int64).Value = bookingRequest.UserId;
                    cmd.Parameters.Add("p_doctor_id", OracleDbType.Int64).Value = bookingRequest.DoctorId;
                    cmd.Parameters.Add("p_description", OracleDbType.Varchar2).Value = bookingRequest.Description;
                    cmd.Parameters.Add("p_booking_time", OracleDbType.Date).Value = bookingRequest.CreateBookingTime;

                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (OracleException ex)
                    {
                        // Handle exception, log it or rethrow
                        throw new Exception("Error adding booking", ex);
                    }
                }
            }
        }

        public List<Booking> get_bookings()
        {
            List<Booking> bookings = new List<Booking>();

            using (OracleConnection conn = new OracleConnection(ConnStr))
            {
                conn.Open();

                using (OracleCommand cmd = new OracleCommand("olerning.PKG_GIORGITSK_BOOKINGS.get_bookings", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_result", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Booking booking = new Booking
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id")),
                                UserId = reader.GetInt32(reader.GetOrdinal("user_id")),
                                DoctorId = reader.GetInt32(reader.GetOrdinal("doctor_id")),
                                Description = reader["description"].ToString(),
                                CreateBookingTime = reader.GetDateTime(reader.GetOrdinal("booking_time"))
                            };

                            bookings.Add(booking);
                        }
                    }
                }
            }

            return bookings;
        }

        public void delete_booking(int bookingId)
        {
            using (OracleConnection conn = new OracleConnection(ConnStr))
            {
                conn.Open();

                using (OracleCommand cmd = new OracleCommand("olerning.PKG_GIORGITSK_BOOKINGS.delete_booking", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_id", OracleDbType.Int64).Value = bookingId;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void update_booking(Booking booking)
        {
            using (OracleConnection conn = new OracleConnection(ConnStr))
            {
                conn.Open();

                using (OracleCommand cmd = new OracleCommand("olerning.PKG_GIORGITSK_BOOKINGS.update_booking", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("p_id", OracleDbType.Int64).Value = booking.Id;
                    cmd.Parameters.Add("p_user_id", OracleDbType.Int64).Value = booking.UserId;
                    cmd.Parameters.Add("p_doctor_id", OracleDbType.Int64).Value = booking.DoctorId;
                    cmd.Parameters.Add("p_description", OracleDbType.Varchar2).Value = booking.Description;
                    cmd.Parameters.Add("p_booking_time", OracleDbType.Date).Value = booking.CreateBookingTime;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Booking get_booking_by_id(int bookingId)
        {
            Booking booking = null;

            using (OracleConnection conn = new OracleConnection(ConnStr))
            {
                conn.Open();

                using (OracleCommand cmd = new OracleCommand("olerning.PKG_GIORGITSK_BOOKINGS.get_booking_by_id", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("p_id", OracleDbType.Int64).Value = bookingId;
                    cmd.Parameters.Add("p_result", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            booking = new Booking
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id")),
                                UserId = reader.GetInt32(reader.GetOrdinal("user_id")),
                                DoctorId = reader.GetInt32(reader.GetOrdinal("doctor_id")),
                                Description = reader["description"].ToString(),
                                CreateBookingTime = reader.GetDateTime(reader.GetOrdinal("booking_time"))
                            };
                        }
                    }
                }
            }

            return booking;
        }
    }
}
