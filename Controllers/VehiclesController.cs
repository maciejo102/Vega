using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vega.Controllers.Resources;
using Vega.Contract.Models;
using Vega.Contract;

namespace Vega.Controllers
{
    [Route("/api/vehicles")]
    public class VehiclesController : Controller
    {
        public VehiclesController(IMapper mapper, IVehicleDao vehicleDao, IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.vehicleDao = vehicleDao;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateVehicle([FromBody] SaveVehicleResource vehicleResource)
        {
            throw new Exception();
            if (!ModelState.IsValid) // it looks on data annotations of VehicleResource
                return BadRequest(ModelState);

            // other validation 
            // if (true)
            // {
            //     ModelState.AddModelError("key", "exception");
            //     return BadRequest(ModelState);
            // }

            var vehicle = mapper.Map<SaveVehicleResource, Vehicle>(vehicleResource);
            vehicle.LastUpdate = DateTime.Now;

            vehicleDao.Add(vehicle);
            await unitOfWork.CompleteAsync();

            vehicle = await vehicleDao.GetVehicleDbData(vehicle.Id);

            var result = mapper.Map<Vehicle, VehicleResource>(vehicle);
            return Ok(result);
        }


        [HttpPut("{id}")] // /api/vehicles/{id}
        public async Task<IActionResult> UpdateVehicle(int id, [FromBody] SaveVehicleResource vehicleResource)
        {
            if (!ModelState.IsValid) // it looks on data annotations of VehicleResource
                return BadRequest(ModelState);

            var vehicle = await vehicleDao.GetVehicleDbData(id);

            // validation
            if (vehicle == null)
                return NotFound();

            // here save is being done
            mapper.Map<SaveVehicleResource, Vehicle>(vehicleResource, vehicle);
            vehicle.LastUpdate = DateTime.Now;

            await unitOfWork.CompleteAsync();

            vehicle = await vehicleDao.GetVehicleDbData(vehicle.Id);

            var result = mapper.Map<Vehicle, VehicleResource>(vehicle);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var vehicle = await vehicleDao.GetVehicleDbData(id, includeRelated: false);

            if (vehicle == null)
                return NotFound();

            vehicleDao.Remove(vehicle);
            await unitOfWork.CompleteAsync();

            return Ok(id);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicle(int id)
        {
            var vehicle = await vehicleDao.GetVehicleDbData(id);

            if (vehicle == null)
                return NotFound();

            var result = mapper.Map<Vehicle, VehicleResource>(vehicle);

            return Ok(result);
        }


        private readonly IMapper mapper;
        private readonly IVehicleDao vehicleDao;
        private readonly IUnitOfWork unitOfWork;
    }
}