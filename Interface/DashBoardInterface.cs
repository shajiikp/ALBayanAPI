using AlBayanWebAPI.Context;
using AlBayanWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace AlBayanWebAPI.Interface
{
    public class DashBoardInterface : IDashBoardInterface
    {
        private readonly SqlConnectionConfiguration _configuration;
        public DashBoardInterface(SqlConnectionConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async  Task<IEnumerable<DashBoardData>> GetDashBoardData(string connection, filters filter)
        {

            List<DashBoardData> obj = new List<DashBoardData>();           
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection con = new SqlConnection(connection))
                {
                    const string query = "sp_GetDashBoardData_Complete_API";
                    SqlCommand cmd = new SqlCommand(query, con)
                    {
                        CommandType = CommandType.StoredProcedure,
                    };
                    cmd.Parameters.AddWithValue("@Skip", filter.skip > 0 ? filter.skip : 0);
                    cmd.Parameters.AddWithValue("@Limit", filter.limit > 0 ? filter.limit : 0);
                    cmd.Parameters.AddWithValue("@SearchKey", filter.searchingText);
                    con.Open();
                  
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    try
                    {
                        await Task.Run(() => da.Fill(ds));                       
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {

                                obj.Add(new DashBoardData
                                {
                                    _submittedDate = long.Parse( dr["SUBMITTED_DATE"].ToString()),
                                    //fileName =  dr["FILE_NAME"].ToString().Trim(),
                                    //records = Convert.ToInt32(dr["RECORDS"].ToString()),
                                    _receiver = dr["RECEIVER"].ToString(),
                                    _receiverCode = dr["RECEIVER_CODE"].ToString(),
                                    _payer = dr["PAYER"].ToString(),
                                    _payerCode = dr["PAYER_CODE"].ToString(),
                                    //claimId = dr["CLAIMID"].ToString(),
                                    _grossAmount= float.Parse(dr["GROSS"].ToString(), CultureInfo.InvariantCulture.NumberFormat),
                                    _patientShare = float.Parse(dr["PATIENTSHARE"].ToString(), CultureInfo.InvariantCulture.NumberFormat),
                                    _netAmount = float.Parse(dr["NET"].ToString(), CultureInfo.InvariantCulture.NumberFormat),
                                    _approvedAmount = float.Parse(dr["RAAPPROVED"].ToString(), CultureInfo.InvariantCulture.NumberFormat),
                                    _approvedDate= Convert.ToInt64(dr["RA_DATE"].ToString()),
                                    _submissionType = dr["SubmissionType"].ToString().Trim(),
                                    _claimStatus = Convert.ToInt32(dr["STATUS"].ToString()),
                                });
                            }
                        }


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
                return obj;
            }

            return obj;
        }

        public async Task<IEnumerable<DashBoardData>> GetDashBoardDataDetails(string connection, filters filter)
        {
            List<DashBoardData> obj = new List<DashBoardData>();
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection con = new SqlConnection(connection))
                {
                    const string query = "sp_GetDashBoardData_Complete_API_DETAILS";
                    SqlCommand cmd = new SqlCommand(query, con)
                    {
                        CommandType = CommandType.StoredProcedure,
                    };
                    cmd.Parameters.AddWithValue("@Skip", filter.skip > 0 ? filter.skip : 0);
                    cmd.Parameters.AddWithValue("@Limit", filter.limit > 0 ? filter.limit : 0);
                    cmd.Parameters.AddWithValue("@SearchKey", filter.searchingText);
                    con.Open();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    try
                    {
                        await Task.Run(() => da.Fill(ds));
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {

                                obj.Add(new DashBoardData
                                {
                                    claimId = dr["CSID"].ToString(),
                                    _submittedDate = long.Parse(dr["SUBMITTED_DATE"].ToString()),
                                    //fileName =  dr["FILE_NAME"].ToString().Trim(),
                                    //records = Convert.ToInt32(dr["RECORDS"].ToString()),
                                    _receiver = dr["RECEIVER"].ToString(),
                                    _receiverCode = dr["RECEIVER_CODE"].ToString(),
                                    _payer = dr["PAYER"].ToString(),
                                    _payerCode = dr["PAYER_CODE"].ToString(),                                    
                                    _grossAmount = float.Parse(dr["GROSS"].ToString(), CultureInfo.InvariantCulture.NumberFormat),
                                    _patientShare = float.Parse(dr["PATIENTSHARE"].ToString(), CultureInfo.InvariantCulture.NumberFormat),
                                    _netAmount = float.Parse(dr["NET"].ToString(), CultureInfo.InvariantCulture.NumberFormat),
                                    _approvedAmount = float.Parse(dr["RAAPPROVED"].ToString(), CultureInfo.InvariantCulture.NumberFormat),
                                    _approvedDate = Convert.ToInt64(dr["RA_SUBMITTED"].ToString()),
                                    _settlementDate = Convert.ToInt64(dr["CLAIM_SETTLE_DATE"].ToString()),
                                    _submissionType = dr["SubmissionType"].ToString().Trim(),
                                    _claimStatus = Convert.ToInt32(dr["STATUS"].ToString()),
                                    activityDetails = (from drActivity in ds.Tables[1].AsEnumerable().Where(x => x.Field<Int64>("PKCSID") == Convert.ToInt64(dr["PKCSID"].ToString()))
                                                       select new ActivityDetails()
                                                       {
                                                           _activityCode = drActivity["COD"].ToString(),
                                                           _activityDescription = drActivity["Description"].ToString(),
                                                           _activityNet =double.Parse( drActivity["CNET"].ToString()),
                                                           _activityApproved= double.Parse(drActivity["APPROVED"].ToString()),
                                                           _denialCode= drActivity["DENIALCODE"].ToString(),
                                                           _clinicianCode= drActivity["ACLN"].ToString(),
                                                           _clinicianName= drActivity["CLINICIAN"].ToString(),

                                                       }).ToList(),
                                });



                           
                            }
                        }


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
                return obj;
            }

            return obj;
        }
        public async Task<IEnumerable<ClaimData>> GetClaimData(string connection, filters filter)
        {
            List<ClaimData> obj = new List<ClaimData>();
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection con = new SqlConnection(connection))
                {
                    const string query = "sp_GetDashBoardData_Complete_API_DETAILS_CLAIMS_NEW";
                    SqlCommand cmd = new SqlCommand(query, con)
                    {
                        CommandType = CommandType.StoredProcedure,
                        CommandTimeout = 0
                    };
                    cmd.Parameters.AddWithValue("@Skip", filter.skip > 0 ? filter.skip : 0);
                    cmd.Parameters.AddWithValue("@Limit", filter.limit > 0 ? filter.limit : 0);
                    cmd.Parameters.AddWithValue("@SearchKey", filter.searchingText);
                    con.Open();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    try
                    {
                        await Task.Run(() => da.Fill(ds));
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {

                                obj.Add(new ClaimData
                                {
                                    _claimId = dr["CSID"].ToString(),                                   
                                    _receiver = dr["RECEIVER"].ToString(),
                                    _receiverCode = dr["RECEIVER_CODE"].ToString(),
                                    _payer = dr["PAYER"].ToString(),
                                    _payerCode = dr["PAYER_CODE"].ToString(),
                                   
                                    ClaimSubmissions = (from drSubmissions in ds.Tables[1].AsEnumerable().Where(x => x.Field<string>("CSID") ==dr["CSID"].ToString())
                                                  select new ClaimSubmissions()
                                                  {
                                                      _submittedDate = long.Parse(drSubmissions["SUBMITTED_DATE"].ToString()),
                                                      _grossAmount = float.Parse(drSubmissions["GROSS"].ToString(), CultureInfo.InvariantCulture.NumberFormat),
                                                      _patientShare = float.Parse(drSubmissions["PATIENTSHARE"].ToString(), CultureInfo.InvariantCulture.NumberFormat),
                                                      _netAmount = float.Parse(drSubmissions["NET"].ToString(), CultureInfo.InvariantCulture.NumberFormat),                                                     
                                                      _submissionType = Convert.ToInt32(drSubmissions["SubmissionType"].ToString().Trim()),                                                      
                                                      _fileId = Convert.ToInt64(drSubmissions["PKHD4"].ToString()),
                                                      _linkId = Convert.ToInt64(drSubmissions["PKCSID"].ToString()),
                                                      _fileName= drSubmissions["FILENAME"].ToString()
                                                  }).ToList(),
                                    ClaimRemittance=
                                    (from drRemittance in ds.Tables[2].AsEnumerable().Where(x => x.Field<string>("CSID") == dr["CSID"].ToString())
                                     select new ClaimRemittance()
                                     {
                                         _submittedDate = long.Parse(drRemittance["RA_SUBMITTED"].ToString()),                                       
                                         _approvedAmount = float.Parse(drRemittance["RAAPPROVED"].ToString(), CultureInfo.InvariantCulture.NumberFormat),
                                         _approvedDate = Convert.ToInt64(drRemittance["CLAIM_SETTLE_DATE"].ToString()),
                                         _settlementDate = Convert.ToInt64(drRemittance["CLAIM_SETTLE_DATE"].ToString()),
                                         _submissionType = Convert.ToInt32(drRemittance["SubmissionType"].ToString().Trim()),
                                         _claimStatus = Convert.ToInt32(drRemittance["STATUS"].ToString()),
                                         _paymentReference = drRemittance["CSPR"].ToString(),
                                         _fileId = Convert.ToInt64(drRemittance["PKHD5"].ToString()),
                                         _linkId = Convert.ToInt64(drRemittance["PKRAID"].ToString()),
                                         _fileName = drRemittance["FILENAME"].ToString(),
                                     }).ToList(),

                                });

                            }
                        }


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
                return obj;
            }

            return obj;
        }

        public async Task<IEnumerable<ActivityDetails>> GetActivityData(string connection, filters filter)
        {
            List<ActivityDetails> obj = new List<ActivityDetails>();
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection con = new SqlConnection(connection))
                {
                    const string query = "sp_GetDashBoardData_Complete_API_DETAILS_ACTIVITY";
                    SqlCommand cmd = new SqlCommand(query, con)
                    {
                        CommandType = CommandType.StoredProcedure,
                    };
                    cmd.Parameters.AddWithValue("@Skip", filter.skip > 0 ? filter.skip : 0);
                    cmd.Parameters.AddWithValue("@Limit", filter.limit > 0 ? filter.limit : 0);
                    cmd.Parameters.AddWithValue("@SearchKey", filter.searchingText);
                    con.Open();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    try
                    {
                        await Task.Run(() => da.Fill(ds));
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow drActivity in ds.Tables[0].Rows)
                            {

                                obj.Add(new ActivityDetails
                                {
                                    _activityType = Convert.ToInt32(drActivity["ATYP"].ToString()),
                                    _activityCode = drActivity["COD"].ToString(),
                                    _activityDescription = drActivity["Description"].ToString(),
                                    _activityNet = double.Parse(drActivity["CNET"].ToString()),
                                    _activityQuantity = double.Parse(drActivity["QTY"].ToString()),
                                    _activityApproved = double.Parse(drActivity["APPROVED"].ToString()),
                                    _denialCode = drActivity["DENIALCODE"].ToString(),
                                    _clinicianCode = drActivity["ACLN"].ToString(),
                                    _clinicianName = drActivity["CLINICIAN"].ToString(),
                                    _fileId = Convert.ToInt64(drActivity["PKHD4"].ToString()),
                                    _linkId = Convert.ToInt64(drActivity["PKCSID"].ToString()),
                                });


                                

                            }
                        }


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
                return obj;
            }

            return obj;
        }

        public async  Task<IEnumerable<DenialCodes>> GetDenialCodes(string Connection)
        {
            List<DenialCodes> obj = new List<DenialCodes>();
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    const string query = "sp_GetDenialCodes";
                    SqlCommand cmd = new SqlCommand(query, con)
                    {
                        CommandType = CommandType.StoredProcedure,
                    };
                   
                    con.Open();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    try
                    {
                        await Task.Run(() => da.Fill(ds));
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow drActivity in ds.Tables[0].Rows)
                            {

                                obj.Add(new DenialCodes
                                {
                                    _denialCode = drActivity["code"].ToString(),
                                    _denialCodeDescription = drActivity["Description"].ToString(),
                                   
                                });




                            }
                        }


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
                return obj;
            }

            return obj;
        }

        public async Task<IEnumerable<ClaimsSummaryData>> GetSummaryData(string connection, filters filter)
        {
            List<ClaimsSummaryData> obj = new List<ClaimsSummaryData>();
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection con = new SqlConnection(connection))
                {
                    const string query = "sp_GetDashBoardData_Complete_API_DETAILS_SUMMARY";
                    SqlCommand cmd = new SqlCommand(query, con)
                    {
                        CommandType = CommandType.StoredProcedure,
                    };
                    cmd.Parameters.AddWithValue("@Skip", filter.skip > 0 ? filter.skip : 0);
                    cmd.Parameters.AddWithValue("@Limit", filter.limit > 0 ? filter.limit : 0);
                    cmd.Parameters.AddWithValue("@SearchKey", filter.searchingText);
                    con.Open();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    try
                    {
                        await Task.Run(() => da.Fill(ds));
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                               

                            obj.Add(new ClaimsSummaryData
                                {
                                     _fileName = dr["FN"].ToString(),
                                    _submittedDate = long.Parse(dr["SUBMITTED_DATE"].ToString()),                                   
                                    //_receiver = dr["RECEIVER"].ToString(),
                                     _claimsCount = Convert.ToInt32(dr["RECORDS"].ToString().Trim()),
                                    _patientShare = float.Parse(dr["PATIENT_SHARE"].ToString(), CultureInfo.InvariantCulture.NumberFormat),
                                    _netAmount = float.Parse(dr["NET_AMOUNT"].ToString(), CultureInfo.InvariantCulture.NumberFormat),
                                    _approvedAmount = float.Parse(dr["APPROVED_AMT"].ToString(), CultureInfo.InvariantCulture.NumberFormat),
                                    _fileId = Convert.ToInt64(dr["LINKID"].ToString()),

                                });

                            }
                        }


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
                return obj;
            }

            return obj;
        }

        public async Task<IEnumerable<ObservationCodes>> GetObservationCodes(string Connection)
        {
            List<ObservationCodes> obj = new List<ObservationCodes>();
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    const string query = "sp_GetObservation";
                    SqlCommand cmd = new SqlCommand(query, con)
                    {
                        CommandType = CommandType.StoredProcedure,
                    };

                    con.Open();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    try
                    {
                        await Task.Run(() => da.Fill(ds));
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow drActivity in ds.Tables[0].Rows)
                            {

                                obj.Add(new ObservationCodes
                                {
                                    _observationType = drActivity["TYPE"].ToString(),
                                    _observationCode = drActivity["CODE"].ToString(),
                                    _observationValueType = drActivity["VALUETYPE"].ToString(),
                                    _observationValueDataType = drActivity["VALUEDATATYPE"].ToString(),
                                    _observationRemarks = drActivity["REMARKS"].ToString(),

                                });




                            }
                        }


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
                return obj;
            }

            return obj;
        }

        public async Task<IEnumerable<ICDCodes>> GetICDCodes(string Connection)
        {
            List<ICDCodes> obj = new List<ICDCodes>();
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    const string query = "sp_GetICDCodes";
                    SqlCommand cmd = new SqlCommand(query, con)
                    {
                        CommandType = CommandType.StoredProcedure,
                    };

                    con.Open();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    try
                    {
                        await Task.Run(() => da.Fill(ds));
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow drICD in ds.Tables[0].Rows)
                            {

                                obj.Add(new ICDCodes
                                {
                                    _icdId = Convert.ToInt64(drICD["ID"].ToString()),
                                    _icdCode = drICD["CODE"].ToString(),
                                    _icdDescription = drICD["ShortDesc"].ToString(),
                                   
                                });




                            }
                        }


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
                return obj;
            }

            return obj;
        }

        public async  Task<IEnumerable<Payers>> GetPayers(string Connection)
        {
            List<Payers> obj = new List<Payers>();
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    const string query = "sp_GetPayers";
                    SqlCommand cmd = new SqlCommand(query, con)
                    {
                        CommandType = CommandType.StoredProcedure,
                    };

                    con.Open();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    try
                    {
                        await Task.Run(() => da.Fill(ds));
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow drICD in ds.Tables[0].Rows)
                            {

                                obj.Add(new Payers
                                {
                                   
                                    _code = drICD["CODE"].ToString(),
                                    _name = drICD["ShortDesc"].ToString(),

                                });




                            }
                        }


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
                return obj;
            }

            return obj;
        }

        public async Task<IEnumerable<Receivers>> GetReceivers(string Connection)
        {
            List<Receivers> obj = new List<Receivers>();
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    const string query = "sp_Receivers";
                    SqlCommand cmd = new SqlCommand(query, con)
                    {
                        CommandType = CommandType.StoredProcedure,
                    };

                    con.Open();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    try
                    {
                        await Task.Run(() => da.Fill(ds));
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow drICD in ds.Tables[0].Rows)
                            {

                                obj.Add(new Receivers
                                {

                                    _code = drICD["CODE"].ToString(),
                                    _name = drICD["ShortDesc"].ToString(),

                                });




                            }
                        }


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
                return obj;
            }

            return obj;
        }

        public async Task<IEnumerable<Clinicians>> GetClinicians(string connection)
        {
            List<Clinicians> obj = new List<Clinicians>();
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection con = new SqlConnection(connection))
                {
                    const string query = "sp_Clinicians";
                    SqlCommand cmd = new SqlCommand(query, con)
                    {
                        CommandType = CommandType.StoredProcedure,
                    };

                    con.Open();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    try
                    {
                        await Task.Run(() => da.Fill(ds));
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow drICD in ds.Tables[0].Rows)
                            {

                                obj.Add(new Clinicians
                                {

                                    _code = drICD["ClinicianID"].ToString(),
                                    _name = drICD["ProfessionalName"].ToString(),

                                });




                            }
                        }


                        con.Close();
                        cmd.Dispose();
                    }
                    catch (Exception ex)
                    {
                        con.Close();
                        cmd.Dispose();
                        throw ex;
                    }
                   
                }

            }
            catch (Exception ex)
            {
                return obj;
            }

            return obj;
        }

        public async Task<IEnumerable<CPTCodes>> GetCptCodes(string connection)
        {
            List<CPTCodes> obj = new List<CPTCodes>();
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection con = new SqlConnection(connection))
                {
                    const string query = "sp_GetCPTCodes";
                    SqlCommand cmd = new SqlCommand(query, con)
                    {
                        CommandType = CommandType.StoredProcedure,
                    };

                    con.Open();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    try
                    {
                        await Task.Run(() => da.Fill(ds));
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow drICD in ds.Tables[0].Rows)
                            {

                                obj.Add(new CPTCodes
                                {

                                    _cptCode = drICD["CPT_CODE"].ToString(),
                                    _description = drICD["SHORT_DESCRIPTION"].ToString(),

                                });




                            }
                        }


                        con.Close();
                        cmd.Dispose();
                    }
                    catch (Exception ex)
                    {
                        con.Close();
                        cmd.Dispose();
                        throw ex;
                    }

                }

            }
            catch (Exception ex)
            {
                return obj;
            }

            return obj;
        }

        public async Task<IEnumerable<DrugCodes>> GetDrugCodes(string connection)
        {
            List<DrugCodes> obj = new List<DrugCodes>();
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection con = new SqlConnection(connection))
                {
                    const string query = "sp_GetDrugCodes";
                    SqlCommand cmd = new SqlCommand(query, con)
                    {
                        CommandType = CommandType.StoredProcedure,
                    };

                    con.Open();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    try
                    {
                        await Task.Run(() => da.Fill(ds));
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow drICD in ds.Tables[0].Rows)
                            {

                                obj.Add(new DrugCodes
                                {

                                    _drugCode = drICD["DDC_CODE"].ToString(),
                                    _description = drICD["TRADE_NAME"].ToString(),

                                });




                            }
                        }


                        con.Close();
                        cmd.Dispose();
                    }
                    catch (Exception ex)
                    {
                        con.Close();
                        cmd.Dispose();
                        throw ex;
                    }

                }

            }
            catch (Exception ex)
            {
                return obj;
            }

            return obj;
        }

        public async Task<IEnumerable<activityCodes>> GetActivity(string connection, searchActivity searchActivity)
        {
            List<activityCodes> obj = new List<activityCodes>();
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection con = new SqlConnection(connection))
                {
                    const string query = "sp_GetActivityCodes";
                    SqlCommand cmd = new SqlCommand(query, con)
                    {
                        CommandType = CommandType.StoredProcedure,
                    };
                    cmd.Parameters.AddWithValue("@Skip", searchActivity.skip > 0 ? searchActivity.skip : 0);
                    cmd.Parameters.AddWithValue("@Limit", searchActivity.limit > 0 ? searchActivity.limit : 0);
                    cmd.Parameters.AddWithValue("@SearchKey", searchActivity.searchingText);
                    cmd.Parameters.AddWithValue("@Id", searchActivity.id);
                    con.Open();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    try
                    {
                        await Task.Run(() => da.Fill(ds));
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow drICD in ds.Tables[0].Rows)
                            {

                                obj.Add(new activityCodes
                                {

                                    _code = drICD["CODE"].ToString(),
                                    _name = drICD["NAME"].ToString(),

                                });




                            }
                        }


                        con.Close();
                        cmd.Dispose();
                    }
                    catch (Exception ex)
                    {
                        con.Close();
                        cmd.Dispose();
                        throw ex;
                    }

                }

            }
            catch (Exception ex)
            {
                return obj;
            }

            return obj;
        }
    }
}
