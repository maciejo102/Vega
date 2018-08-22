using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vega.Contract;
using Vega.Contract.Models;
using Vega.Extensions;

namespace Vega.Persistance
{
    public class VehicleDao : IVehicleDao
    {
        private readonly VegaDbContext context;

        public VehicleDao(VegaDbContext context)
        {
            this.context = context;
        }

        public void Add(Vehicle vehicle)
        {
            context.Add(vehicle);
        }

        public async Task<Vehicle> GetVehicleDbData(int id, bool includeRelated = true)
        {
            if(!includeRelated)
                return await context.Vehicles.FindAsync(id);

            return await context.Vehicles
                            .Include(v => v.Features)
                                .ThenInclude(vf => vf.Feature)
                            .Include(v => v.Model)
                                .ThenInclude(m => m.Make)
                            .SingleOrDefaultAsync(v => v.Id == id);
        }

        public async Task<IEnumerable<Vehicle>> GetAllVehicles(VehicleQuery queryObject) {
            var query = context.Vehicles
                        .Include(v => v.Model)
                            .ThenInclude(m => m.Make)
                        .Include(v => v.Features)
                            .ThenInclude(vf => vf.Feature)
                        .AsQueryable();


            // filtering
            if (queryObject.MakeId.HasValue)
                query = query.Where(v => v.Model.MakeId == queryObject.MakeId);
            
            if (queryObject.ModelId.HasValue)
                query = query.Where(v => v.Model.Id == queryObject.ModelId);

            // sorting
            var sortingColumnsMap = new Dictionary<string, Expression<Func<Vehicle, object>>>()
            {
                ["make"] = v => v.Model.Make.Name,
                ["model"] = v => v.Model.Name,
                ["contactName"] = v => v.ContactName,
            };
            
            query = query.ApplyOrdering(queryObject, sortingColumnsMap);
            
            return await query.ToListAsync();
        }

        

        public void Remove(Vehicle vehicle)
        {
            context.Remove(vehicle);
        }

    }
}