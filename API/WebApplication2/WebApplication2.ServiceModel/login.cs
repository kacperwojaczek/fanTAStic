using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack;

namespace WebApplication2.ServiceModel
{
    [Route("/login", "GET")]
    [Route("/login/{Login}", "GET")]
    public class loginRequest : IReturn<loginResponse>
    {
        public string Login { get; set; }

        public string Password { get; set; }
    }
    public class loginResponse
    {
        public ResponseStatus ResponseStatus { get; set; }

        public string Result { get; set; }
    }
}
