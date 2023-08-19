using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlBayanWebAPI.Interface
{
    public interface IMailService
    {
        Task SendEmailAsync(string Email,string Provider);
    }
}
