using AlBayanWebAPI.Context;
using AlBayanWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AlBayanWebAPI.Interface
{
    public class ClaimsInterface : IClaimsInterface
    {

        private readonly SqlConnectionConfiguration _configuration;
        public ClaimsInterface(SqlConnectionConfiguration configuration)
        {
            _configuration = configuration;
        }
        //public async Task<IEnumerable<ClaimDetailsData>> GetClaimsData(filters filter,string connection)
        //{
        //    ClaimDetailsData> obj = new List<ClaimDetailsData>();
        //    DataSet ds = new DataSet();
        //    try
        //    {
        //        using (SqlConnection con = new SqlConnection(connection))
        //        {
        //            const string query = "sp_GetClaimsDetailsByClaimId";
        //            SqlCommand cmd = new SqlCommand(query, con)
        //            {
        //                CommandType = CommandType.StoredProcedure,
        //            };
                   
        //            cmd.Parameters.AddWithValue("@SearchKey", filter.searchingText);
        //            con.Open();

        //            SqlDataAdapter da = new SqlDataAdapter(cmd);
        //            try
        //            {
        //                await Task.Run(() => da.Fill(ds));
        //                if (ds.Tables[0].Rows.Count > 0)
        //                {
                            
                              
        //                    foreach (DataRow dr in ds.Tables[0].Rows)
        //                    {
        //                        obj.Add(new ClaimDetailsData
        //                        {                                   
        //                          _receiver= dr["Receiver"].ToString(),
        //                          _receiverCode = dr["RECEIVER_CODE"].ToString(),
        //                          _payer = dr["Payer"].ToString(),
        //                          _payerCode = dr["PAYER_CODE"].ToString(),
        //                          _claimId = dr["CLAIMID"].ToString(),
        //                          _memberId = dr["MEMBERID"].ToString(),
        //                          _emiratesId = dr["EMIRATESID"].ToString(),
        //                            //_grossAmount =float.Parse( dr["GROSS"].ToString()),
        //                            //_patientShare = float.Parse(dr["SHARE"].ToString()),
        //                            //_netAmount = float.Parse(dr["NET"].ToString()),
        //                            //_approvedAmount = float.Parse(dr["APPROVED"].ToString()),
        //                            //_rejectedAmount = float.Parse(dr["REJECTED"].ToString()),
        //                            claimSubmissions = (from drClaims in ds.Tables[1].AsEnumerable().Where(x => x.Field<string>("CSID") == dr["CLAIMID"].ToString())
        //                             select new ClaimDetailsSubmissions()
        //                             {
        //                                 _submittedDate= Convert.ToInt64(drClaims["SUBMITTED_DATE"].ToString()),                                                                    
        //                               _grossAmount = float.Parse(drClaims["GROSS"].ToString()),
        //                               _patientShare = float.Parse(drClaims["PATIENTSHARE"].ToString()),
        //                               _netAmount = float.Parse(drClaims["NET"].ToString()),
        //                               _approvedAmount = float.Parse(drClaims["RAAPPROVED"].ToString()),
        //                                 _approvedtDate = Convert.ToInt64(drClaims["RA_SUBMITTED"].ToString()),
        //                                 _settlementDate = Convert.ToInt64(drClaims["CLAIM_SETTLE_DATE"].ToString()),
        //                                 _submissionType = drClaims["SubmissionType"].ToString(),
        //                                 _claimStatus= Convert.ToInt32(drClaims["STATUS"].ToString()),
        //                                 _idPayer = drClaims["CSIDPYR"].ToString(),
        //                                 _paymentReference = drClaims["CSPR"].ToString(),
        //                                  _fileId = Convert.ToInt64(drClaims["PKHD4"].ToString()),
        //                                 _diagnosis = (from drDiagnosis in ds.Tables[3].AsEnumerable().Where(x => x.Field<Int64>("PKCSID") == Convert.ToInt64(drClaims["PKCSID"].ToString()))
        //                                            select new Diagnosis()
        //                                            {
        //                                                _type= drDiagnosis["ICDTYP"].ToString(),
        //                                                _code = drDiagnosis["COD"].ToString(),
        //                                                _description= drDiagnosis["ShortDesc"].ToString(),

        //                                            }).ToList(),
        //                               _activities = (from drActivities in ds.Tables[2].AsEnumerable().Where(x => x.Field<Int64>("PKCSID") == Convert.ToInt64(drClaims["PKCSID"].ToString()))
        //                                              select new Activity()
        //                                              {
                                                         
        //                                                  _activityType= drActivities["ATYP"].ToString(),
        //                                                  _activityCode = drActivities["COD"].ToString(),
        //                                                  _activityDescription = drActivities["Description"].ToString(),
        //                                                  _activityQuantity = drActivities["QTY"].ToString(),
        //                                                  _activityNet= drActivities["CNET"].ToString(),
        //                                                  _activityPaid = drActivities["APPROVED"].ToString(),
        //                                                  _activityClinician = drActivities["ACLN"].ToString(),
        //                                                  _activityClinicianName= drActivities["CLINICIAN"].ToString(),
        //                                                  _activityDenialCode = drActivities["DENIALCODE"].ToString(),
        //                                                  _observations = (from drObservaton in ds.Tables[4].AsEnumerable().Where(x => x.Field<Int64>("APKID") == Convert.ToInt64(drActivities["APKID"].ToString()))
        //                                                                  select new Observation()
        //                                                                  {

        //                                                                      _observationType= drObservaton["OT"].ToString(),
        //                                                                      _observationCode = drObservaton["OCOD"].ToString (),
        //                                                                      _observationValue= drObservaton["OV"].ToString(),
        //                                                                      _observationValueType= drObservaton["OVT"].ToString(),
        //                                                                  }).ToList(),

        //                                              }).ToList(),
        //                               //_encounter = (from drRates in ds.Tables[1].AsEnumerable().Where(x => x.Field<Int64>("ProductId") == Convert.ToInt64(dr["ID"].ToString()))
        //                               //              select new Encounter()
        //                               //              {
        //                               //                  _encounterType= Convert.ToInt32(dr["RateId"].ToString()),
        //                               //                  _patientId= dr["observationcode"].ToString(),
        //                               //                  _encounterStart = Convert.ToInt64( dr["RateId"].ToString()),
        //                               //                  _encounterEnd= Convert.ToInt64(dr["RateId"].ToString()),
        //                               //                  _encounterStartType= Convert.ToInt32(dr["RateId"].ToString()),
        //                               //                  _encounterEndType = Convert.ToInt32(dr["RateId"].ToString()),

        //                               //              }).ToList(),
        //                             }).ToList()                                  
        //                        });
        //                    }
        //                }


        //                con.Close();
        //                cmd.Dispose();
        //            }
        //            catch (Exception ex)
        //            {
        //                con.Close();
        //                cmd.Dispose();
        //                throw ex;
        //            }
        //            //UserId = (Int64)cmd.ExecuteScalar();
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        return obj;
        //    }
        //    return obj;
        //}

        public async Task<DataSet> GetClaimsDataSet(filters filter, string Connection)
        {
          
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    const string query = "sp_GetClaimsDetailsByClaimId";
                    SqlCommand cmd = new SqlCommand(query, con)
                    {
                        CommandType = CommandType.StoredProcedure,
                    };

                    cmd.Parameters.AddWithValue("@SearchKey", filter.searchingText);
                    con.Open();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    try
                    {
                        await Task.Run(() => da.Fill(ds));
                        con.Close();
                        cmd.Dispose();
                    }
                    catch (Exception ex)
                    {
                        con.Close();
                        cmd.Dispose();
                        throw ex;
                    }
                    //UserId = (Int64)cmd.ExecuteScalar();
                }

            }
            catch (Exception ex)
            {
                return ds;
            }
            return ds;
        }
    }
}
