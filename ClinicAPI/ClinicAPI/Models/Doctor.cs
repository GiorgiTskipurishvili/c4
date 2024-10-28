namespace ClinicAPI.Models
{
    public class Doctor : User
    {
        //public int Id { get; set; }
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        //public string Email { get; set; }
        //public string Password { get; set; }
        //public long PersonalId { get; set; }


        public string? Category { get; set; }
        public int? Rating { get; set; }
        public IFormFile? Photo { get; set; }    
        public IFormFile? CV { get; set; }

    }
}
