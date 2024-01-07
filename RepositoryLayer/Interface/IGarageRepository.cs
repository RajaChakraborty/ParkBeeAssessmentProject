using RepositoryLayer.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IGarageRepository
    {
        public Task<List<Garage>> GetAllGaragesAsync(CancellationToken cancellationToken);

        public Task<Garage?> GetGarageAsync(Guid garageId, CancellationToken cancellationToken);

        public Task<Parking?> GetGarageParkingAsync(Guid garageId);

        public Task UpdateParkingSpotAsync(Guid garageId, string operation, CancellationToken cancellationToken);        

        public Task RemoveParkingSpotAsync(Guid garageId, string operation, CancellationToken cancellationToken);
    }
}
