using System;
using System.Collections.Generic;
using System.Text;

namespace BestBuyTests.Model.ForSampleData
{
    public class Address
    {
        public string type { get; set; }
        public int houseNumber { get; set; }
        public string city { get; set; }
    }

    public class RootDetails
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int employeeId { get; set; }
        public List<long> phoneNumber { get; set; }
        public List<Address> address { get; set; }
    }
}
