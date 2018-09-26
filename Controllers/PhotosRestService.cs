using System;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Vega.Contract;
using Vega.Contract.Models;
using Vega.Controllers.Resources;
using Vega.Persistance;

namespace Vega.Controllers
{
    [Route("/api/vehicles/{vehicleId}/photos/")]
    public class PhotosRestService : Controller
    {
        public PhotosRestService(VegaDbContext context, 
            IMapper mapper, 
            IHostingEnvironment host,
            IVehicleDao vehicleDao,
            IUnitOfWork unitOfWork,
            IOptionsSnapshot<PhotoSettings> photoOptions)
        {
            this.host = host;
            this.vehicleDao = vehicleDao;
            this.unitOfWork = unitOfWork;
            this.context = context;
            this.mapper = mapper;
            photoSettings = photoOptions.Value;
        }

        [HttpPost]
        public async Task<IActionResult> UploadAsync(int vehicleId, IFormFile file)
        {
            var vehicle = await vehicleDao.GetVehicleDbData(vehicleId, includeRelated: false);
            
            if (vehicle == null)
             return NotFound();

            if (file == null) return BadRequest("Null file.");
            if (file.Length == 0) return BadRequest("Empty file.");
            if (!photoSettings.IsSupported(file.FileName)) return BadRequest("Unsupported file type.");
            
            
            var photosStoragePath = Path.Combine(host.WebRootPath, "uploads");
            if (!Directory.Exists(photosStoragePath))
                Directory.CreateDirectory(photosStoragePath);

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(photosStoragePath, fileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create)){
                await file.CopyToAsync(fileStream);
            }

            var photo = new Photo{ Name = fileName };
            vehicle.Photos.Add(photo);
            await unitOfWork.CompleteAsync();

            return Ok(mapper.Map<Photo, PhotoResource>(photo));
        }


        private readonly IHostingEnvironment host;
        private readonly IVehicleDao vehicleDao;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly DbContext context;
        private readonly PhotoSettings photoSettings;
    }
}