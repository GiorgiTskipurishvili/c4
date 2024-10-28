//using ClinicAPI.DTOs;
//using ClinicAPI.Models;
//using Oracle.ManagedDataAccess.Client;
//using System.Data;

//namespace ClinicAPI.Package
//{
//    public class PKG_DOCTORS : PKG_BASE
//    {

//        public void add_doctor(DoctorRequest doctor)
//        {
//            using (var conn = new OracleConnection(ConnStr))
//            {
//                conn.Open();
//                using (var cmd = new OracleCommand("olerning.PKG_GIORGITSK_DOCTORS.add_doctor", conn))
//                {
//                    cmd.CommandType = CommandType.StoredProcedure;
//                    cmd.Parameters.Add("p_first_name", OracleDbType.Varchar2).Value = doctor.FirstName;
//                    cmd.Parameters.Add("p_last_name", OracleDbType.Varchar2).Value = doctor.LastName;
//                    cmd.Parameters.Add("p_email", OracleDbType.Varchar2).Value = doctor.Email;
//                    cmd.Parameters.Add("p_password", OracleDbType.Varchar2).Value = doctor.Password;
//                    cmd.Parameters.Add("p_personal_id", OracleDbType.Int64).Value = doctor.PersonalId;
//                    cmd.Parameters.Add("p_category", OracleDbType.Varchar2).Value = doctor.Category;
//                    cmd.Parameters.Add("p_photo", OracleDbType.Blob).Value = GetFileBytes(doctor.Photo);
//                    cmd.Parameters.Add("p_cv", OracleDbType.Blob).Value = GetFileBytes(doctor.CV);
//                    cmd.Parameters.Add("p_role", OracleDbType.Int32).Value = (int)doctor.Role;
//                    cmd.Parameters.Add("p_rating", OracleDbType.Int32).Value = doctor.Rating;

//                    cmd.ExecuteNonQuery();
//                }
//            }
//        }


//        private byte[] GetFileBytes(IFormFile file)
//        {
//            if (file == null)
//            {
//                return null;
//            }

//            using (var memoryStream = new MemoryStream())
//            {
//                file.CopyTo(memoryStream);
//                return memoryStream.ToArray();
//            }
//        }





//        public List<Doctor> get_doctors()
//        {
//            List<Doctor> doctors = new List<Doctor>();

//            OracleConnection conn = new OracleConnection();
//            conn.ConnectionString = ConnStr;

//            conn.Open();

//            OracleCommand cmd = new OracleCommand();
//            cmd.Connection = conn;
//            cmd.CommandText = "olerning.PKG_GIORGITSK_DOCTORS.get_doctors";
//            cmd.CommandType = CommandType.StoredProcedure;

//            cmd.Parameters.Add("p_result", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

//            OracleDataReader reader = cmd.ExecuteReader();

//            while (reader.Read())
//            {
//                Doctor doctor = new Doctor();
//                doctor.Id = int.Parse(reader["id"].ToString());
//                doctor.FirstName = reader["first_name"].ToString();
//                doctor.LastName = reader["last_name"].ToString();
//                doctor.Email = reader["email"].ToString();
//                doctor.Password = reader["password"].ToString();
//                doctor.PersonalId = long.Parse(reader["personal_id"].ToString());
//                doctor.Category = reader["category"].ToString();
//                //doctor.Photo = (byte[])reader["photo"]; // Ensure proper casting for BLOBs
//                //doctor.CV = (byte[])reader["cv"]; // Ensure proper casting for BLOBs
//                cmd.Parameters.Add("p_photo", OracleDbType.Blob).Value = doctor.Photo;
//                cmd.Parameters.Add("p_cv", OracleDbType.Blob).Value = doctor.CV;
//                doctor.Role = UserRole.User;
//                string ratingStr = reader["rating"].ToString();
//                if (!string.IsNullOrEmpty(ratingStr) && int.TryParse(ratingStr, out int rating))
//                {
//                    doctor.Rating = rating;
//                }
//                else
//                {
//                    doctor.Rating = 0; // Set a default value or handle this case appropriately
//                }

//                doctors.Add(doctor);
//            }

//            conn.Close();
//            return doctors;

//        }






//        public void delete_doctor(int doctorId)
//        {
//            using (var conn = new OracleConnection(ConnStr))
//            {
//                conn.Open();
//                using (var cmd = new OracleCommand("olerning.PKG_GIORGITSK_DOCTORS.delete_doctor", conn))
//                {
//                    cmd.CommandType = CommandType.StoredProcedure;
//                    cmd.Parameters.Add("p_id", OracleDbType.Int32).Value = doctorId;

//                    cmd.ExecuteNonQuery();
//                }
//            }
//        }


