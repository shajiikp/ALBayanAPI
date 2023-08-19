using AlBayanWebAPI.Interface;
using AlBayanWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ServiceReference1;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AlBayanWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class userController : BaseController
    {

        private readonly JWTSettings _jwtsettings;
        private readonly IUserInterface _userRepo;
        private readonly IMailService _mailService;
       

        public userController( IUserInterface userRepo, IOptions<JWTSettings> jwtsettings , IMailService mailService)
        {          
            _userRepo = userRepo;
            _jwtsettings = jwtsettings.Value;
            _mailService = mailService;
            
        }
        // GET api/<UserController>/5
        [HttpPost("getUserDataFromId")]
        public async Task<ActionResult<ResultJson>> getUserDataFromId([FromBody] int userId)
        {
            ResultJson result = new ResultJson();
            int UserId = userId;
            UserInfo usr = new UserInfo();
            try
            {
                usr = await _userRepo.GetUserDataFromId(UserId);
                result.Message = "Success";
                result.Data.Add("userInfo", usr);

            }
            catch (Exception ex)
            {
                result.Message = "API Error " + ex.Message;
            }
            return result;
        }

        // POST api/<UserController>
        [HttpPost("login")]
        public async Task<ActionResult<ResultJson>> Login([FromBody] UserLogin userdata)
        {
            ResultJson result = new ResultJson();
            SecureData obj = new SecureData();
            User usr = new User();
            try
            {
                obj = await _userRepo.ValidateLogin(userdata.email, userdata.password);


                UserWithToken userWithToken = null;

                if (obj.UserId != 0)
                {
                    usr._userId = obj.UserId;
                    usr._userName =obj.Code+'-'+ obj.Name;
                    userWithToken = new UserWithToken(usr);
                    userWithToken.AccessToken = GenerateAccessToken(obj);
                    Response.Cookies.Append("X-Access-Token", userWithToken.AccessToken, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });
                    //return userWithToken;
                    result.Message = "Success";
                    result.Data.Add("userInfo", userWithToken);
                }

                if (userWithToken == null)
                {
                    result.Message = "No Data Found ";
                    this.HttpContext.Response.StatusCode = 500;
                }

                //sign your token here here..

            }
            catch (Exception ex)
            {
                result.Message = "API Error " + ex.Message;
            }
            return result;

        }

        [HttpPost("getUserDataFromToken")]
        public async Task<ActionResult<ResultJson>> GetUserDataFromToken()
        {
            //string errmsg = "";
            //byte[] tcpbuffer = null;
            //int status = 0;
            //string FileNameToDownload = string.Empty;
            //ValidateTransactionsSoapClient.EndpointConfiguration endpointConfiguration = new ValidateTransactionsSoapClient.EndpointConfiguration();
            //ValidateTransactionsSoapClient validateTransactionsSoapClient = new ValidateTransactionsSoapClient(endpointConfiguration);
            //status = validateTransactionsSoapClient.DownloadTransactionFile("fampharmacy", "Curo9246@", "b812c236-f273-444f-9100-52e93d25bb7f", out FileNameToDownload, out tcpbuffer, out errmsg);



            ResultJson result = new ResultJson();          
            int UserId = GetUserId();
            UserInfo usr = new UserInfo();
            if (UserId >0)
            {
                try
                {
                    usr = await _userRepo.GetUserDataFromId(UserId);
                    result.Message = "Success";
                    result.Data.Add("userInfo", usr);

                }
                catch (Exception ex)
                {
                    result.Message = "API Error " + ex.Message;
                }
            }
            else
            {
                result.Message = "Failure";
                result.Data.Add("userInfo", usr);
            }
            
            return result;

        }

        private string GenerateAccessToken(SecureData obj)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtsettings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {                    
                    //new Claim(ClaimTypes.Uri, obj.Connection),
                    new Claim("Conn", obj.Connection),
                    new Claim("Id", obj.UserId.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(24),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        [HttpPost("register")]
        public async Task<ActionResult<ResultJson>> RegisterUser([FromBody] Registration registration)
        {
            ResultJson result = new ResultJson();
           // int UserId = GetUserId();
            UserInfo usr = new UserInfo();
            try
            {
                usr = await _userRepo.RegisterUser(registration);
                result.Message = "Success";
                result.Data.Add("userInfo", usr);
                //await _mailService.SendEmailAsync(registration.Email,registration.UserName);
                await _mailService.SendEmailAsync("Shajiikp@gmail.com", "Shaji");

            }
            catch (Exception ex)
            {
                result.Message = "API Error " + ex.Message;
            }
            return result;

        }
    }
}
