using System;
using System.Collections.Generic;
using System.Linq;
using SkillTrackerProfile.API.Models;

namespace SkillTrackerProfile.API.Services
{
    public class SkillProfileService : ISkillProfileService
    {
        private readonly ApplicationContext _context;

        public SkillProfileService(ApplicationContext context)
        {
            _context = context;
        }
        public IEnumerable<Profile> GetAllTrips()
        {
            return _context.Profiles.ToList();
        }

        public IEnumerable<Profile> GetEmployee(string empId)
        {
            return _context.Profiles.Where(e => e.EmpId == empId).ToList();
        }

    }
}
