using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillTrackerProfile.API.Models;
using SkillTrackerProfile.API.Services;

namespace SkillTrackerProfile.API.Controllers
{
    [Authorize]
    [Route("api/trip")]
    [ApiController]

    public class EmployeeController : ControllerBase
    {
        private readonly ISkillProfileService _tripService;

        public EmployeeController(ISkillProfileService Service)
        {
            _tripService = Service;
        }


        [Route("GetALLTrips")]
        [HttpGet]
        public IActionResult GetAllTrips()
        {
            IEnumerable<Profile> trips = _tripService.GetAllTrips();
            return Ok(trips);
        }

        [Route("GetTripbyEmployee")]
        [HttpGet]
        public IActionResult GetTripbyEmployee(string id)
        {
            IEnumerable<Profile> trips = _tripService.GetEmployee(id);
            return Ok(trips);
        }
    }
}

