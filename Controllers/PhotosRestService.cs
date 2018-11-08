using System;
using System.Collections.Generic;
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
using Vega.Persistence;

namespace Vega.Controllers
{
    [Route("/api/vehicles/{vehicleId}/photos/")]
    public class PhotoRestService : Controller
    {
        public PhotoRestService(VegaDbContext context, 
            IMapper mapper, 
            IHostingEnvironment host,
            IVehicleDao vehicleDao,
            IUnitOfWork unitOfWork,
            IOptionsSnapshot<PhotoSettings> photoOptions,
            IPhotoDao photoDao)
        {
            this.host = host;
            this.vehicleDao = vehicleDao;
            this.unitOfWork = unitOfWork;
            this.photoDao = photoDao;
            this.context = context;
            this.mapper = mapper;
            photoSettings = photoOptions.Value;
        }

        [HttpGet]
        public async Task<IActionResult> GetPhotos(int vehicleId) 
        {
            var photos = await photoDao.GetPhotosDbSet(vehicleId);
            return Ok(mapper.Map<IEnumerable<Photo>, IEnumerable<PhotoResource>>(photos));
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
            
            // save photo to localstorage
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
        private readonly IPhotoDao photoDao;
        private readonly IMapper mapper;
        private readonly DbContext context;
        private readonly PhotoSettings photoSettings;
    }
}