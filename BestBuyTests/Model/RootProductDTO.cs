using System;
using System.Collections.Generic;
using System.Text;

namespace BestBuyTests.Model
{
    public class RootProductDTO
    {
        public int total { get; set; }
        public int limit { get; set; }
        public int skip { get; set; }
        public List<DatumDTO> data { get; set; }
    }
}
