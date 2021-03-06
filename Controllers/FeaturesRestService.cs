using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vega.Contract.Models;
using Vega.Controllers.Resources;
using Vega.Persistence;

namespace Vega.Controllers
{
    public class FeaturesRestService : Controller
    {
        private readonly VegaDbContext context;
        private readonly IMapper mapper;
        public FeaturesRestService(VegaDbContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;
        }

        [HttpGet("/api/features")]
        public async Task<IEnumerable<KeyValuePairResource>> GetFeatures()
        {
            var features = await context.Features.ToListAsync();
            return mapper.Map<List<Feature>, List<KeyValuePairResource>>(features); 
        }
    }
}