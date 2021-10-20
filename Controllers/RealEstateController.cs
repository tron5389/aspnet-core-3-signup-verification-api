using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models.RealEstate;
using WebApi.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RealEstateController : BaseController
    {
        private readonly RealEstateService _realEstateService;
        public RealEstateController(RealEstateService realEstateService)
        {
            _realEstateService = realEstateService;
        }
        
        [HttpGet("rentals/{zipCode}")]
        public async Task<IList<RentalKPI>> GetRentalKPIsAsync(string zipCode)
        {
           return await _realEstateService.GetRentalKPIsAsync(zipCode);
        }

    }
}