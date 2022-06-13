using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recap.Core.Entities.Concrete
{
    public class ErrorLoggerModel : IDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }

        public string ErrorStackTrace { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorDetail { get; set; }
        public int Level { get; set; }


        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
