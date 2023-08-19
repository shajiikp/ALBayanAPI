using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlBayanWebAPI.Models
{
    public class DashBoardData
    {
        public long _submittedDate { get; set; }
        
        public string _receiver { get; set; }
        public string _receiverCode { get; set; }
        public string _payer { get; set; }
        public string _payerCode { get; set; }
        public string claimId { get; set; }

        public float _grossAmount { get; set; }
        public float _patientShare { get; set; }
        public float _netAmount { get; set; }

        public float _approvedAmount { get; set; }

        public long _approvedDate { get; set; }
        public long _settlementDate { get; set; }

        public string _submissionType { get; set; }

        public int _claimStatus { get; set; }

        public List<ActivityDetails> activityDetails { get; set; }
    }
    /*
    public class ClaimData
    {
        //public string _fileName { get; set; }
        public long _submittedDate { get; set; }

        public string _receiver { get; set; }
        public string _receiverCode { get; set; }
        public string _payer { get; set; }
        public string _payerCode { get; set; }
        public string _claimId { get; set; }

        public float _grossAmount { get; set; }
        public float _patientShare { get; set; }
        public float _netAmount { get; set; }

        public float _approvedAmount { get; set; }

        public long _approvedDate { get; set; }
        public long _settlementDate { get; set; }
        /// <summary>
        /// 1:  First Submission  2 : Second Submission  3: Third Submission
        /// </summary>
        public int _submissionType { get; set; }

        public int _claimStatus { get; set; }

        public  string _paymentReference { get; set; }

        public long _fileId { get; set; }
        public long _linkId { get; set; }
    }
    */

    public class ClaimData
    {
        
        public string _receiver { get; set; }
        public string _receiverCode { get; set; }
        public string _payer { get; set; }
        public string _payerCode { get; set; }
        public string _claimId { get; set; }
        public List<ClaimSubmissions> ClaimSubmissions { get; set; }
        public List<ClaimRemittance> ClaimRemittance { get; set; }


    }
    public class ClaimSubmissions
    {
        public long _submittedDate { get; set; }
        public float _grossAmount { get; set; }
        public float _patientShare { get; set; }
        public float _netAmount { get; set; }
       
        /// <summary>
        /// 1:  First Submission  2 : Second Submission  3: Third Submission
        /// </summary>
        public int _submissionType { get; set; }     
        public long _fileId { get; set; }
        public long _linkId { get; set; }

        public string _fileName { get; set; }
    }
    public class ClaimRemittance
    {
        public long _submittedDate { get; set; }      
        public float _approvedAmount { get; set; }
        public long _approvedDate { get; set; }
        public long _settlementDate { get; set; }
        /// <summary>
        /// 1:  First Submission  2 : Second Submission  3: Third Submission
        /// </summary>
        public int _submissionType { get; set; }
        public int _claimStatus { get; set; }
        public string _paymentReference { get; set; }
        public string _fileName { get; set; }
        public long _fileId { get; set; }
        public long _linkId { get; set; }

    }
    public class ClaimsSummaryData
    {
        public string _fileName { get; set; }
        public long _submittedDate { get; set; }
        //public string _receiver { get; set; }
        public int _claimsCount { get; set; }      
        public float _patientShare { get; set; }
        public float _netAmount { get; set; }
        public float _approvedAmount { get; set; }    
        public long _fileId { get; set; }
       
    }
    public class ActivityDetails
    {

        public int _activityType { get; set; }
        public string _activityCode { get; set; }
        public string _activityDescription { get; set; }
        public double _activityNet { get; set; }
        public double _activityQuantity { get; set; }
        public double _activityApproved { get; set; }      
        public string _denialCode { get; set; }
        public string _clinicianCode { get; set; }
        public string _clinicianName { get; set; }
        public long _fileId { get; set; }
        public long _linkId { get; set; }



    }

    public class DashBoardDetails
    {
        public long _submittedDate { get; set; }
        public long _approvedDate { get; set; }

        public string _claimId { get; set; }

        public string _activityCode { get; set; }
        public string _activityDescription { get; set; }
        public string _clinicianCode { get; set; }

        public float _approvedAmount { get; set; }
        public string _denialCode { get; set; }
        public string _clinician { get; set; }

        public string _submissionType { get; set; }
        /// <summary>
        /// 1: paid 0  : Not PAid
        /// </summary>
        public int _claimStatus { get; set; }
        /// <summary>
        /// 1: Approved  0 : Not Approved
        /// </summary>
        public int _approvalStatus { get; set; }

        public string _receiver { get; set; }
        public string _receiverCode { get; set; }
        public string _payer { get; set; }
        public string _payerCode { get; set; }
        //public string claimId { get; set; }

        public float _grossAmount { get; set; }
        public float _patientShare { get; set; }
        public float _netAmount { get; set; }

       

       

       


    }
}
