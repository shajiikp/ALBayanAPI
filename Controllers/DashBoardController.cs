using AlBayanWebAPI.Interface;
using AlBayanWebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;



// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AlBayanWebAPI.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DashBoardController : BaseController
    {

        private readonly JWTSettings _jwtsettings;
        private readonly IDashBoardInterface _dashboardRepo;
        private static string Connection;

        public DashBoardController(IDashBoardInterface dashboardRepo, IOptions<JWTSettings> jwtsettings)
        {
            _dashboardRepo = dashboardRepo;
            _jwtsettings = jwtsettings.Value;
        }


        // GET: api/<DashBoardController>
        [HttpPost("GetDashBoardData")]
        public async Task<ActionResult<ResultJson>> GetDashBoardData(filters filter)
        {

            Connection = GetConnection();
          
            ResultJson result = new ResultJson();
            try
            {
              
                var dashboarddata = await _dashboardRepo.GetDashBoardData(Connection,filter);


                if (dashboarddata == null)
                {
                    result.Message = "No Data Found ";
                    this.HttpContext.Response.StatusCode = 500;
                }
                else
                {
                    result.Message = "Success";
                    result.Data.Add("dashBoardData", dashboarddata);
                   
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

        [HttpPost("GetDashBoardDataDetails")]
        public async Task<ActionResult<ResultJson>> GetDashBoardDataDetails(filters filter)
        {

            Connection = GetConnection();

            ResultJson result = new ResultJson();
            try
            {
                if (filter.screenType.Contains(16))
                {
                    result.Message = "Success";
                    result.Data.Add("urlData", "https://dotnetlamda.s3.ap-south-1.amazonaws.com/claimsdata.json");
                }

                else
                {
                    var summaryData = await _dashboardRepo.GetSummaryData(Connection, filter);
                    var dashboarddata = await _dashboardRepo.GetClaimData(Connection, filter);
                    var activitydata = await _dashboardRepo.GetActivityData(Connection, filter);

                    if (dashboarddata == null)
                    {
                        result.Message = "No Data Found ";
                        this.HttpContext.Response.StatusCode = 500;
                    }
                    else
                    {
                        result.Message = "Success";
                        result.Data.Add("fileData", summaryData);
                        result.Data.Add("dashBoardData", dashboarddata);
                        result.Data.Add("activityDetails", activitydata);
                        ////if (filter.screenType[0] == 1)
                        //{
                        //    var tblUser = _context.Users
                        //      .Where(user => user.UserId == UserID)
                        //      .FirstOrDefault();
                        //    result.Data.Add("userDetails", tblUser);

                        //}


                    }

                    if (filter.screenType.Contains(2))
                    {
                        List<EncounterType> encounterTypes = new List<EncounterType>
                        {
                            new EncounterType { _encounterType = 1 ,_encounterTypeDescription = "No Bed + No emergency room"},
                            new EncounterType { _encounterType = 2 ,_encounterTypeDescription = "No Bed + Emergency room"},
                            new EncounterType { _encounterType = 3 ,_encounterTypeDescription = "Inpatient Bed + No emergency room"},
                            new EncounterType { _encounterType = 4 ,_encounterTypeDescription = "Inpatient Bed + Emergency room"},
                            new EncounterType { _encounterType = 5 ,_encounterTypeDescription = "Daycase Bed + No emergency room"},
                            new EncounterType { _encounterType = 6 ,_encounterTypeDescription = "Daycase Bed + Emergency room"},
                            new EncounterType { _encounterType = 7 ,_encounterTypeDescription = "Nationals Screening"},
                            new EncounterType { _encounterType = 8 ,_encounterTypeDescription = "New Visa Screening"},
                            new EncounterType { _encounterType = 9 ,_encounterTypeDescription = "Renewal Visa Screening"},
                            new EncounterType { _encounterType = 10 ,_encounterTypeDescription = "PRE-OP TEST PROCEDURES"},
                            new EncounterType { _encounterType = 11 ,_encounterTypeDescription = "Home"},
                             new EncounterType { _encounterType = 12 ,_encounterTypeDescription = "Assisted Living Facility"},
                            new EncounterType { _encounterType = 13 ,_encounterTypeDescription = "Mobile Unit"},
                            new EncounterType { _encounterType = 14 ,_encounterTypeDescription = "Ambulance - Land"},
                        };

                        result.Data.Add("encounterTypes", encounterTypes);
                    }
                    if (filter.screenType.Contains(3))
                    {
                        List<ActivityType> activityTypes = new List<ActivityType>
                        {
                            new ActivityType { _activityType = 3 ,_activityTypeDescription = "CPT"},
                            new ActivityType { _activityType = 4 ,_activityTypeDescription = "HCPCS"},
                            new ActivityType { _activityType = 5 ,_activityTypeDescription = "Drug"},
                            new ActivityType { _activityType = 6 ,_activityTypeDescription = "Denial"},
                            new ActivityType { _activityType = 8 ,_activityTypeDescription = "Service Code"},
                            new ActivityType { _activityType = 9 ,_activityTypeDescription = "DRG"},
                            new ActivityType { _activityType = 10 ,_activityTypeDescription = "Sceintific Code"},
                        };

                        result.Data.Add("activityTypes", activityTypes);
                    }


                    if (filter.screenType.Contains(4))
                    {
                        List<EncounterStartType> encounterStartTypes = new List<EncounterStartType>
                        {
                            new EncounterStartType { _encounterStartType = 1 ,_encounterStartTypeDescription = "Elective"},
                            new EncounterStartType {_encounterStartType = 2 ,_encounterStartTypeDescription= "Emergency"},
                            new EncounterStartType {_encounterStartType = 3 ,_encounterStartTypeDescription= "Transfer"},
                            new EncounterStartType {_encounterStartType = 4 ,_encounterStartTypeDescription= "Live Birth"},
                            new EncounterStartType {_encounterStartType = 5 ,_encounterStartTypeDescription= "Still Birth"},
                            new EncounterStartType {_encounterStartType = 6 ,_encounterStartTypeDescription= "Dead On Arrival"},
                            new EncounterStartType {_encounterStartType = 7, _encounterStartTypeDescription = "Continuing Encounter"},
                        };

                        result.Data.Add("encounterStartTypes", encounterStartTypes);
                    }

                    if (filter.screenType.Contains(5))
                    {
                        List<EncounterEndType> encounterEndTypes = new List<EncounterEndType>
                        {
                            new EncounterEndType { _encounterEndType = 1 ,_encounterEndTypeDescription = "Discharged with approval"},
                            new EncounterEndType {_encounterEndType = 2 ,_encounterEndTypeDescription= "Discharged against advice"},
                            new EncounterEndType {_encounterEndType = 3 ,_encounterEndTypeDescription= "Discharged absent without leave"},
                            new EncounterEndType {_encounterEndType = 4 ,_encounterEndTypeDescription= "Discharge transfer to acute care"},
                            new EncounterEndType {_encounterEndType = 5 ,_encounterEndTypeDescription= "Deceased"},
                            new EncounterEndType {_encounterEndType = 6 ,_encounterEndTypeDescription= "Not discharged"},
                            new EncounterEndType {_encounterEndType = 7, _encounterEndTypeDescription = "Discharge transfer to non-acute care(Transfer to long term care)"},
                                new EncounterEndType {_encounterEndType = 8 ,_encounterEndTypeDescription= "Administrative discharge"},
                        };

                        result.Data.Add("encounterEndTypes", encounterEndTypes);
                    }


                    if (filter.screenType.Contains(6))
                    {

                        var denialCodes = await _dashboardRepo.GetDenialCodes(Connection);
                        result.Data.Add("denialCodes", denialCodes);
                    }

                }
              
            }
            catch (Exception ex)
            {
                result.Message = "API Error " + ex.Message;
                this.HttpContext.Response.StatusCode = 400;
            }

            //var data = JsonConvert.SerializeObject(result);
            //var content = Encoding.UTF8.GetBytes(data);

            //var json = System.Text.Json.JsonSerializer.Serialize(result);

            //HttpContext.Response.ContentType = "application/json";
            //HttpContext.Response.ContentLength = json.Length;
            return result;
        }

        [HttpPost("GetICDCode")]
        public async Task<ActionResult<ResultJson>> GetICDCode(filters filter)
        {

            Connection = GetConnection();

            ResultJson result = new ResultJson();
            try
            {
            }
            catch (Exception ex)
            {
                result.Message = "API Error " + ex.Message;
                this.HttpContext.Response.StatusCode = 400;
            }

           
            return result;
        }

        [HttpPost("GetAllComboDatas")]
        public async Task<ActionResult<ResultJson>> GetAllComboDatas(filters filter)
        {

            Connection = GetConnection();

            ResultJson result = new ResultJson();
            try
            {
              
                if (filter.screenType.Contains(17))
                {
                    result.Message = "Success";
                    result.Data.Add("comboUrl", "https://dotnetlamda.s3.ap-south-1.amazonaws.com/ComboBox.json");
                }
                else
                {
                    if (filter.screenType.Contains(2))
                    {
                        List<EncounterType> encounterTypes = new List<EncounterType>
                        {
                            new EncounterType { _encounterType = 1 ,_encounterTypeDescription = "No Bed + No emergency room"},
                            new EncounterType { _encounterType = 2 ,_encounterTypeDescription = "No Bed + Emergency room"},
                            new EncounterType { _encounterType = 3 ,_encounterTypeDescription = "Inpatient Bed + No emergency room"},
                            new EncounterType { _encounterType = 4 ,_encounterTypeDescription = "Inpatient Bed + Emergency room"},
                            new EncounterType { _encounterType = 5 ,_encounterTypeDescription = "Daycase Bed + No emergency room"},
                            new EncounterType { _encounterType = 6 ,_encounterTypeDescription = "Daycase Bed + Emergency room"},
                            new EncounterType { _encounterType = 7 ,_encounterTypeDescription = "Nationals Screening"},
                            new EncounterType { _encounterType = 8 ,_encounterTypeDescription = "New Visa Screening"},
                            new EncounterType { _encounterType = 9 ,_encounterTypeDescription = "Renewal Visa Screening"},
                            new EncounterType { _encounterType = 10 ,_encounterTypeDescription = "PRE-OP TEST PROCEDURES"},
                            new EncounterType { _encounterType = 11 ,_encounterTypeDescription = "Home"},
                            new EncounterType { _encounterType = 12 ,_encounterTypeDescription = "Assisted Living Facility"},
                            new EncounterType { _encounterType = 13 ,_encounterTypeDescription = "Mobile Unit"},
                            new EncounterType { _encounterType = 14 ,_encounterTypeDescription = "Ambulance - Land"},
                        };

                        result.Data.Add("encounterTypes", encounterTypes);
                    }
                    if (filter.screenType.Contains(3))
                    {
                        List<ActivityType> activityTypes = new List<ActivityType>
                        {
                            new ActivityType { _activityType = 3 ,_activityTypeDescription = "CPT"},
                            new ActivityType { _activityType = 4 ,_activityTypeDescription = "HCPCS"},
                            new ActivityType { _activityType = 5 ,_activityTypeDescription = "Drug"},
                            new ActivityType { _activityType = 6 ,_activityTypeDescription = "Denial"},
                            new ActivityType { _activityType = 8 ,_activityTypeDescription = "Service Code"},
                            new ActivityType { _activityType = 9 ,_activityTypeDescription = "DRG"},
                            new ActivityType { _activityType = 10 ,_activityTypeDescription = "Sceintific Code"},
                        };

                        result.Data.Add("activityTypes", activityTypes);
                    }


                    if (filter.screenType.Contains(4))
                    {
                        List<EncounterStartType> encounterStartTypes = new List<EncounterStartType>
                        {
                            new EncounterStartType { _encounterStartType = 1 ,_encounterStartTypeDescription = "Elective"},
                            new EncounterStartType {_encounterStartType = 2 ,_encounterStartTypeDescription= "Emergency"},
                            new EncounterStartType {_encounterStartType = 3 ,_encounterStartTypeDescription= "Transfer"},
                            new EncounterStartType {_encounterStartType = 4 ,_encounterStartTypeDescription= "Live Birth"},
                            new EncounterStartType {_encounterStartType = 5 ,_encounterStartTypeDescription= "Still Birth"},
                            new EncounterStartType {_encounterStartType = 6 ,_encounterStartTypeDescription= "Dead On Arrival"},
                            new EncounterStartType {_encounterStartType = 7, _encounterStartTypeDescription = "Continuing Encounter"},
                        };

                        result.Data.Add("encounterStartTypes", encounterStartTypes);
                    }

                    if (filter.screenType.Contains(5))
                    {
                        List<EncounterEndType> encounterEndTypes = new List<EncounterEndType>
                        {
                            new EncounterEndType { _encounterEndType = 1 ,_encounterEndTypeDescription = "Discharged with approval"},
                            new EncounterEndType {_encounterEndType = 2 ,_encounterEndTypeDescription= "Discharged against advice"},
                            new EncounterEndType {_encounterEndType = 3 ,_encounterEndTypeDescription= "Discharged absent without leave"},
                            new EncounterEndType {_encounterEndType = 4 ,_encounterEndTypeDescription= "Discharge transfer to acute care"},
                            new EncounterEndType {_encounterEndType = 5 ,_encounterEndTypeDescription= "Deceased"},
                            new EncounterEndType {_encounterEndType = 6 ,_encounterEndTypeDescription= "Not discharged"},
                            new EncounterEndType {_encounterEndType = 7, _encounterEndTypeDescription = "Discharge transfer to non-acute care(Transfer to long term care)"},
                                new EncounterEndType {_encounterEndType = 8 ,_encounterEndTypeDescription= "Administrative discharge"},
                        };

                        result.Data.Add("encounterEndTypes", encounterEndTypes);
                    }


                    if (filter.screenType.Contains(6))
                    {

                        var denialCodes = await _dashboardRepo.GetDenialCodes(Connection);
                        result.Data.Add("denialCodes", denialCodes);
                    }


                    if (filter.screenType.Contains(7))
                    {
                        var observationCodes = await _dashboardRepo.GetObservationCodes(Connection);
                        result.Data.Add("observationCodes", observationCodes);
                    }
                    if (filter.screenType.Contains(8))
                    {
                        var icdCodes = await _dashboardRepo.GetICDCodes(Connection);
                        result.Data.Add("iCDCodes", icdCodes);
                    }
                    if (filter.screenType.Contains(12))
                    {
                        var Payers = await _dashboardRepo.GetPayers(Connection);
                        result.Data.Add("payers", Payers);
                    }
                    if (filter.screenType.Contains(13))
                    {
                        var Receivers = await _dashboardRepo.GetReceivers(Connection);
                        result.Data.Add("receivers", Receivers);
                    }

                    if (filter.screenType.Contains(14))
                    {
                        var cptCodes = await _dashboardRepo.GetCptCodes(Connection);                     

                        result.Data.Add("cptCodes", cptCodes);
                    }

                    if (filter.screenType.Contains(15))
                    {
                        var Clinicians = await _dashboardRepo.GetClinicians(Connection);
                        result.Data.Add("clinicians", Clinicians);

                    }

                    if (filter.screenType.Contains(18))
                    {
                        var drugCodes = await _dashboardRepo.GetDrugCodes(Connection);
                        result.Data.Add("drugCodes", drugCodes);

                    }
                }

                

            }
            catch (Exception ex)
            {
                result.Message = "API Error " + ex.Message;
                this.HttpContext.Response.StatusCode = 400;
            }

            //var data = JsonConvert.SerializeObject(result);
            //var content = Encoding.UTF8.GetBytes(data);

            //var json = System.Text.Json.JsonSerializer.Serialize(result);

            //HttpContext.Response.ContentType = "application/json";
            //HttpContext.Response.ContentLength = json.Length;
            return result;
        }

        [HttpPost("GetActivityCode")]
        public async Task<ActionResult<ResultJson>> GetActivityCode(searchActivity searchActivity)
        {

            Connection = GetConnection();

            ResultJson result = new ResultJson();
            try
            {

                var activityCodes = await _dashboardRepo.GetActivity(Connection, searchActivity);


                if (activityCodes == null)
                {
                    result.Message = "No Data Found ";
                    this.HttpContext.Response.StatusCode = 500;
                }
                else
                {
                    result.Message = "Success";
                    result.Data.Add("activityCodes", activityCodes);

                }
            }
            catch (Exception ex)
            {
                result.Message = "API Error " + ex.Message;
                this.HttpContext.Response.StatusCode = 400;
            }


            return result;
        }

       

    }
}
