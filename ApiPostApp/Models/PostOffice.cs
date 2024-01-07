using System.ComponentModel.DataAnnotations;

namespace PostApplication.Models
{
    public class PostOffice
    {
        [Display(Name = "Office Number")]
        public int Id { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        public string Country { get; set; }
        public string County { get; set; }

        [Display(Name = "City")]
        public string CityAddress { get; set; }

        [Display(Name = "Street")]
        public string StreetAddress { get; set; }
    
        public ICollection<Cashier>? CashiersList { get; set; }
        public ICollection<Package>? PackagesList { get; set; }
        public ICollection<Courier>? CouriersList { get;set; }
           
    }
}
