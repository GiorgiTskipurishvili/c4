namespace ClinicAPI.DTOs
{
    public class BookingRequest
    {
        public int UserId { get; set; }

        public int DoctorId { get; set; }

        public string Description { get; set; }

        public DateTime CreateBookingTime { get; set; }
    }
}
