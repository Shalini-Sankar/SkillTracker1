using System;
using System.Linq;
using System.Collections.Generic;
using NUnit.Framework;
using TripManagerSummary.API.Models;
using TripManagerSummary.API.Services;
using Moq;

namespace TripSummary.Test
{
    [TestFixture]
    public class TripSummaryTest
    {
        private ITripSummaryService _mockRepository;
        private List<Trip> _trips;

        [SetUp]
        public void Initialize()
        {
            var repository = new Mock<ITripSummaryService>();        
            _trips = new List<Trip>()
                     {
                         new Trip { TripId = 1,CustomerId = "Cust1",EmployeeId = "Emp1",TripStatus = "Completed",TripFare = 250,TripFromLocation = "Taramani",TripToLocation = "Siruseri" },
                         new Trip { TripId = 2,CustomerId = "Cust2",EmployeeId = "Emp2",TripStatus = "Cancelled",TripFare = 120,TripFromLocation = "Velachery",TripToLocation = "TNagar" },
                         new Trip { TripId = 3,CustomerId = "Cust1",EmployeeId = "Emp3",TripStatus = "Completed",TripFare = 80,TripFromLocation = "Saidapet",TripToLocation = "Nungambakkam" }
                     };

            //Get All
            repository.Setup(r => r.GetAllTrips()).Returns(_trips);

            // Get All trips by Employee Id
            repository.Setup(r => r.GetEmployee(It.IsAny<string>()))
                .Returns((string i) => _trips.Where(t => t.EmployeeId == i).ToList());

            _mockRepository = repository.Object;
        }

        [Test]
        public void Get_All_Trips()
        {
            IEnumerable<Trip> trips = _mockRepository.GetAllTrips();

            Assert.IsTrue(trips.Count() == 3);
            Assert.IsTrue(trips.ElementAt(0).CustomerId == "Cust1");
            Assert.IsTrue(trips.ElementAt(1).TripId == 2);
            Assert.IsTrue(trips.ElementAt(1).EmployeeId == "Emp2");
            Assert.IsTrue(trips.ElementAt(2).TripFare == 80);
            Assert.IsTrue(trips.ElementAt(2).TripStatus == "Completed");
        }

        [Test]
        public void Get_Trip_By_Employee()
        {
            IEnumerable<Trip> trips = _mockRepository.GetEmployee("Emp2");
            Assert.IsTrue(trips.Count() == 1);
            Assert.IsTrue(trips.ElementAt(0).CustomerId == "Cust2");
        }

        [TearDown]
        public void CleanUp()
        {
            _trips.Clear();
        }
    }
}
