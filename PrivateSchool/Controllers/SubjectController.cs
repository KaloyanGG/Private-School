using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrivateSchool.Entities;
using PrivateSchool.Models;
using PrivateSchool.Models.BindingModels;
using PrivateSchool.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrivateSchool.Controllers
{
    public class SubjectController : BaseApiController
    {

        private readonly ISubjectService _subjectService;

        public SubjectController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        
        [HttpGet("all")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        
        public async Task<IActionResult> All()
        {

            List<Subject> subjects = await _subjectService.GetAllSubjects();

            if (subjects == null)
            {
                return BadRequest(new { message = "No Subjects" });
            }
            return Ok(subjects);
        }


        [HttpGet("{name}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromRoute] string name)
        {
            Subject subject = await _subjectService.GetSubjectByName(name);

            if (subject == null)
            {
                return BadRequest("Invalid Subject");
            }

            return Ok(subject);
        }
        
        [HttpPost]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Add([FromBody] AddSubjectBindingModel model)
        {
            if (ModelState.IsValid)
            {
                Subject subject = await _subjectService.Add(model);
                if (subject == null)
                {
                    return BadRequest(new { message = "Subject already exists." });
                }
                return Ok(model);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{name}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Update([FromBody] AddSubjectBindingModel model, [FromRoute] string name)
        {
            if (ModelState.IsValid)
            {
                Subject subject = await _subjectService.GetSubjectByName(name);
                if (subject == null)
                {
                    return BadRequest(new { message = "Subject does not exist." });
                }
                subject.Name = model.Name;
                subject.MaxCapacity = model.MaxCapacity;
                
                Subject result = await _subjectService.updateSubject(subject);

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
                Subject subject = await _subjectService.DeleteSubjectByName(name);
                if (subject == null)
                {
                    return BadRequest(new { message = "Subject does not exist." });
                }
                return Ok(subject);
            }
            return BadRequest(ModelState);
        }




    }
}
