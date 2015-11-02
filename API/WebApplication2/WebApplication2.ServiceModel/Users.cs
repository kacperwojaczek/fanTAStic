using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack;

namespace WebApplication2.ServiceModel
{
    [Route("/users", "GET")]
    [Route("/users/{Login}", "GET")]
    [Route("/users/{Login}", "PATCH")]
    public class UserRequest : IReturn<UserResponse>
    {
        public string Login { get; set; }

        public string Firstname { get; set; }
    }

    public class UserResponse
    {
        public ResponseStatus ResponseStatus { get; set; }

        public string Result { get; set; }
    }
}

    public class User
    {
        public string Login { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
