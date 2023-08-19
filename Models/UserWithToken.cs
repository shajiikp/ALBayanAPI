using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlBayanWebAPI.Models
{
    public class UserWithToken:User
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public UserWithToken(User user)
        {
            this._userId = user._userId;
            this._email = user._email;
            this._fullname = user._fullname;
            this._userName = user._userName;
            this._superUser = user._superUser;
            this._role = user._role;
        }
    }
}
