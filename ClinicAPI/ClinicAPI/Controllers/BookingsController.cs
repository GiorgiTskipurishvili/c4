//using ClinicAPI.Models;
//using ClinicAPI.Package;
//using Microsoft.AspNetCore.Mvc;

//namespace ClinicAPI.Controllers
//{       
//    [Route("api/[controller]")]
//    [ApiController]
//    public class BookingsController : Controller
//    {

//        [HttpGet]
//        public List<Booking> GetBookings()
//        {
//            PKG_BOOKINGS package = new PKG_BOOKINGS();
//            List<Booking> bookings = new List<Booking>();
//            bookings = package.get_bookings();
//            return bookings;
//        }

//        [HttpPost]
//        public void AddBooking(Booking booking)
//        {
//            PKG_BOOKINGS pKG_BOOKINGS = new PKG_BOOKINGS();
//            pKG_BOOKINGS.add_booking(booking);
//        }


//        [HttpPut]
//        public void UpdateBooking(Booking booking)
//        {
//            PKG_BOOKINGS pKG_BOOKINGS = new PKG_BOOKINGS(); 
//            pKG_BOOKINGS.update_booking(booking);
//        }

//        [HttpDelete("{id}")]
//        public void DeleteBooking(int id)
//        {
//            PKG_BOOKINGS pKG_BOOKINGS = new PKG_BOOKINGS();
//            Booking booking = new Booking();
//            booking.Id = id;
//            pKG_BOOKINGS.delete_booking(booking);
//        }


//        [HttpGet("{id}")]
//        public void GetBookingById(int id)
//        {
//            PKG_BOOKINGS pKG_BOOKINGS = new PKG_BOOKINGS();
//            Booking booking = new Booking();
//            booking.Id = id;
//            pKG_BOOKINGS.get_booking_by_id(booking);
//        }
//    }
//}


using ClinicAPI.DTOs;
using ClinicAPI.Models;
using ClinicAPI.Package;
using Microsoft.AspNetCore.Mvc;

namespace ClinicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly PKG_BOOKINGS _pkgBookings;

        public BookingsController()
        {
            _pkgBookings = new PKG_BOOKINGS();
        }

        [HttpPost]
        public IActionResult AddBooking([FromBody] BookingRequest bookingRequest)
        {
            if (bookingRequest == null)
            {
                return BadRequest("Booking request cannot be null.");
            }

            _pkgBookings.add_booking(bookingRequest);
            return Ok(bookingRequest);
        }


        [HttpGet]
        public ActionResult<List<Booking>> GetBookings()
        {
            var bookings = _pkgBookings.get_bookings();
            return Ok(bookings);
        }



        [HttpGet("{id}")]
        public IActionResult GetBookingById(int id)
        {
            try
            {
                var booking = _pkgBookings.get_booking_by_id(id);
                if (booking == null)
                {
                    return NotFound();
                }
                return Ok(booking);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPut("{id}")]
        public void UpdateBooking(Booking booking)
        {
            PKG_BOOKINGS pKG_BOOKINGS = new PKG_BOOKINGS();
            pKG_BOOKINGS.update_booking(booking);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBooking(int id)
        {
            try
            {
                _pkgBookings.delete_booking(id);
                return NoContent(); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
