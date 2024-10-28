namespace ClinicAPI.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public int DoctorId { get; set; }

        public string Description { get; set; }

        public DateTime CreateBookingTime { get; set; }



    }
}
