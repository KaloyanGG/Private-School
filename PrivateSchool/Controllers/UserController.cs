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
        public async Task<IActionResult> Login([FromBody]LoginBindingModel model)
        {
            if (ModelState.IsValid)
            {
                UserReturnModel userModel = await _userService.Login(model.Username, model.Password);
                if (userModel == null)
                {
                    return BadRequest(new { message = "Username or password is invalid."});
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

        [HttpDelete("{id}")]
        //[Authorize]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            if (ModelState.IsValid)
            {
                FullInfoUserReturnModel user = await _userService.DeleteUserById(id);
                if (user == null)
                {
                    return BadRequest(new { message = "User does not exist." });
                }
                return Ok(user);
            }
            return BadRequest(ModelState);
        }


    }
}
