//using ClinicAPI.DTOs;
//using ClinicAPI.Models;
//using ClinicAPI.Package;
//using Microsoft.AspNetCore.Mvc;

//namespace ClinicAPI.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class DoctorsController : ControllerBase
//    {

//        private readonly PKG_DOCTORS _pkgDoctors;

//        public DoctorsController()
//        {
//            _pkgDoctors = new PKG_DOCTORS();
//        }

//        [HttpPost("add")]
//        public IActionResult AddDoctor([FromForm] DoctorRequest doctorRequest)
//        {
//            _pkgDoctors.add_doctor(doctorRequest);
//            return Ok("Doctor added successfully.");
//        }


//        [HttpGet("get")]
//        public IActionResult GetDoctors()
//        {
//            var doctors = _pkgDoctors.get_doctors();
//            return Ok(doctors);
//        }

//        [HttpDelete("delete/{id}")]
//        public IActionResult DeleteDoctor(int id)
//        {
//            _pkgDoctors.delete_doctor(id);
//            return Ok("Doctor deleted successfully.");
//        }

//        [HttpPut]
//        public IActionResult UpdateDoctor(int id, DoctorResponse doctorResponse)
//        {
//            if (id != doctorResponse.Id)
//            {
//                return BadRequest(new { message = "Doctor ID mismatch" });
//            }

//            try
//            {

//                _pkgDoctors.update_doctor(doctorResponse);

//                return Ok(new { message = "Doctor updated successfully" });
//            }
//            catch (Exception ex)
//            {
//                // Log the exception and return a failure message
//                return StatusCode(500, new { message = "Error updating Doctor", error = ex.Message });
//            }
//        }




//        [HttpGet("{id}")]
//        public IActionResult GetDoctorById(int id)
//        {
//            try
//            {
//                var doctor = _pkgDoctors.get_doctor_by_id(id);

//                if (doctor == null)
//                {
//                    return NotFound(new { message = "Doctor not found" });
//                }

//                return Ok(doctor);
//            }
//            catch (Exception ex)
//            {
//                // Log the exception (implement logging as needed)
//                return StatusCode(500, new { message = "Error retrieving doctor", error = ex.Message });
//            }
//        }



//    }
//}


using ClinicAPI.DTOs;
using ClinicAPI.Models;
using ClinicAPI.Package;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClinicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly PKG_DOCTORS _pkgDoctors;

        public DoctorsController()
        {
            _pkgDoctors = new PKG_DOCTORS();
        }

        // POST: api/Doctors
        [HttpPost("add")]
        public IActionResult AddDoctor([FromForm] DoctorRequest doctor)
        {
            try
            {
                _pkgDoctors.add_doctor(doctor);
                return Ok("Doctor added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET: api/Doctors
        [HttpGet("all")]
        public IActionResult GetDoctors()
        {
            try
            {
                var doctors = _pkgDoctors.get_doctors();
                return Ok(doctors);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET: api/Doctors/{id}
        [HttpGet("{id}")]
        public IActionResult GetDoctorById(int id)
        {
            try
            {
                var doctor = _pkgDoctors.get_doctor_by_id(id);
                if (doctor == null) return NotFound("Doctor not found.");
                return Ok(doctor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //// PUT: api/Doctors/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateDoctor([FromBody] DoctorResponse doctor)
        {
            try
            {
                _pkgDoctors.update_doctor(doctor);
                return Ok("Doctor updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //[HttpPut("{id}")]
        //public void UpdateDoctor(DoctorResponse doctor)
        //{
        //    PKG_DOCTORS pKG_DOCTORS = new PKG_DOCTORS();
        //    pKG_DOCTORS.update_doctor(doctor);
        //}


        // DELETE: api/Doctors/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteDoctor(int id)
        {
            try
            {
                _pkgDoctors.delete_doctor(id);
                return Ok("Doctor deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // PUT: api/Doctors/{id}/photo
        [HttpPut("{id}/photo")]
        public IActionResult UpdatePhoto(int id, IFormFile photo)
        {
            try
            {
                _pkgDoctors.update_photo(id, photo);
                return Ok("Doctor photo updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET: api/Doctors/{id}/photo
        [HttpGet("{id}/photo")]
        public IActionResult GetPhotoByDoctorId(int id)
        {
            try
            {
                var photo = _pkgDoctors.get_photo_by_doctor_id(id);
                if (photo == null) return NotFound("Photo not found.");
                return File(photo, "image/jpeg");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // PUT: api/Doctors/{id}/cv
        [HttpPut("{id}/cv")]
        public IActionResult UpdateCV(int id, IFormFile cv)
        {
            try
            {
                _pkgDoctors.update_cv(id, cv);
                return Ok("Doctor CV updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET: api/Doctors/{id}/cv
        [HttpGet("{id}/cv")]
        public IActionResult GetCVByDoctorId(int id)
        {
            try
            {
                var cv = _pkgDoctors.get_cv_by_doctor_id(id);
                if (cv == null) return NotFound("CV not found.");
                return File(cv, "application/pdf");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
