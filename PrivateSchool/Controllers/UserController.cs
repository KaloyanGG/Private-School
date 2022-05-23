using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrivateSchool.Models;
using PrivateSchool.Models.BindingModels;
using PrivateSchool.Services.Interfaces;
using System.Threading.Tasks;

namespace PrivateSchool.Controllers
{
    public class UserController : BaseApiController
    {
        private readonly IUserService _userRespository;

        public UserController(IUserService userRespository)
        {
            _userRespository = userRespository;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Login([FromBody]LoginBindingModel model)
        {
            if (ModelState.IsValid)
            {
                UserReturnModel userModel = await _userRespository.Login(model.Username, model.Password);
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
                UserReturnModel userModel = await _userRespository.Register(model);

                if (userModel == null)
                {
                    return BadRequest(new { message = "Username already exists." });
                }

                return Ok(userModel);
            }
            return BadRequest(ModelState);

        }
    }
}
