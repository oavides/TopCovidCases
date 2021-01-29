using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TopCovidCases.Models
{

    public class Region
    {
        public String iso { get; set; }
        public String name { get; set; }
        public String province { get; set; }
        public List<Cities> cities { get; set; }
    }
}
