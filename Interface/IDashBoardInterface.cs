using AlBayanWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlBayanWebAPI.Interface
{
   public  interface IDashBoardInterface
    {

        Task<IEnumerable<DashBoardData>> GetDashBoardData(string connection,filters filter);
        Task<IEnumerable<DashBoardData>> GetDashBoardDataDetails(string connection, filters filter);

        Task<IEnumerable<ClaimData>> GetClaimData(string connection, filters filter);
        Task<IEnumerable<ActivityDetails>> GetActivityData(string connection, filters filter);

        Task<IEnumerable<DenialCodes>> GetDenialCodes(string Connection);
        Task<IEnumerable<ObservationCodes>> GetObservationCodes(string Connection);

        Task<IEnumerable<ClaimsSummaryData>> GetSummaryData(string connection, filters filter);
        Task<IEnumerable<ICDCodes>> GetICDCodes(string Connection);

        Task<IEnumerable<Payers>> GetPayers(string Connection);
        Task<IEnumerable<Receivers>> GetReceivers(string Connection);
        Task<IEnumerable<Clinicians>> GetClinicians(string connection);

        Task<IEnumerable<CPTCodes>> GetCptCodes(string connection);
        Task<IEnumerable<DrugCodes>> GetDrugCodes(string connection);
        Task<IEnumerable<activityCodes>> GetActivity(string connection, searchActivity searchActivity);
      

        
    }
}
