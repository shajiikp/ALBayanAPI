using AlBayanWebAPI.Interface;
using AlBayanWebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AlBayanWebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClaimsController : BaseController
    {
        private readonly JWTSettings _jwtsettings;
        private readonly IClaimsInterface _claimsInterface;
        private static string Connection;
        public ClaimsController(IClaimsInterface claimsInterface, IOptions<JWTSettings> jwtsettings)
        {
            _claimsInterface = claimsInterface;
            _jwtsettings = jwtsettings.Value;
        }


        // GET: api/<DashBoardController>
        [HttpPost("GetClaimDetails")]
        public async Task<ActionResult<ResultJson>> GetClaimDetails(filters filter)
        {

            Connection = GetConnection();
            DataSet ds = new DataSet();
            ClaimDetailsData detailsData = new ClaimDetailsData();
           
            ResultJson result = new ResultJson();
            try
            {

                ds = await _claimsInterface.GetClaimsDataSet( filter, Connection);


                if (ds.Tables[0].Rows.Count ==0)
                {
                    result.Message = "No Data Found ";
                    this.HttpContext.Response.StatusCode = 500;
                }
                else
                {
                    result.Message = "Success";
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        detailsData = new ClaimDetailsData
                        {
                            _receiver = dr["Receiver"].ToString(),
                            _receiverCode = dr["RECEIVER_CODE"].ToString(),
                            _payer = dr["Payer"].ToString(),
                            _payerCode = dr["PAYER_CODE"].ToString(),
                            _claimId = dr["CLAIMID"].ToString(),
                            _memberId = dr["MEMBERID"].ToString(),
                            _emiratesId = dr["EMIRATESID"].ToString(),
                            claimSubmissions = (from drClaims in ds.Tables[1].AsEnumerable().Where(x => x.Field<string>("CSID") == dr["CLAIMID"].ToString())
                                                select new ClaimDetailsSubmissions()
                                                {
                                                    _submittedDate = Convert.ToInt64(drClaims["SUBMITTED_DATE"].ToString()),
                                                    _grossAmount = float.Parse(drClaims["GROSS"].ToString()),
                                                    _patientShare = float.Parse(drClaims["PATIENTSHARE"].ToString()),
                                                    _netAmount = float.Parse(drClaims["NET"].ToString()),
                                                    _approvedAmount = float.Parse(drClaims["RAAPPROVED"].ToString()),
                                                    _approvedtDate = Convert.ToInt64(drClaims["RA_SUBMITTED"].ToString()),
                                                    _settlementDate = Convert.ToInt64(drClaims["CLAIM_SETTLE_DATE"].ToString()),
                                                    _submissionType = Convert.ToInt32(drClaims["SubmissionType"].ToString()),
                                                    _claimStatus = Convert.ToInt32(drClaims["STATUS"].ToString()),
                                                    _idPayer = drClaims["CSIDPYR"].ToString(),
                                                    _paymentReference = drClaims["CSPR"].ToString(),
                                                    _fileId = Convert.ToInt64(drClaims["PKHD4"].ToString()),
                                                    _fileName= drClaims["FILE_NAME"].ToString(),
                                                    _diagnosis = (from drDiagnosis in ds.Tables[3].AsEnumerable().Where(x => x.Field<Int64>("PKCSID") == Convert.ToInt64(drClaims["PKCSID"].ToString()))
                                                                  select new Diagnosis()
                                                                  {
                                                                      _type = drDiagnosis["ICDTYP"].ToString(),
                                                                      _code = drDiagnosis["COD"].ToString(),
                                                                      _description = drDiagnosis["ShortDesc"].ToString(),

                                                                  }).ToList(),
                                                    _activities = (from drActivities in ds.Tables[2].AsEnumerable().Where(x => x.Field<Int64>("PKCSID") == Convert.ToInt64(drClaims["PKCSID"].ToString()))
                                                                   select new Activity()
                                                                   {

                                                                       _activityType = drActivities["ATYP"].ToString(),
                                                                       _activityCode = drActivities["COD"].ToString(),
                                                                       _activityDescription = drActivities["Description"].ToString(),
                                                                       _activityQuantity = Convert.ToDouble(drActivities["QTY"].ToString()),
                                                                       _activityNet = Convert.ToDouble(drActivities["CNET"].ToString()),
                                                                       _activityApproved = Convert.ToDouble(drActivities["APPROVED"].ToString()),
                                                                       _clinicianCode = drActivities["ACLN"].ToString(),
                                                                       _clinicianName = drActivities["CLINICIAN"].ToString(),
                                                                       _denialCode = drActivities["DENIALCODE"].ToString(),
                                                                       _observations = (from drObservaton in ds.Tables[4].AsEnumerable().Where(x => x.Field<Int64>("APKID") == Convert.ToInt64(drActivities["APKID"].ToString()))
                                                                                        select new Observation()
                                                                                        {

                                                                                            _observationType = drObservaton["OT"].ToString(),
                                                                                            _observationCode = drObservaton["OCOD"].ToString(),
                                                                                            _observationValue = drObservaton["OV"].ToString(),
                                                                                            _observationValueType = drObservaton["OVT"].ToString(),
                                                                                        }).ToList(),

                                                                   }).ToList(),

                                                }).ToList()
                        };

                    }


                result.Data.Add("claimDetails", detailsData);
                  

                }
            }
            catch (Exception ex)
            {
                result.Message = "API Error " + ex.Message;
                this.HttpContext.Response.StatusCode = 400;
            }


            return result;

            //var json = JsonConvert.SerializeObject(result);
            //var bytes = Encoding.UTF8.GetBytes(json);
            //var result_data = Ok(json);
            //Response.Headers.Add("Content-Length", bytes.Length.ToString());
            //return result_data;

        }


    }
}
