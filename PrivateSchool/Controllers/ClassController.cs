using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrivateSchool.Entities;
using PrivateSchool.Models;
using PrivateSchool.Models.BindingModels;
using PrivateSchool.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrivateSchool.Controllers
{
    //[Authorize]
    public class ClassController : BaseApiController
    {
        private readonly IClassService _classService;

        public ClassController(IClassService classService)
        {
            _classService = classService;
           
        }

        [HttpGet("all")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> All()
        {
            var classes = await _classService.GetAllClasses();

            if (classes == null)
            {
                return BadRequest(new { message = "No Classes" });
            }

            return Ok(classes);
        }

        [HttpGet("{id}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            var classs = await _classService.GetClassById(int.Parse(id));

            if(classs == null)
            {
                return BadRequest("Invalid Class");
            }


            return Ok(classs);
        }

        [HttpPost("add")]
        [AllowAnonymous]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Add([FromBody] AddClassBindingModel model)
        {

            if (ModelState.IsValid)
            {
                ClassReturnModel classModel = await _classService.Add(model);

                if(classModel == null)
                {
                    return BadRequest(new { message = "Class already exists." });
                }
                return Ok(model);
            }
            return BadRequest(ModelState);

        }


    }
}
