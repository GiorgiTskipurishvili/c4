using System.ComponentModel.DataAnnotations;

namespace ClinicAPI.DTOs
{
    public class UserResponse
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string Password { get; set; } 

        [Required]
        public long PersonalId { get; set; }

    }
}
