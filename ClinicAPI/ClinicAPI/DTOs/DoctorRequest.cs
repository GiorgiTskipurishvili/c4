namespace ClinicAPI.DTOs
{
    public class DoctorRequest : UserRequest
    {

        public string? Category { get; set; }
        public int? Rating { get; set; }
        public IFormFile? Photo { get; set; }
        public IFormFile? CV { get; set; }
       
    }
}
