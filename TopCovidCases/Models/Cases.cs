using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TopCovidCases.Models
{
    public class Cases
    {
        public String date { get; set; }
        public int? confirmed { get; set; }
        public int? deaths { get; set; }
        public int? recovered { get; set; }
        public int? confirmed_diff { get; set; }
        public int? deaths_diff { get; set; }
        public int? recovered_diff { get; set; }
        public String last_update { get; set; }
        public int? active { get; set; }
        public int? active_diff { get; set; }
        public String fatality_rate { get; set; }
        public Region region { get; set; }
    }
}
