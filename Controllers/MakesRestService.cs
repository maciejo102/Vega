using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vega.Contract.Models;
using Vega.Controllers.Resources;
using Vega.Persistance;

namespace Vega.Controllers
{
    public class MakesRestService : Controller
    {
        public MakesRestService(VegaDbContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;

        }
        [HttpGet("/api/makes")]
        public async Task<IEnumerable<MakeResource>> GetMakes()
        {
            var makes = await context.Makes.Include(m => m.Models).ToListAsync();
            return mapper.Map<List<Make>, List<MakeResource>>(makes);
        }


        private readonly VegaDbContext context;
        private readonly IMapper mapper;
    }
}