using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrivateSchool.Entities;
using PrivateSchool.Models;
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

            if(subjects == null)
            {
                return BadRequest(new { message = "No Subjects" });
            }
            return Ok(subjects);

        }

    }
}
