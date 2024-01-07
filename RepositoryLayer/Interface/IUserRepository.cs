using RepositoryLayer.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IUserRepository
    {
        public Task<List<User>> GetUsersAsync(string partnerId, CancellationToken cancellationToken);

        public Task<bool> DoesUserHasRuningParkingSessionsAsync(Guid userId, CancellationToken cancellationToken);

        public Task<Guid> AddUserSessionAsync(Guid userId, CancellationToken cancellationToken);

        public Task RemoveUserSessionAsync(Guid userId, CancellationToken cancellationToken);
    }
}
