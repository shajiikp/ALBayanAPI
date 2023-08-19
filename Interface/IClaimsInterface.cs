using AlBayanWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AlBayanWebAPI.Interface
{
    public interface IClaimsInterface
    {
        //Task<IEnumerable<ClaimDetailsData>> GetClaimsData(filters filter,string Connection);
        Task<DataSet> GetClaimsDataSet(filters filter, string Connection);
    }
}
