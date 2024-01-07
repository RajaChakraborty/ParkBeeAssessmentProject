using ApI.Controllers;
using ApI.Request_Model;
using ApI.ViewModel;
using DomainLayer.Implementation;
using DomainLayer.Interface;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Implementation;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject_API.Controller
{
    [Collection("SharedDbContext")]
    public class UserControllerTest
    {
        private readonly IGarageLayer _garageLayer;
        private readonly IUserLayer _userLayer;
        private readonly IUserRepository _userRepository;
        private readonly UsersController _usersController;
        private readonly IGarageRepository _garageRepository;
        private readonly DbContextFixture _fixture;

        public UserControllerTest(DbContextFixture fixture)
        {
            _fixture = fixture;
            _garageRepository = new GarageRepository(_fixture.DbContext);
            _userRepository = new UserRepository(_fixture.DbContext);
            _userLayer = new UserLayer(_userRepository, _garageRepository);
            _garageLayer = new GarageLayer(_garageRepository);
            _usersController = new UsersController(_userLayer, _garageLayer);
        }

        [Fact]
        public async void StartParkingSession_Success_Test()
        {
            var startSessionObj = new ParkingSession
            {
                GarageId = Guid.Parse("0f6d8372-ad6f-4d25-b636-df2d1088b95c"),
                UserId = Guid.Parse("0acde387-0940-4bd9-80b9-84916cbb9a46"),
                LicensePlateNumber = "123-4545",
                IPAddress = "127.0.0.1"
            };

            var result = await _usersController.StartParkingSession(startSessionObj, new CancellationTokenSource().Token);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void StartParkingSession_UserSessionRunning_Failed_Test()
        {
            var startSessionObj = new ParkingSession
            {
                GarageId = Guid.Parse("b46f25c5-f21f-402f-9542-cdca2e5d74fd"),
                UserId = Guid.Parse("0acde387-0940-4bd9-80b9-84916cbb9a46"),
                LicensePlateNumber = "123-4545",
                IPAddress = "127.0.0.1"
            };

            var result = await _usersController.StartParkingSession(startSessionObj, new CancellationTokenSource().Token);
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Another parking session for the user is already active", result?.Value?.ToString());
        }

        [Fact]
        public async void StartParkingSession_NoParkingSpotAvailable_Failed_Test()
        {
            var startSessionObj = new ParkingSession
            {
                GarageId = Guid.Parse("0f6d8372-ad6f-4d25-b636-df2d1088b95c"),
                UserId = Guid.Parse("0acde387-0940-4bd9-80b9-84916cbb9a42"),
                LicensePlateNumber = "123-4545",
                IPAddress = "127.0.0.1"
            };

            var result = await _usersController.StartParkingSession(startSessionObj, new CancellationTokenSource().Token);
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("No parking spots available", result?.Value?.ToString());
        }

        [Fact]
        public async void StartParkingSession_HardwareNotReachable_Failed_Test()
        {
            var startSessionObj = new ParkingSession
            {
                GarageId = Guid.Parse("0f6d8372-ad6f-4d25-b636-df2d1088b95c"),
                UserId = Guid.Parse("0acde387-0940-4bd9-80b9-84916cbb9a42"),
                LicensePlateNumber = "123-4545",
                IPAddress = "192.168.10.2"
            };

            var result = await _usersController.StartParkingSession(startSessionObj, new CancellationTokenSource().Token);
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Location hardware is not reachable", result?.Value?.ToString());
        }

        [Fact]
        public async void StopParkingSession_NoActiveSession_Failed_Test()
        {
            var stopSessionObj = new StopSession
            {
                GarageId = Guid.Parse("0f6d8372-ad6f-4d25-b636-df2d1088b95c"),
                UserId = Guid.Parse("0acde387-0940-4bd9-80b9-84916cbb9a42"),
                IPAddress = "127.0.0.1"
            };

            var result = await _usersController.StopParkingSession(stopSessionObj, new CancellationTokenSource().Token);
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("User has no active session", result?.Value?.ToString());
        }

        [Fact]
        public async void StopParkingSession_Success_Test()
        {
            var stopSessionObj = new StopSession
            {
                GarageId = Guid.Parse("0f6d8372-ad6f-4d25-b636-df2d1088b95c"),
                UserId = Guid.Parse("0acde387-0940-4bd9-80b9-84916cbb9a46"),
                IPAddress = "127.0.0.1"
            };

            var result = await _usersController.StopParkingSession(stopSessionObj, new CancellationTokenSource().Token);
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
