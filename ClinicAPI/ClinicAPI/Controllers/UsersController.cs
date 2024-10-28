using ClinicAPI.DTOs;
using ClinicAPI.Models;
using ClinicAPI.Package;
using Microsoft.AspNetCore.Mvc;

namespace ClinicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        //public List<User> GetUsers()
        //{
        //    PKG_USERS package = new PKG_USERS();
        //    List<User> users = new List<User>();
        //    users = package.get_users();
        //    return users;
        //}
        [HttpGet]
        public IActionResult GetUsers()
        {
            PKG_USERS pKG_USERS = new PKG_USERS();
            var users = pKG_USERS.get_users(); // Assuming get_users is part of a service layer

            return Ok(users);
        }

        [HttpPost]
        public void AddUser(UserRequest userRequest)
        {
            PKG_USERS packager = new PKG_USERS();
            packager.add_user(userRequest);
        }


        //[HttpPut]
        //public void UpdateUser(User user)
        //{
        //    PKG_USERS package = new PKG_USERS();
        //    package.update_user(user);
        //}

        [HttpPut("update")]
        public IActionResult UpdateUser([FromBody] User user)
        {
            var _userPackage = new PKG_USERS();

            if (user == null)
            {
                return BadRequest("User cannot be null.");
            }

            _userPackage.update_user(user);
            return NoContent();
        }



        [HttpDelete("{id}")]
        public void DeleteUser(int id)
        {
            PKG_USERS package = new PKG_USERS();
            User user = new User();
            user.Id = id;
            package.delete_user(user);
        }



        //public void GetUserById(int id)
        //{
        //    PKG_USERS package = new PKG_USERS();
        //    User user = new User();
        //    user.Id = id;
        //    package.get_user_by_id(user);
        //}
        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            try
            {
                PKG_USERS pKG_USERS = new PKG_USERS();
                var userById = pKG_USERS.get_user_by_id(id);
                if (userById == null) return NotFound("User Not Found");
                return Ok(userById);
            }
            catch(Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
