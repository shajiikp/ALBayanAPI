using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlBayanWebAPI.Models
{
    public class ResultJson
    {
        public string Message { get; set; }

        private Dictionary<string, object> _Data = new Dictionary<string, object>();
        public Dictionary<string, object> Data
        {
            get { return _Data; }
            set { _Data = value; }
        }

    }
}
