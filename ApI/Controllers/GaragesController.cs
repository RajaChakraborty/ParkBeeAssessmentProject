using ApI.Mapper;
using ApI.ViewModel;
using DomainLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.DatabaseContext;

namespace ApI.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class GaragesController : ControllerBase
{
    private readonly IGarageLayer _garageLayer;

    public GaragesController(IGarageLayer garageLayer)
    {
        _garageLayer = garageLayer;
    }

    [HttpGet]
    public async Task<ActionResult<List<GarageVM>>> GetGarages(CancellationToken cancellationToken)
    {
        try
        {
            var garages = (await _garageLayer.GetAllGaragesAsync(cancellationToken)).ToViewModel();
            return garages;
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An unexpected error occurred. Please try again later.");
        }
    }

    [HttpGet("{garageId}")]
    public async Task<ActionResult<GarageVM>> GetGarage(Guid garageId, CancellationToken cancellationToken)
    {
        try
        {
            var garage = (await _garageLayer.GetGarageAsync(garageId, cancellationToken))?.ToViewModel();
            if (garage == null)
            {
                return new NotFoundResult();
            }

            return garage;
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An unexpected error occurred. Please try again later.");
        }
    }

    [HttpGet("GetAvailableSpots/{garageId}")]
    public async Task<ActionResult<AvailableParkingSpotsVM>> GetAvailableParkingSpots(Guid garageId, CancellationToken cancellationToken)
    {
        try
        {
            var parkingSpotsResponse = (await _garageLayer.GetGarageParkingAsync(garageId, cancellationToken))?.ToViewModel();

            if (parkingSpotsResponse == null)
            {
                return new NotFoundResult();
            }

            return parkingSpotsResponse;
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An unexpected error occurred. Please try again later.");
        }
    }
}