using System.ComponentModel.DataAnnotations;

namespace PostApplication.Models
{
    public class Cashier
    {
        public int Id { get; set; }
        [Display(Name = "Cashier First Name")]
        public string CashierFirstName { get; set; }
        [Display(Name = "Cashier Last Name")]
        public string CashierLastName { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        public int? PostOfficeID { get; set; }
        public PostOffice? PostOffice { get; set; }

        public string? FullName
        {
            get
            {
                return CashierFirstName + " " + CashierLastName;
            }
        }
    }
}
