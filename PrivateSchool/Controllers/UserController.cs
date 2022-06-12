using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrivateSchool.Entities;
using PrivateSchool.Models;
using PrivateSchool.Models.BindingModels;
using PrivateSchool.Services.Interfaces;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PrivateSchool.Controllers
{
    public class UserController : BaseApiController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userRespository)
        {
            _userService = userRespository;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Login([FromBody] LoginBindingModel model)
        {
            if (ModelState.IsValid)
            {
                UserReturnModel userModel = await _userService.Login(model.Username, model.Password);
                if (userModel == null)
                {
                    return BadRequest(new { message = "Username or password is invalid." });
                }
                return Ok(userModel);
            }
            return BadRequest(ModelState);

        }

        [HttpPost("register")]
        [AllowAnonymous]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Register([FromBody] RegisterBindingModel model)
        {
            if (ModelState.IsValid)
            {
                UserReturnModel userModel = await _userService.Register(model);

                if (userModel == null)
                {
                    return BadRequest(new { message = "Username already exists." });
                }

                return Ok(userModel);
            }
            return BadRequest(ModelState);

        }

        [HttpGet("logout")]
        [Authorize]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> logout()
        {
            await _userService.Logout();

            return Ok();
        }
        
        [HttpPut("teacher")]
        [Authorize(Roles = "Teacher")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateTeacher([FromBody] UpdateTeacherBindingModel model)
        {
            if (ModelState.IsValid)
            {
               // string role = User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).Any() ? "Teacher" : "Student";

                FullInfoTeacherModel teacherDTO = await _userService.UpdateTeacher(model, User.Identity.Name);

                return Ok(teacherDTO);
            }

            return BadRequest(ModelState);
        }
        [HttpPut("student")]
        [Authorize(Roles = "Student")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateStudent([FromBody] UpdateStudentBindingModel model)
        {
            if (ModelState.IsValid)
            {

                FullInfoStudentModel teacherDTO = await _userService.UpdateStudent(model, User.Identity.Name);

                return Ok(teacherDTO);
            }

            return BadRequest(ModelState);
        }

        [HttpPut]
        [Authorize]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Update([FromBody] UpdateUserBindingModel model)
        {
            if (ModelState.IsValid)
            {
                string role = User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).Any() ? "Teacher" : "Student";
               

                FullInfoUserReturnModel userDTO = await _userService.Update(model, User.Identity.Name, role);

                return Ok(userDTO);
            }

            return BadRequest(ModelState);
        }

        [HttpPut("{subjectId}")]
        [Authorize(Roles ="Teacher")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AddSubjectToTeacher([FromRoute] string subjectId)
        {
            if (ModelState.IsValid)
            {

                TeacherReturnModel res = await _userService.AddSubjectByIdToTeacher(User.Identity.Name, int.Parse(subjectId));
                if(res == null)
                {
                    return BadRequest("No subject with that Id exists");
                }
                return Ok(res);
            }
            return BadRequest(ModelState);

        }

        [HttpDelete]
        [Authorize]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete()
        {
            if (ModelState.IsValid)
            {
                FullInfoUserReturnModel user = await _userService.DeleteUserById(User.Identity.Name);
                if (user == null)
                {
                    return BadRequest(new { message = "User does not exist." });
                }
                return Ok(new { message = "User deleted successfully" });
            }
            return BadRequest(ModelState);
        }


    }
}