//        public void update_doctor(DoctorResponse doctorResponse)
//        {
//            using (var conn = new OracleConnection(ConnStr))
//            {
//                conn.Open();
//                using (var cmd = new OracleCommand("olerning.PKG_GIORGITSK_DOCTORS.update_doctor", conn))
//                {
//                    cmd.CommandType = CommandType.StoredProcedure;

//                    cmd.Parameters.Add("p_id", OracleDbType.Int32).Value = doctorResponse.Id;
//                    cmd.Parameters.Add("p_first_name", OracleDbType.Varchar2).Value = doctorResponse.FirstName;
//                    cmd.Parameters.Add("p_last_name", OracleDbType.Varchar2).Value = doctorResponse.LastName;
//                    cmd.Parameters.Add("p_email", OracleDbType.Varchar2).Value = doctorResponse.Email;
//                    cmd.Parameters.Add("p_password", OracleDbType.Varchar2).Value = doctorResponse.Password;
//                    cmd.Parameters.Add("p_personal_id", OracleDbType.Int64).Value = doctorResponse.PersonalId;
//                    cmd.Parameters.Add("p_category", OracleDbType.Varchar2).Value = doctorResponse.Category;
//                    cmd.Parameters.Add("p_rating", OracleDbType.Int32).Value = doctorResponse.Rating;

//                    cmd.ExecuteNonQuery();
//                }
//            }
//        }


//        public DoctorResponse get_doctor_by_id(int id)
//        {
//            DoctorResponse doctor = null;

//            using (var conn = new OracleConnection(ConnStr))
//            {
//                conn.Open();
//                using (var cmd = new OracleCommand("olerning.PKG_GIORGITSK_DOCTORS.get_doctor_by_id", conn))
//                {
//                    cmd.CommandType = CommandType.StoredProcedure;

//                    cmd.Parameters.Add("p_id", OracleDbType.Int32).Value = id;
//                    cmd.Parameters.Add("p_result", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

//                    using (OracleDataReader reader = cmd.ExecuteReader())
//                    {
//                        if (reader.Read())
//                        {
//                            doctor = new DoctorResponse
//                            {
//                                Id = int.Parse(reader["id"].ToString()),
//                                FirstName = reader["first_name"].ToString(),
//                                LastName = reader["last_name"].ToString(),
//                                Email = reader["email"].ToString(),
//                                Password = reader["password"].ToString(),
//                                PersonalId = long.Parse(reader["personal_id"].ToString()),
//                                Category = reader["category"].ToString(),
//                                Rating = int.Parse(reader["rating"].ToString())
//                            };
//                        }
//                    }
//                }
//            }

//            return doctor;
//        }





//    }
//}





