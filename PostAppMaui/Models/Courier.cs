using System.ComponentModel.DataAnnotations;

namespace PostApplication.Models
{
    public class Courier
    {
        public int Id { get; set; }

        [Display(Name = "Courier First Name")]
        public string CourierFirstName { get; set; }

        [Display(Name = "Courier Last Name")]
        public string CourierLastName { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; } 

        public int? PostOfficeID { get; set; }
        public PostOffice? PostOffice { get; set; }

        public ICollection<Package>? AssignedPackages { get; set; }

        public string? FullName
        {
            get
            {
                return CourierFirstName + " " + CourierLastName;
            }
        }


    }
}
