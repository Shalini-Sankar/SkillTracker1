using System;
using System.Collections.Generic;
using System.Text;
using SkillTrackerProfile.API.Models;

namespace SkillTrackerProfile.API.Services

{
    public interface ISkillProfileService
    {
        IEnumerable <Profile> GetAllTrips();
        IEnumerable<Profile> GetEmployee(string id);
    }
}
