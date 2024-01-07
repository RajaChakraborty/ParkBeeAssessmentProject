using DomainLayer.Models;
using RepositoryLayer.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Interface
{
    public interface IUserLayer
    {
        public Task<List<UserModel>> GetUsersAsync(string partnerId, CancellationToken cancellationToken);

        public Task<bool> DoesUserHasRuningParkingSessionsAsync(Guid userId, CancellationToken cancellationToken);

        public Task<Guid> StartParkingSessionAsync(Guid userId, Guid garageId, CancellationToken cancellationToken);

        public Task StopParkingSessionAsync(Guid userId, Guid garageId, CancellationToken cancellationToken);
    }
}
