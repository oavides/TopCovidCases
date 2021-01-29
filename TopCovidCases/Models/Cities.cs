using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TopCovidCases.Models
{
    public class Cities
    {
        public String name { get; set; }
        public String date { get; set; }
        public int? fips { get; set; }
        public int? confirmed { get; set; }
        public int? deaths { get; set; }
        public int? confirmed_diff { get; set; }
        public int? deaths_diff { get; set; }
        public String last_update { get; set; }
    }
}
