using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TopCovidCases.Models
{
    public interface IDataAccess
    {
        public Task<IList<Region>> ListRegion();
        public Task<IList<Cases>> ListCases(String iso);
    }
}
