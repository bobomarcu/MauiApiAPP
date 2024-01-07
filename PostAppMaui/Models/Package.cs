using System.ComponentModel.DataAnnotations;
using System.Net;

namespace PostApplication.Models
{
    public class Package
    {
        public int ID { get; set; }

        [Display(Name = "Weight (in KG)")]
        public double? Weight { get; set; }

        [Display(Name = "Status")]
        public string? Status { get; set; }

        [Display(Name = "Sender")]
        public int SenderId { get; set; }
        public User? Sender { get; set; }

        [Display(Name = "Receiver")]
        public int ReceiverId { get; set; }
        public User? Receiver { get; set; }

        
        public int AssignedCourierId { get; set; }
        [Display(Name = "Assigned Courier")]
        public Courier? AssignedCourier { get; set; }

        [Display(Name = "Date of Creation")]
        [DataType(DataType.DateTime)]
        public DateTime CreationDate { get; set; } = DateTime.UtcNow.AddHours(2);

        [Display(Name = "Expected Arrival")]
        [DataType(DataType.DateTime)]
        public DateTime ExpectedArrival { get; set; } = DateTime.UtcNow.AddHours(2).AddDays(2);

        public int? PostOfficeID { get; set; }

        [Display(Name = "Post Office")]
        public PostOffice? PostOffice { get; set; }
        

    }
}
