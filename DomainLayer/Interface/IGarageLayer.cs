using DomainLayer.Models;
using RepositoryLayer.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Interface
{
    public interface IGarageLayer
    {
        public Task<List<GarageModel>> GetAllGaragesAsync(CancellationToken cancellationToken);

        public Task<GarageModel?> GetGarageAsync(Guid garageId, CancellationToken cancellationToken);

        public Task<GarageParkingModel?> GetGarageParkingAsync(Guid garageId, CancellationToken cancellationToken);

        public Task<bool> HardwareReadabilityAndOpenDoor(string ipAddress);
    }
}
