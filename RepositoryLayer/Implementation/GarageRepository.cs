using Microsoft.EntityFrameworkCore;
using RepositoryLayer.DatabaseContext;
using RepositoryLayer.Enum;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Implementation
{
    public class GarageRepository : IGarageRepository
    {
        private readonly ParkingDbContext _parkingDbContext;

        public GarageRepository(ParkingDbContext parkingDbContext)
        {
            _parkingDbContext = parkingDbContext;
        }

        

        public async Task<List<Garage>> GetAllGaragesAsync(CancellationToken cancellationToken)
        {
            try
            {
                return await _parkingDbContext.Garages.Include(x => x.Doors).ToListAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Garage?> GetGarageAsync(Guid garageId, CancellationToken cancellationToken)
        {
            try
            {
                var garage = await _parkingDbContext.Garages.Include(x => x.Doors)
                .FirstOrDefaultAsync(g => g.Id == garageId, cancellationToken);

                return garage;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Parking?> GetGarageParkingAsync(Guid garageId)
        {
            try
            {
                var parkedGarage = await _parkingDbContext.Parkings.Where(p => p.GarageId.Equals(garageId)).FirstOrDefaultAsync();
                return parkedGarage;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task RemoveParkingSpotAsync(Guid garageId, string operation, CancellationToken cancellationToken)
        {
            try
            {
                if (operation.ToUpper() == OperationType.REMOVE.ToString())
                {
                    var garage = await _parkingDbContext.Parkings.Where(p => p.GarageId == garageId).SingleAsync();
                    garage.BookedParking--;
                    await _parkingDbContext.SaveChangesAsync();
                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task UpdateParkingSpotAsync(Guid garageId, string operation, CancellationToken cancellationToken)
        {
            try
            {
                if (operation.ToUpper() == OperationType.ADD.ToString())
                {
                    var garage = await _parkingDbContext.Parkings.Where(p => p.GarageId == garageId).FirstOrDefaultAsync();

                    if (garage == null)
                    {
                        //No session of that garage has started
                        var garageParking = new Parking
                        {
                            GarageId = garageId,
                            BookedParking = 1
                        };

                        await _parkingDbContext.Parkings.AddAsync(garageParking);
                        await _parkingDbContext.SaveChangesAsync();
                    }
                    else
                    {
                        garage.BookedParking++;
                        await _parkingDbContext.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
