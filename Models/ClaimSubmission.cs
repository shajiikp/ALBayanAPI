using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlBayanWebAPI.Models
{
  
    public class SClaimSubmission
    {
        public SHeader Header { get; set; }
        public List<SClaim> Claim { get; set; }
    }

    public class SHeader
    {
        public string SenderID { get; set; }
        public string ReceiverID { get; set; }
        public string TransactionDate { get; set; }
        public int RecordCount { get; set; }
        public string DispositionFlag { get; set; }
    }

    public class SClaim
    {
        public int ID { get; set; }
        public string MemberID { get; set; }
        public string PayerID { get; set; }
        public string ProviderID { get; set; }
        public string EmiratesIDNumber { get; set; }
        public int Gross { get; set; }
        public int PatientShare { get; set; }
        public int Net { get; set; }
        public SEncounter Encounter { get; set; }
        public SDiagnosis Diagnosis { get; set; }
        public List<SActivity> Activity { get; set; }
    }

    public class SEncounter
    {
        public string FacilityID { get; set; }
        public int Type { get; set; }
        public int PatientID { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public int StartType { get; set; }
        public int EndType { get; set; }
    }

    public class SDiagnosis
    {
        public string Type { get; set; }
        public string Code { get; set; }
        public List<DxInfo> DxInfo { get; set; }
    }

    public class SActivity
    {
        public int ID { get; set; }
        public string Start { get; set; }
        public int Type { get; set; }
        public string Code { get; set; }
        public int Quantity { get; set; }
        public int Net { get; set; }
        public string Clinician { get; set; }
        public List<SObservation> Observation { get; set; }
    }

    public class SObservation
    {
        public string Type { get; set; }
        public string Code { get; set; }
        public string Value { get; set; }
        public string ValueType { get; set; }
    }

    public class DxInfo
    {
        /// <summary>
        ///  "Y"= Yes. Definition: present at the time of inpatient admission.
        ///  "N"= No.Definition: not present at the time of inpatient ad.
        ///  "U"= Unknown.Definition: documentation is insufficient to determine if condition is present on admission.
        ///  "W"= Clinically Undetermined.Definition: provider is unable tp clinically determine whether condition was present on admission or not.
        ///  "1"= Unreported/Not used. Definition: exempt from POA reporting. 
        ///  "OP"= Outpatient claim. Definition: this is an outpatient claim which does not require DRGsin Dubai (for the time being).   
        /// </summary>
        public string DxInfoType { get; set; }
        public string DxInfoCode { get; set; }
       
    }


}
