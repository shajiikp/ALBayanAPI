using AlBayanWebAPI.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlBayanWebAPI.Interface
{
    public interface IUserInterface
    {

        Task<SecureData> ValidateLogin(string Email, string Password);
         Task<UserInfo> GetUserDataFromId(int UserId);
         Task<UserInfo> RegisterUser(Registration registration);
    }
}
