using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlBayanWebAPI.Models
{  



    public class Header
    {

        public string _receiverCode { get; set; }
        public string _receiverName { get; set; }
        public string _payerCode { get; set; }

        public string _payerName { get; set; }

        public long _transactionDate { get; set; }

        public int _recordCount { get; set; }

        public string _flag { get; set; }


    }

    public class Submissions
    {
        public string _submissionType { get; set; }
        public string _receiver { get; set; }
        public string _payer { get; set; }
        public string _claimId { get; set; }
        public string _memberId { get; set; }
        public string _emiratesId { get; set; }
        public float _grossAmount { get; set; }
        public float _patientShare { get; set; }
        public float _netAmount { get; set; }
        public float _approvedAmount { get; set; }
        public float _rejectedAmount { get; set; }
        public List<ClaimDetails> _claimDetails { get; set; }
    }


    public class ClaimDetails
    {
        public string _idPayer { get; set; }       
        public float _grossAmount { get; set; }
        public float _patientShare { get; set; }
        public float _netAmount { get; set; }        
        public float _approvedAmount { get; set; }
        public long _submittedDate { get; set; }
        public string _fileName { get; set; }
        public long _dateSettlement { get; set; }
        public string  _paymentReference { get; set; }
        public List<Encounter> _encounter { get; set; }    

        public List<Diagnosis> _diagnosis  { get; set; }

        public List<Activity> _activities { get; set; }
    }

    public class Encounter
    {
        /// <summary>
        /// 1 =No Bed + No emergency room
        /// 2 = No Bed + Emergency room
        ///   3 = Inpatient Bed + No emergency room
        ///  4 = Inpatient Bed + Emergency room
        ///  5 = Daycase Bed + No emergency room
        ///  6 = Daycase Bed + Emergency room
        ///   7 = Nationals Screening
       ///  8 = New Visa Screening
        ///   9 = Renewal Visa Screening
     ///   10 = PRE-OP TEST PROCEDURES
       ///  12 = Home
        ///  13 = Assisted Living Facility
        ///   15 = Mobile Unit
        ///   41 = Ambulance - Land
        /// </summary>
        public int _encounterType { get; set; }

        public string _patientId { get; set; }

        public long _encounterStart { get; set; }

        public long _encounterEnd { get; set; }
        /// <summary>
        ///  1 = Elective
        ///  2 = Emergency
        ///  3 = Transfer
        ///  4 = Live birth
        ///  5 = Still birth
        ///  6 = Dead On Arrival
        ///  7 = Continuing Encounter.
        /// </summary>
        public int _encounterStartType { get; set; }
        /// <summary>
        ///   1 = Discharged with approval
        ///   2 = Discharged against advice
        ///   3 = Discharged absent without leave
        ///   4 = Discharge transfer to acute care
        ///   5 = Deceased
        ///   6 = Not discharged
        ///   7 = Discharge transfer to non-acute care(Transfer to long term care).
        ///   8 = Administrative discharge
        /// </summary>
        public int _encounterEndType { get; set; }
    }

    public class Diagnosis
    {
        /// <summary>
        /// Principal  Secondary   Admitting
        /// </summary>
        public string _type { get; set; }

         public string _code { get; set; }
        public string _description { get; set; }
    }

    public class Activity
    {
        //public string _activityId { get; set; }

        //public long _activityStart { get; set; }
        /// <summary>
        /// 3 = CPT; 4 = HCPCS; 5 = Drug; 6 = Dental; 8 = Service Code;9 = DRG; 10 = Scientific Code
        /// </summary>
        public string _activityType { get; set; }
        public string _activityCode { get; set; }
        public string _activityDescription { get; set; }
        public double _activityQuantity { get; set; }
        public double _activityNet { get; set; }
        public double _activityApproved { get; set; }
        public string _clinicianCode { get; set; }
        public string _clinicianName { get; set; }
        public string _denialCode { get; set; }     

        public List<Observation> _observations { get; set; }

    }

    public class Observation
    {
        /// <summary>
        /// LOINC  ,Text  ,   File   ,   Universal Dental  ,  Financial  ,  Grouping  ,   ERX     ,   Result
        /// </summary>
        public string _observationType { get; set; }
        public string _observationCode { get; set; }
        public string _observationValue { get; set; }
        public string _observationValueType { get; set; }
    }


    public class EncounterType
    {

        public int _encounterType { get; set; }
        public string _encounterTypeDescription { get; set; }
    }

    public class ActivityType
    {

        public int _activityType { get; set; }
        public string _activityTypeDescription { get; set; }
    }

    public class EncounterStartType
    {

        public int _encounterStartType { get; set; }
        public string _encounterStartTypeDescription { get; set; }
    }

    public class EncounterEndType
    {

        public int _encounterEndType { get; set; }
        public string _encounterEndTypeDescription { get; set; }
    }

    public class DenialCodes
    {

        public string _denialCode { get; set; }
        public string _denialCodeDescription { get; set; }
    }

   
    public class ObservationCodes
    {
        public string _observationType { get; set; }
        public string _observationCode { get; set; }
        public string _observationValueType { get; set; }
        public string _observationValueDataType { get; set; }
        public string _observationRemarks { get; set; }
    }

    public class ICDCodes
    {
        public long _icdId { get; set; }
        public string _icdCode { get; set; }
        public string _icdDescription { get; set; }

    }
    public class CPTCodes
    {
       
        public string _cptCode { get; set; }
        public string _description { get; set; }

    }

    public class DrugCodes
    {
        public string _drugCode { get; set; }
        public string _description { get; set; }

    }
    public class Payers
    {
       
        public string _code { get; set; }
        public string _name { get; set; }

    }
    public class Receivers
    {

        public string _code { get; set; }
        public string _name { get; set; }

    }
    public class Clinicians
    {

        public string _code { get; set; }
        public string _name { get; set; }

    }
    public class ClaimSummary
    {
     public ClaimDetailsData claimDetails { get; set; }
     public List<ClaimDetailsSubmissions> claimSubmissions { get; set; }
    }
    public class ClaimDetailsData
    {

        public string _receiver { get; set; }
        public string _receiverCode { get; set; }
        public string _payer { get; set; }
        public string _payerCode { get; set; }
        public string _claimId { get; set; }
        public string _memberId { get; set; }
        public string _emiratesId { get; set; }
       public List<ClaimDetailsSubmissions> claimSubmissions { get; set; }

    }

    public class ClaimDetailsSubmissions
    {

        public long _submittedDate { get; set; }      
        public float _grossAmount { get; set; }
        public float _patientShare { get; set; }
        public float _netAmount { get; set; }
        public float _approvedAmount { get; set; }
        public long _approvedtDate { get; set; }
        public long _settlementDate { get; set; }
        public int _submissionType { get; set; }
        public int _claimStatus { get; set; }           
        public string _paymentReference { get; set; }
        public string _idPayer { get; set; }
        public string _fileName { get; set; }
        public long _fileId { get; set; }
        public long _linkId { get; set; }
    
        public List<Diagnosis> _diagnosis { get; set; }
        public List<Activity> _activities { get; set; }
    }
    //public class ClaimDetailsSubmissions
    //{
    //    public long _submittedDate { get; set; }
    //    public float _grossAmount { get; set; }
    //    public float _patientShare { get; set; }
    //    public float _netAmount { get; set; }
    //    public float _approvedAmount { get; set; }
    //    public long _approvedDate { get; set; }
    //    public long _settlementDate { get; set; }
    //    /// <summary>
    //    /// 1:  First Submission  2 : Second Submission  3: Third Submission
    //    /// </summary>
    //    public int _submissionType { get; set; }
    //    public int _claimStatus { get; set; }
    //    public string _paymentReference { get; set; }
    //    public long _fileId { get; set; }
    //    public long _linkId { get; set; }
    //}


    public class ClaimSubmission
    {
        public string _payerId { get; set; }
        public string _receiverId { get; set; }
        public double _weight { get; set; }
    }


    public class activityCodes
    {

        public string _code { get; set; }
        public string _name { get; set; }

    }


}


