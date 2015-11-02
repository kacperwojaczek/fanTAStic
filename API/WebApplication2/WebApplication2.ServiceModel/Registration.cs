using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack;

namespace WebApplication2.ServiceModel
{
    [Route("/register/{Login}", "POST")]
    public class RegistrationRequest : IReturn<RegistrationResponse>
    {
        public string Login { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }


    }

    public class RegistrationResponse
    {
        public ResponseStatus ResponseStatus { get; set; }

        public string Result { get; set; }
    }

}