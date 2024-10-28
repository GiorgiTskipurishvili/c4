namespace ClinicAPI.Models
{
    public class User
    {

        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public long? PersonalId { get; set; }

        public UserRole? Role  {  get; set; }

    }

    public enum UserRole
    {
        Admin = 0,
        Doctor = 1,
        User = 2
    }


}
