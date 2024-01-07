using ApI.Mapper;
using ApI.Middleware;
using ApI.Request_Model;
using ApI.ViewModel;
using DomainLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace ApI.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly IUserLayer _userLayer;
    private readonly IGarageLayer _garageLayer;

    public UsersController(IUserLayer userLayer, IGarageLayer garageLayer)
    {
        _userLayer = userLayer;
        _garageLayer = garageLayer;
    }


    [HttpGet]
    public async Task<ActionResult<List<UserVM>>> GetUsers(CancellationToken cancellationToken)
    {
        try
        {
            var partnerId = User.Claims.First(x => x.Type == CustomClaimNames.PartnerId).Value;
            var usersResponse = (await _userLayer.GetUsersAsync(partnerId, cancellationToken)).ToViewModel();

            return usersResponse;
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An unexpected error occurred. Please try again later.");
        }
    }

    [HttpPost("StartParkingSession")]
    public async Task<ObjectResult> StartParkingSession([FromBody] ParkingSession parkingSesionRequest, CancellationToken cancellationToken)
    {
        try
        {
            var openDoor = false;
            var isSessionRunning = await _userLayer.DoesUserHasRuningParkingSessionsAsync(parkingSesionRequest.UserId, cancellationToken);

            if (isSessionRunning)
            {
                return BadRequest("Another parking session for the user is already active");
            }

            var availableParkingSpot = await _garageLayer.GetGarageParkingAsync(parkingSesionRequest.GarageId, cancellationToken);

            if (availableParkingSpot?.AvailableParkingSpots == 0)
            {
                return BadRequest("No parking spots available");
            }

            var isLocationHardwareReachable = await _garageLayer.HardwareReadabilityAndOpenDoor(parkingSesionRequest.IPAddress);

            if (!isLocationHardwareReachable)
            {
                return BadRequest("Location hardware is not reachable");
            }

            var sessionId = await _userLayer.StartParkingSessionAsync(parkingSesionRequest.UserId, parkingSesionRequest.GarageId, cancellationToken);
            if (sessionId != Guid.Empty)
            {
                openDoor = await _garageLayer.HardwareReadabilityAndOpenDoor(parkingSesionRequest.IPAddress);
            }

            return Ok(new { ParkingSessionId = sessionId, OpenEntryDoor = openDoor });
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An unexpected error occurred. Please try again later.");
        }
    }

    [HttpPost("StopParkingSession")]
    public async Task<ObjectResult> StopParkingSession([FromBody] StopSession stopSesionRequest, CancellationToken cancellationToken)
    {
        try
        {
            var isSessionRunning = await _userLayer.DoesUserHasRuningParkingSessionsAsync(stopSesionRequest.UserId, cancellationToken);
            if (!isSessionRunning)
            {
                return BadRequest("User has no active session");
            }

            await _userLayer.StopParkingSessionAsync(stopSesionRequest.UserId, stopSesionRequest.GarageId, cancellationToken);
            var doorOpen = await _garageLayer.HardwareReadabilityAndOpenDoor(stopSesionRequest.IPAddress);

            return Ok(new { OpenExitDoor = doorOpen });
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An unexpected error occurred. Please try again later.");
        }
    }
}