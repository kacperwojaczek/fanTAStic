﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack;
using WebApplication2;
using WebApplication2.ServiceModel;
using System.Net;
using Newtonsoft.Json;

namespace WebApplication2.ServiceInterface
{
    public class MyServices : Service
    {
        public object Any(Hello request)
        {
            return new HelloResponse { Result = "Hello, {0}!".Fmt(request.Name) };
        }
    }

    public class RegistrationService : Service
    {
        public object Post(RegistrationRequest request)
        {
            //sprawdzenie czy user jest w bazie danych
            //wstawienei do bazy danych
            //odpowiedz do frontu
            User user1 = new User();
            user1.Firstname = request.Firstname.ToString();
            user1.Lastname = request.Lastname.ToString();
            user1.Login = request.Login.ToString();
            user1.Password = request.Password.ToString();
            user1.Email = request.Email.ToString();
            RegistrationResponse Response = new RegistrationResponse();
            int result = Response.Session(request);
            base.Response.StatusCode = result;
            return Response;
        }
    }

    public class UsersService: Service
    {
        public object Get(UserRequest request)
        {
            User user;
            UserResponse Response = new UserResponse();
            if (request.Login.IsNullOrEmpty())
            {
                base.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Response;
            }
            user = Response.Respond(request);
            string response = JsonConvert.SerializeObject(user,Formatting.Indented);
            return response;
        }

        //modyfikacja danych uzytkownika

        public object Patch(UserRequest request)
        {
            return new UserResponse { Result = "Modifiying user {0} ".Fmt(request.Login) };
        }
    }
    public class loginService : Service
    {
        public object Post(loginRequest request)
        {
            loginResponse Response = new loginResponse();
            if (request.Login.IsNullOrEmpty())
            {
                base.Response.StatusCode = 400;
                return base.Response;
            }
            
            base.Response.StatusCode = Response.Session(request);
            return Response;
        }
    }
    public class UserPostsService : Service
    {
        public object Get(UserPostsRequest request)
        {
            UserPostsResponse Response = new UserPostsResponse();
            var posts = Response.Respond(request);
            string response = JsonConvert.SerializeObject(posts, Formatting.Indented);
            return response;
        }
    }
    public class UserPostService : Service
    {
        public object Get(UserPostRequest request)
        {
            UserPostResponse Response = new UserPostResponse();
            var post = Response.Respond(request);
            if(post == null)
            {
                base.Response.StatusCode = (int)HttpStatusCode.NoContent;
                return Response;
            }
            string response = JsonConvert.SerializeObject(post, Formatting.Indented);
            return response;
        }

        public object Post(UserPostPOSTRequest request)
        {

            return Response;
        }
    }
}