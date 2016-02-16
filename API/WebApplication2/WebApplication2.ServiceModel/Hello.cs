using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack;

namespace WebApplication2.ServiceModel
{
    [Route("/hello")]
    [Route("/hello/{Name}")]
    public class Hello : IReturn<HelloResponse>
    {
        public string Name { get; set; }
    }

    public class HelloResponse
    {
        public ResponseStatus ResponseStatus { get; set; }

        public string Result { get; set; }
    }
}