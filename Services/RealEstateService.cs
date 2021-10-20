using System.Collections.Generic;
using Newtonsoft.Json;
using WebApi.Models.RealEstate;
using WebApi.DataHelpers;
using System.Linq;
using WebApi.Helpers;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Services
{
    public class RealEstateService
    {
        private readonly DataContext _context;

        public RealEstateService(DataContext context)
        {
            _context = context;
        }
        private IList<RentalKPI> GetRealEstateInfoFromWeb(string zipCode, int offset = 0)
        {
            
            var data = new RealtorDataAccess(zipCode);
                var stringContent = data.GetRentData().Result;
                var rentData = JsonConvert.DeserializeObject<RealtorResponse>(stringContent);
                var rentalKPIList = new List<RentalKPI>();
                var responseConverter = new ResponseConverter();
                foreach (var listing in rentData.listings)
                {
                    rentalKPIList.Add(new RentalKPI
                    {
                        PropertyId = listing.property_id,
                        ListingId = listing.listing_id,
                        ZipCode = responseConverter.GetZipCode(listing,zipCode),
                        Address = listing.address,
                        PropertyType = listing.prop_type,
                        LastUpdated = listing.last_update,
                        MonthlyRent = responseConverter.GetRent(listing),
                        Beds = responseConverter.GetBeds(listing),
                        Baths = responseConverter.GetBaths(listing),
                        Sqft = responseConverter.GetSqft(listing),
                        Lat = listing.lat,
                        Lon = listing.lon
                    });
                }
                rentalKPIList = rentalKPIList.Where(list => list.Beds > 0).ToList();
                return rentalKPIList;
        }

        private async Task<IList<RentalKPI>> SaveRealEstateDataAsync(string zipCode)
        {
            var listingsToSave = GetRealEstateInfoFromWeb(zipCode);
            foreach(var listing in listingsToSave)
            {
                _context.RentalKPIs.Add(listing);
            }
            await _context.SaveChangesAsync();
            return listingsToSave;
        }

        public async Task<IList<RentalKPI>> GetRentalKPIsAsync(string zipCode)
        {
            var rentalList = await _context.RentalKPIs.Where(x => x.ZipCode == zipCode).ToListAsync();
            if(rentalList != null && rentalList.Any())
            {
                return rentalList;
            }
            
           return await SaveRealEstateDataAsync(zipCode);

        }
    }
}