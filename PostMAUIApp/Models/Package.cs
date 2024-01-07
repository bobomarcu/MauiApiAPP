using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostMAUIApp.Models
{
    public class Package
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public double Weight { get; set; }

        public string Status { get; set; }

        public string Address { get; set; }

        public string Sender { get; set; }

        public string Receiver { get; set; }

        public string PackageTitle => $"Package {ID}";

        public string StatusAndAddress => $"Status {Status}\nAddress {Address}";

    }
}
