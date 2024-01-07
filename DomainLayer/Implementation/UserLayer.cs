using DomainLayer.Interface;
using DomainLayer.Mapper;
using DomainLayer.Models;
using RepositoryLayer.DatabaseContext;
using RepositoryLayer.Enum;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Implementation
{
    public class UserLayer : IUserLayer
    {
        private readonly IUserRepository _userRepository;
        private readonly IGarageRepository _garageRepository;

        public UserLayer(IUserRepository userRepository, IGarageRepository garageRepository)
        {
            _userRepository = userRepository;
            _garageRepository = garageRepository;
        }

        public async Task<bool> DoesUserHasRuningParkingSessionsAsync(Guid userId, CancellationToken cancellationToken)
        {
            try
            {
                var isSessionRunning = await _userRepository.DoesUserHasRuningParkingSessionsAsync(userId, cancellationToken);
                return isSessionRunning;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<UserModel>> GetUsersAsync(string partnerId, CancellationToken cancellationToken)
        {
            try
            {
                var users = (await _userRepository.GetUsersAsync(partnerId, cancellationToken)).ToDomainModel();
                return users;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Guid> StartParkingSessionAsync(Guid userId, Guid garageId, CancellationToken cancellationToken)
        {
            try
            {
                await _garageRepository.UpdateParkingSpotAsync(garageId, OperationType.ADD.ToString(), cancellationToken);
                var sessionId = await _userRepository.AddUserSessionAsync(userId, cancellationToken);

                return sessionId;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task StopParkingSessionAsync(Guid userId, Guid garageId, CancellationToken cancellationToken)
        {
            try
            {
                await _garageRepository.RemoveParkingSpotAsync(garageId, OperationType.REMOVE.ToString(), cancellationToken);
                await _userRepository.RemoveUserSessionAsync(userId, cancellationToken);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
