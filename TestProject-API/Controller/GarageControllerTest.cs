using ApI.Controllers;
using ApI.ViewModel;
using DomainLayer.Implementation;
using DomainLayer.Interface;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Implementation;
using RepositoryLayer.Interface;

namespace TestProject_API.Controller
{
    [Collection("SharedDbContext")]
    public class GarageControllerTest
    {
        private readonly IGarageLayer _garageLayer;
        private readonly IGarageRepository _garageRepository;
        private readonly GaragesController _controller;
        private readonly DbContextFixture _fixture;

        public GarageControllerTest(DbContextFixture fixture)
        {

            _fixture = fixture;

            _garageRepository = new GarageRepository(_fixture.DbContext);
            _garageLayer = new GarageLayer(_garageRepository);
            _controller = new GaragesController(_garageLayer);
        }
        

        [Fact]
        public async void GetGarages_Test()
        {
            var result = await _controller.GetGarages(new CancellationTokenSource().Token);
            Assert.IsType<ActionResult< List<GarageVM>>>(result as ActionResult<List<GarageVM>>);
            Assert.Equal(3, result?.Value?.Count);
        }

        [Fact]
        public async void GetAvailableParkingSpots_Has_1_AvailableSpots_Test()
        {
            Guid garageId = Guid.Parse("0f6d8372-ad6f-4d25-b636-df2d1088b95c");
            var result = await _controller.GetAvailableParkingSpots(garageId, new CancellationTokenSource().Token);
            var availableSpotsObj = Assert.IsType<AvailableParkingSpotsVM>(result.Value);
            Assert.Equal(1, availableSpotsObj.AvailableParkingSpots);
        }        
    }
}
