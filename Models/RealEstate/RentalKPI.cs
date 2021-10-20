using System;
//spnet_core_3_signup_verification_api.Models.RealEstate
namespace WebApi.Models.RealEstate
{
    public class RentalKPI
    {
        public int Id { get; set; }
        public string PropertyId { get; set; }
        public string ListingId { get; set; }
        public string ZipCode {get; set;}
        public string Address { get; set; }
        public string PropertyType { get; set; }
        public DateTime LastUpdated { get; set; }
        public double MonthlyRent { get; set; }
        public float Beds { get; set; }
        public float Baths { get; set; }
        public int Sqft { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }

        public double PricePerSqFt => MonthlyRent / Sqft;

        public double NormalPricePerSqFt => PricePerSqFt * (Baths / Beds);
    
    }
}