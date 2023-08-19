using AlBayanWebAPI.Context;
using AlBayanWebAPI.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AlBayanWebAPI.Interface
{
    public class UserInterface : IUserInterface
    {

        private readonly SqlConnectionConfiguration _configuration;
        public UserInterface(SqlConnectionConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async  Task<SecureData> ValidateLogin(string Email, string Password)
        {

           
            SecureData obj = new SecureData();
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection con = new SqlConnection(_configuration.Value))
                {
                    const string query = "sp_ValidateLogin";
                    SqlCommand cmd = new SqlCommand(query, con)
                    {
                        CommandType = CommandType.StoredProcedure,
                    };
                    con.Open();
                    cmd.Parameters.AddWithValue("@UserName", Email);
                    cmd.Parameters.AddWithValue("@Password", Password);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    try
                    {
                        await Task.Run(() => da.Fill(ds));
                        con.Close();
                        cmd.Dispose();
                        if (ds.Tables[0].Rows.Count>0)
                        {
                            obj.UserId = Convert.ToInt32( ds.Tables[0].Rows[0]["ID"].ToString());
                            obj.Connection = Encrypt(ds.Tables[0].Rows[0]["Connection"].ToString());
                            obj.Code= ds.Tables[0].Rows[0]["FacilityID"].ToString();
                            obj.Name= ds.Tables[0].Rows[0]["FACILITY_NAME"].ToString();
                        }
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

        public static string Encrypt(string clearText)
        {
            try
            {
                string EncryptionKey = "JHJDHFJDSJ87767BB7TMCVNMCN8787JBCB";
                byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(clearBytes, 0, clearBytes.Length);
                            cs.Close();
                        }
                        clearText = Convert.ToBase64String(ms.ToArray());
                    }
                }
                return clearText;
            }
            catch (Exception e)
            {
                return e.InnerException.ToString().Trim();
            }
        }

        public async  Task<UserInfo> GetUserDataFromId(int UserId)
        {
            UserInfo obj = new UserInfo();
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection con = new SqlConnection(_configuration.Value))
                {
                    const string query = "sp_GetUserDataFromId";
                    SqlCommand cmd = new SqlCommand(query, con)
                    {
                        CommandType = CommandType.StoredProcedure,
                    };
                    con.Open();
                    cmd.Parameters.AddWithValue("@UserId", UserId);                    
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    try
                    {
                        await Task.Run(() => da.Fill(ds));
                        con.Close();
                        cmd.Dispose();
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            obj._userId = Convert.ToInt32(ds.Tables[0].Rows[0]["ID"].ToString());
                            obj._userName = ds.Tables[0].Rows[0]["FACILITY_NAME"].ToString();
                            obj._email = ds.Tables[0].Rows[0]["Login"].ToString();
                            obj._password = Encrypt(ds.Tables[0].Rows[0]["Password"].ToString());
                            obj._active =Convert.ToBoolean( ds.Tables[0].Rows[0]["IsActive"].ToString());
                            obj._role = 1;
                        }
                    }
                    catch (Exception ex)
                    {
                        con.Close();
                        cmd.Dispose();
                       
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

        public async Task<UserInfo> RegisterUser(Registration registration)
        {
            UserInfo obj = new UserInfo();
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection con = new SqlConnection(_configuration.Value))
                {
                    const string query = "sp_RegisterUser";
                    SqlCommand cmd = new SqlCommand(query, con)
                    {
                        CommandType = CommandType.StoredProcedure,
                    };
                    con.Open();
                    cmd.Parameters.AddWithValue("@AccountType", registration._accountType);
                    cmd.Parameters.AddWithValue("@Facility", registration._facility);
                    cmd.Parameters.AddWithValue("@UserName", registration._userName);
                    cmd.Parameters.AddWithValue("@Email", registration._email);
                    cmd.Parameters.AddWithValue("@Password", registration._password);
                    cmd.Parameters.AddWithValue("@ContactPerson", registration._contactPerson);
                    cmd.Parameters.AddWithValue("@Phone", registration._phone);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    try
                    {
                        await Task.Run(() => da.Fill(ds));
                        con.Close();
                        cmd.Dispose();
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            obj._userId = Convert.ToInt32(ds.Tables[0].Rows[0]["ID"].ToString());
                            obj._userName = Encrypt(ds.Tables[0].Rows[0]["FACILITY_NAME"].ToString());
                            obj._email = ds.Tables[0].Rows[0]["Login"].ToString();
                            obj._password = ds.Tables[0].Rows[0]["Password"].ToString();
                            obj._active = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsActive"].ToString());
                            obj._role = 1;
                        }
                    }
                    catch (Exception ex)
                    {
                        con.Close();
                        cmd.Dispose();

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
    }
}
