using System;
using System.Linq;
using System.Collections.Generic;
using NUnit.Framework;
using TripManagerIdentity.API.Models;
using TripManagerIdentity.API.Services;
using Moq;

namespace TripSummary.Test
{
    [TestFixture]
    public class TripIdentityTest
    {
        private IUserService _mockRepository;
        private List<User> _users;

        [SetUp]
        public void Initialize()
        {
            var repository = new Mock<IUserService>();
            
            _users = new List<User>() { 
                new User{ UserId = 1, UserName = "Admin", Password = "Test123", Role = "admin"}, 
                new User{ UserId = 2, UserName = "Emp1", Password = "Test123", Role = "employee"}, 
                new User{ UserId = 3, UserName = "Emp2", Password = "Test123", Role = "employee" }};

            // Get All trips by Employee Id
            repository.Setup(r => r.GetUser(It.IsAny<string>(), It.IsAny<string>()))
                .Returns((string i,string j) => _users.Where(t => t.UserName == i && t.Password == j).FirstOrDefault());

            _mockRepository = repository.Object;
        }

        [Test]
        public void Get_User_By_Credential()
        {
            User _user = _mockRepository.GetUser("Emp2","Test123");
            Assert.IsTrue(_user.UserId == 3);
        }

        [TearDown]
        public void CleanUp()
        {
            _users.Clear();
        }
    }
}