using ClinicAPI.DTOs;
using ClinicAPI.Models;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace ClinicAPI.Package
{
    public class PKG_DOCTORS : PKG_BASE
    {
        public void add_doctor(DoctorRequest doctor)
        {
            using (var conn = new OracleConnection(ConnStr))
            {
                conn.Open();
                using (var cmd = new OracleCommand("olerning.PKG_GIORGITSK_DOCTORS.add_doctor", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_first_name", OracleDbType.Varchar2).Value = doctor.FirstName;
                    cmd.Parameters.Add("p_last_name", OracleDbType.Varchar2).Value = doctor.LastName;
                    cmd.Parameters.Add("p_email", OracleDbType.Varchar2).Value = doctor.Email;
                    cmd.Parameters.Add("p_password", OracleDbType.Varchar2).Value = doctor.Password;
                    cmd.Parameters.Add("p_personal_id", OracleDbType.Int64).Value = doctor.PersonalId;
                    cmd.Parameters.Add("p_category", OracleDbType.Varchar2).Value = doctor.Category;
                    cmd.Parameters.Add("p_photo", OracleDbType.Blob).Value = GetFileBytes(doctor.Photo);
                    cmd.Parameters.Add("p_cv", OracleDbType.Blob).Value = GetFileBytes(doctor.CV);
                    cmd.Parameters.Add("p_role", OracleDbType.Int32).Value = (int)doctor.Role;
                    cmd.Parameters.Add("p_rating", OracleDbType.Int32).Value = doctor.Rating;

                    cmd.ExecuteNonQuery();
                }
            }
        }


        private byte[] GetFileBytes(IFormFile file)
        {
            if (file == null)
            {
                return null;
            }

            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

        public List<Doctor> get_doctors()
        {
            List<Doctor> doctors = new List<Doctor>();
            using (var conn = new OracleConnection(ConnStr))
            {
                conn.Open();
                using (var cmd = new OracleCommand("olerning.PKG_GIORGITSK_DOCTORS.get_doctors", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_result", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Doctor doctor = new Doctor
                            {
                                Id = int.Parse(reader["id"].ToString()),
                                FirstName = reader["first_name"].ToString(),
                                LastName = reader["last_name"].ToString(),
                                Email = reader["email"].ToString(),
                                Password = reader["password"].ToString(),
                                PersonalId = long.Parse(reader["personal_id"].ToString()),
                                Category = reader["category"].ToString(),
                                Rating = int.Parse(reader["rating"].ToString())
                            };
                            doctors.Add(doctor);
                        }
                    }
                }
            }
            return doctors;
        }

        public void delete_doctor(int doctorId)
        {
            using (var conn = new OracleConnection(ConnStr))
            {
                conn.Open();
                using (var cmd = new OracleCommand("olerning.PKG_GIORGITSK_DOCTORS.delete_doctor", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_id", OracleDbType.Int32).Value = doctorId;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        //public void update_doctor(DoctorResponse doctorResponse)
        //{
        //    using (var conn = new OracleConnection(ConnStr))
        //    {
        //        conn.Open();
        //        using (var cmd = new OracleCommand("olerning.PKG_GIORGITSK_DOCTORS.update_doctor", conn))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.Add("p_id", OracleDbType.Int32).Value = doctorResponse.Id;
        //            cmd.Parameters.Add("p_first_name", OracleDbType.Varchar2).Value = doctorResponse.FirstName;
        //            cmd.Parameters.Add("p_last_name", OracleDbType.Varchar2).Value = doctorResponse.LastName;
        //            cmd.Parameters.Add("p_email", OracleDbType.Varchar2).Value = doctorResponse.Email;
        //            cmd.Parameters.Add("p_password", OracleDbType.Varchar2).Value = doctorResponse.Password;
        //            cmd.Parameters.Add("p_personal_id", OracleDbType.Int64).Value = doctorResponse.PersonalId;
        //            cmd.Parameters.Add("p_category", OracleDbType.Varchar2).Value = doctorResponse.Category;
        //            cmd.Parameters.Add("p_rating", OracleDbType.Int32).Value = doctorResponse.Rating;

        //            cmd.ExecuteNonQuery();
        //        }
        //    }
        //}

        //public void update_doctor(Doctor doctor)
        //{
        //    using (OracleConnection conn = new OracleConnection(ConnStr))
        //    {
        //        conn.Open();

        //        using (OracleCommand cmd = new OracleCommand("olerning.PKG_GIORGITSK_DOCTORS.update_doctor", conn))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;

        //            cmd.Parameters.Add("p_id", OracleDbType.Int32).Value = doctor.Id;
        //            cmd.Parameters.Add("p_first_name", OracleDbType.Varchar2).Value = doctor.FirstName;
        //            cmd.Parameters.Add("p_last_name", OracleDbType.Varchar2).Value = doctor.LastName;
        //            cmd.Parameters.Add("p_email", OracleDbType.Varchar2).Value = doctor.Email;
        //            cmd.Parameters.Add("p_password", OracleDbType.Varchar2).Value = doctor.Password;
        //            cmd.Parameters.Add("p_personal_id", OracleDbType.Int32).Value = doctor.PersonalId;
        //            cmd.Parameters.Add("p_category", OracleDbType.Varchar2).Value = doctor.Category;
        //            cmd.Parameters.Add("p_role", OracleDbType.Int32).Value = doctor.Role;
        //            cmd.Parameters.Add("p_rating", OracleDbType.Int32).Value = doctor.Rating;

        //            try
        //            {
        //                cmd.ExecuteNonQuery();
        //            }
        //            catch (OracleException ex)
        //            {
        //                // Handle Oracle specific exceptions
        //                Console.WriteLine($"Oracle Error: {ex.Message}");
        //            }
        //            catch (Exception ex)
        //            {
        //                // Handle general exceptions
        //                Console.WriteLine($"Error: {ex.Message}");
        //            }
        //        }
        //    }
        //}

        //public void update_doctor(DoctorResponse doctor)
        //{
        //    using (OracleConnection conn = new OracleConnection(ConnStr))
        //    {
        //        conn.Open();

        //        using (OracleCommand cmd = new OracleCommand("olerning.PKG_GIORGITSK_BOOKINGS.update_booking", conn))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;

        //            cmd.Parameters.Add("p_id", OracleDbType.Int64).Value = doctor.Id;
        //            cmd.Parameters.Add("p_first_name", OracleDbType.Int64).Value = doctor.FirstName;
        //            cmd.Parameters.Add("p_last_name", OracleDbType.Int64).Value = doctor.LastName;
        //            cmd.Parameters.Add("p_email", OracleDbType.Varchar2).Value = doctor.Email;
        //            cmd.Parameters.Add("p_password", OracleDbType.Date).Value = doctor.Password;
        //            cmd.Parameters.Add("p_personal_id", OracleDbType.Date).Value = doctor.Password;
        //            cmd.Parameters.Add("p_category", OracleDbType.Date).Value = doctor.Category;
        //            cmd.Parameters.Add("p_rating", OracleDbType.Date).Value = doctor.Rating;

        //            cmd.ExecuteNonQuery();
        //        }
        //    }
        //}

        public void update_doctor(DoctorResponse doctor)
        {
            using (OracleConnection conn = new OracleConnection(ConnStr))
            {
                conn.Open();

                using (OracleCommand cmd = new OracleCommand("olerning.PKG_GIORGITSK_DOCTORS.update_doctor", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("p_id", OracleDbType.Int64).Value = doctor.Id;
                    cmd.Parameters.Add("p_first_name", OracleDbType.Varchar2).Value = doctor.FirstName;
                    cmd.Parameters.Add("p_last_name", OracleDbType.Varchar2).Value = doctor.LastName;
                    cmd.Parameters.Add("p_email", OracleDbType.Varchar2).Value = doctor.Email;
                    cmd.Parameters.Add("p_password", OracleDbType.Varchar2).Value = doctor.Password; // Ensure this is correct as per your DB schema
                    cmd.Parameters.Add("p_personal_id", OracleDbType.Int64).Value = doctor.PersonalId;
                    cmd.Parameters.Add("p_category", OracleDbType.Varchar2).Value = doctor.Category;
                    cmd.Parameters.Add("p_rating", OracleDbType.Int32).Value = doctor.Rating;

                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (OracleException ex)
                    {
                        Console.WriteLine($"Oracle Error: {ex.Message}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
            }
        }




        public DoctorResponse get_doctor_by_id(int id)
        {
            DoctorResponse doctor = null;
            using (var conn = new OracleConnection(ConnStr))
            {
                conn.Open();
                using (var cmd = new OracleCommand("olerning.PKG_GIORGITSK_DOCTORS.get_doctor_by_id", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_id", OracleDbType.Int32).Value = id;
                    cmd.Parameters.Add("p_result", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            doctor = new DoctorResponse
                            {
                                Id = int.Parse(reader["id"].ToString()),
                                FirstName = reader["first_name"].ToString(),
                                LastName = reader["last_name"].ToString(),
                                Email = reader["email"].ToString(),
                                Password = reader["password"].ToString(),
                                PersonalId = long.Parse(reader["personal_id"].ToString()),
                                Category = reader["category"].ToString(),
                                Rating = int.Parse(reader["rating"].ToString())
                            };
                        }
                    }
                }
            }
            return doctor;
        }

        public void update_photo(int doctorId, IFormFile photo)
        {
            using (var conn = new OracleConnection(ConnStr))
            {
                conn.Open();
                using (var cmd = new OracleCommand("olerning.PKG_GIORGITSK_DOCTORS.update_photo", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_id", OracleDbType.Int32).Value = doctorId;
                    cmd.Parameters.Add("p_photo", OracleDbType.Blob).Value = GetFileBytes(photo);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public byte[] get_photo_by_doctor_id(int doctorId)
        {
            byte[] photo = null;
            using (var conn = new OracleConnection(ConnStr))
            {
                conn.Open();
                using (var cmd = new OracleCommand("olerning.PKG_GIORGITSK_DOCTORS.get_photo_by_doctor_id", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_id", OracleDbType.Int32).Value = doctorId;
                    cmd.Parameters.Add("p_result", OracleDbType.Blob).Direction = ParameterDirection.Output;

                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            photo = reader["photo"] as byte[];
                        }
                    }
                }
            }
            return photo;
        }

        public void update_cv(int doctorId, IFormFile cv)
        {
            using (var conn = new OracleConnection(ConnStr))
            {
                conn.Open();
                using (var cmd = new OracleCommand("olerning.PKG_GIORGITSK_DOCTORS.update_cv", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_id", OracleDbType.Int32).Value = doctorId;
                    cmd.Parameters.Add("p_cv", OracleDbType.Blob).Value = GetFileBytes(cv);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public byte[] get_cv_by_doctor_id(int doctorId)
        {
            byte[] cv = null;
            using (var conn = new OracleConnection(ConnStr))
            {
                conn.Open();
                using (var cmd = new OracleCommand("olerning.PKG_GIORGITSK_DOCTORS.get_cv_by_doctor_id", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_id", OracleDbType.Int32).Value = doctorId;
                    cmd.Parameters.Add("p_result", OracleDbType.Blob).Direction = ParameterDirection.Output;

                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            cv = reader["cv"] as byte[];
                        }
                    }
                }
            }
            return cv;
        }
    }
}
