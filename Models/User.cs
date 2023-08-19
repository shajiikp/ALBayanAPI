using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlBayanWebAPI.Models
{
    public class User
    {
        public int _userId { get; set; }
        public string _userName { get; set; }
        public string _email { get; set; }
        public string _password { get; set; }
        public bool _active { get; set; }
        public string _fullname { get; set; }
        public int? _role { get; set; }
        public bool? _superUser { get; set; }
    }
}
