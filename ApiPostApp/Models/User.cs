using System.ComponentModel.DataAnnotations;

namespace PostApplication.Models
{
    public class User
    {
        public int Id { get; set; }

        [Display(Name ="First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        public string Country { get; set; }
        public string County { get; set; }

        [Display(Name = "City")]
        public string CityAddress { get; set; }

        [Display(Name = "Street")]
        public string StreetAddress { get; set; }

        public string? FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

    }
}
