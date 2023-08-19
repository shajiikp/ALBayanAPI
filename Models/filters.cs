using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlBayanWebAPI.Models
{
    public class filters
    {
        /// <summary>
        /// screenType 1  for UserData with response
        /// 2:EncounterType
        /// 3:ActivityType
        /// 4:EncounterStartType
        /// 5:EncounterEndType
        /// 6:DenialCodes
        /// 7:ObservationTypes
        /// 8:ObservationCodes
        /// 9:ObservationValueTypes
        /// 10:ActivityTypes
        /// 11:DiagnosisTypes
        /// 12:Payers
        /// 13:Receivers
        /// 14:CPTCodes
        /// 15:Clinicians
        /// 16: Urldata
        /// 17:Combourl
        /// 18:DrugCodes
        /// </summary>
        public int sortType { get; set; } = 0;
        public int sortOrder { get; set; } = 0;
        public int[] statusArray { get; set; }
        public int[] screenType { get; set; }
        public string? searchingText { get; set; }
        public int[] responseFormat { get; set; }
        public int limit { get; set; } = 0;
        public int skip { get; set; } = 0;
        public int Id { get; set; } = 0;
    }

    public class searchActivity
    {
        public int id { get; set; } = 0;      
        public string? searchingText { get; set; }     
        public int limit { get; set; } = 0;
        public int skip { get; set; } = 0;
      
    }
}
