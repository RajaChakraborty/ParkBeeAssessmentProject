using Microsoft.EntityFrameworkCore;
using RepositoryLayer.DatabaseContext;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ParkingDbContext _parkingDbContext;

        public UserRepository(ParkingDbContext parkingDbContext)
        {
            _parkingDbContext = parkingDbContext;
        }

        public async Task<bool> DoesUserHasRuningParkingSessionsAsync(Guid userId, CancellationToken cancellationToken)
        {
            try
            {
                var isSessionRunning = await _parkingDbContext.UserSessions.AnyAsync(session => session.UserId.Equals(userId), cancellationToken);
                return isSessionRunning;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<User>> GetUsersAsync(string partnerId, CancellationToken cancellationToken)
        {
            try
            {
                return await _parkingDbContext.Users.Where(x => x.PartnerId == partnerId).ToListAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Guid> AddUserSessionAsync(Guid userId, CancellationToken cancellationToken)
        {
            try
            {
                var sessionId = Guid.NewGuid();
                var userSessionObj = new UserSession { SessionId = sessionId, UserId = userId };
                await _parkingDbContext.UserSessions.AddAsync(userSessionObj, cancellationToken);
                _parkingDbContext.SaveChanges();

                return sessionId;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task RemoveUserSessionAsync(Guid userId, CancellationToken cancellationToken)
        {
            try
            {
                var sessionObj = await _parkingDbContext.UserSessions.SingleAsync(u => u.UserId.Equals(userId), cancellationToken);
                _parkingDbContext.Remove(sessionObj);
                _parkingDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
