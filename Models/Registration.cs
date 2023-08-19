using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlBayanWebAPI.Models
{
    public class Registration
    {
        /// <summary>
        /// 1: Provider  2: Payer
        /// </summary>
        public int _accountType { get; set; }
        /// <summary>
        /// DHA ,Riyati or HAAD Facility ID
        /// </summary>
        public string _facility { get; set; }       
        public string _userName { get; set; }
        public string _email { get; set; }
        public string _password { get; set; }
        public string _contactPerson { get; set; }
        public string _phone { get; set; }


    }
}
