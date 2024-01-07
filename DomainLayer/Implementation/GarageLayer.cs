using DomainLayer.Interface;
using DomainLayer.Mapper;
using DomainLayer.Models;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DomainLayer.Implementation
{
    public class GarageLayer : IGarageLayer
    {
        private readonly IGarageRepository _garageRepository;

        public GarageLayer(IGarageRepository garageRepository)
        {
            _garageRepository = garageRepository;
        }

        public async Task<List<GarageModel>> GetAllGaragesAsync(CancellationToken cancellationToken)
        {
            try
            {
                var garages = (await _garageRepository.GetAllGaragesAsync(cancellationToken)).ToDomainModel();
                return garages;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<GarageModel?> GetGarageAsync(Guid garageId, CancellationToken cancellationToken)
        {
            try
            {
                var garage = (await _garageRepository.GetGarageAsync(garageId, cancellationToken))?.ToDomainModel();
                return garage;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<GarageParkingModel?> GetGarageParkingAsync(Guid garageId, CancellationToken cancellationToken)
        {
            try
            {
                var garage = (await _garageRepository.GetGarageAsync(garageId, cancellationToken))?.ToDomainModel();
                var parkedGarage = (await _garageRepository.GetGarageParkingAsync(garageId))?.ToDomainModel();

                if (garage != null)
                {
                    return new GarageParkingModel
                    {
                        GarageId = garage.Id,
                        GarageName = garage.Name,
                        AvailableParkingSpots = parkedGarage == null ? garage.TotalParkingSpots : garage.TotalParkingSpots - parkedGarage.BookedParking
                    };
                }

                return null;
            }
             catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> HardwareReadabilityAndOpenDoor(string ipAddress)
        {
            try
            {
                Ping pingSender = new();
                var reply = await pingSender.SendPingAsync(ipAddress);

                if (reply.Status == IPStatus.Success)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
