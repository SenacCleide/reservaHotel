using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Application.Dto
{
    public class UserToken
    {
        public bool Status { get; set; }
        public string Created { get; set; }
        public string Token { get; set; }
        public string Expiration { get; set; }
        public string Message { get; set; }
        public string Tenant { get; set; }

        public class LoginFailureDto
        {
            public bool Status { get; set; }
            public string Message { get; set; }
        }
    }
}
