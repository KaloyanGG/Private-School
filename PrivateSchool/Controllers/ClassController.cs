using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrivateSchool.Entities;
using PrivateSchool.Models;
using PrivateSchool.Models.BindingModels;
using PrivateSchool.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrivateSchool.Controllers
{
    public class ClassController : BaseApiController
    {
        private readonly IClassService _classService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public ClassController(IClassService classService, IUserService userService, IMapper mapper)
        {
            _classService = classService;
            _userService = userService;
            _mapper = mapper;
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
            var classs = await _classService.GetClassReturnModelById(int.Parse(id));

            if(classs == null)
            {
                return BadRequest("Invalid Class");
            }


            return Ok(classs);
        }

        [HttpPost("add")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Teacher")]
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

        [HttpPut("{name}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Update([FromBody] AddClassBindingModel model, [FromRoute] string name)
        {
            if (ModelState.IsValid)
            {
                Class classs = await _classService.GetClassByName(name);
                if (classs == null)
                {
                    return BadRequest(new { message = "Class does not exist." });
                }
                classs.Name = model.Name;
                classs.TeacherId = model.TeacherId;

                Class result = await _classService.updateClass(classs);

                return Ok(result);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{name}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Delete([FromRoute] string name)
        {
            if (ModelState.IsValid)
            {
                ClassReturnModel classs = await _classService.DeleteClassByName(name);
                if (classs == null)
                {
                    return BadRequest(new { message = "Class does not exist." });
                }
                return Ok(classs);
            }
            return BadRequest(ModelState);
        }

        [HttpPost("{classId}")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> AddStudentToAClass([FromRoute] string classId)
        {
            if (ModelState.IsValid)
            {
                Class classs = await _classService.GetClassById(int.Parse(classId));
                if (classs == null)
                {
                    return BadRequest(new { message = "Class does not exist." });
                }
                //Student student = await _userService.GetStudentById(int.Parse(studentId));
                return Ok(await _classService.AddStudentToAClass(User.Identity.Name, classs));
            }
            return BadRequest(ModelState);
        }

        [HttpGet("{classId}/students")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetStudentsInAClass([FromRoute] string classId)
        {
            if (ModelState.IsValid)
            {
                List<StudentReturnModel> students = await _classService.GetAllStudentsByClassId(int.Parse(classId));
                if (students == null)
                {
                    return BadRequest(new { message = "Class has no students." });
                }
                return Ok(students);
            }
            return BadRequest(ModelState);
        }


    }
}
