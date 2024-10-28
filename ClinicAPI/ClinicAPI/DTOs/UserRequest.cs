using ClinicAPI.Models;

namespace ClinicAPI.DTOs
{
    public class UserRequest
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public long? PersonalId { get; set; }

        public UserRole? Role { get; set; } 
    }
}
