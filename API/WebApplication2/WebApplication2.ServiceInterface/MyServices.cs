using System;
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

    public class UsersService: Service
    {
        public object Get(UserRequest request)
        {
            User user;
            UserResponse Response = new UserResponse();

                if (request.Login.IsNullOrEmpty())
                {
                    throw new HttpError(HttpStatusCode.BadRequest, "Bad Request")
                    {
                        Response = new ErrorResponse
                        {
                            ResponseStatus = new ResponseStatus
                            {
                                Errors = new List<ResponseError> {
                                new ResponseError {
                                    ErrorCode = "LoginIsNull",
                                    FieldName = "Login",
                                    Message = "'Login' should not be empty."
                                }
                            }
                            }
                        }
                    };
                }

                user = Response.Get(request);

                if (user == null)
                {
                    throw new HttpError(HttpStatusCode.NotFound, "Not Found")
                    {
                        Response = new ErrorResponse
                        {
                            ResponseStatus = new ResponseStatus
                            {
                                Errors = new List<ResponseError> {
                                new ResponseError {
                                    ErrorCode = "NotFound",
                                    Message = "User not found"
                                }
                            }
                            }
                        }
                    };
                }

            base.Response.StatusCode = (int)HttpStatusCode.OK;
            string response = JsonConvert.SerializeObject(user,Formatting.Indented);
            return response;
        }

        public object Patch(UserRequest request)
        {

            User user;
            UserResponse Response = new UserResponse();
            if (request.Login.IsNullOrEmpty())
            {
                throw new HttpError(HttpStatusCode.BadRequest, "Bad Request")
                {
                    Response = new ErrorResponse
                    {
                        ResponseStatus = new ResponseStatus
                        {
                            Errors = new List<ResponseError> {
                                new ResponseError {
                                    ErrorCode = "LoginIsNull",
                                    FieldName = "Login",
                                    Message = "'Login' should not be empty."
                                }
                            }
                        }
                    }
                };
            }

            user = Response.Patch(request);

            if(user == null)
            {
                throw new HttpError(HttpStatusCode.NotFound, "Not Found")
                {
                    Response = new ErrorResponse
                    {
                        ResponseStatus = new ResponseStatus
                        {
                            Errors = new List<ResponseError> {
                                new ResponseError {
                                    ErrorCode = "NotFound",
                                    Message = "User not found"
                                }
                            }
                        }
                    }
                };
            }

            base.Response.StatusCode = (int)HttpStatusCode.OK;
            string response = JsonConvert.SerializeObject(user, Formatting.Indented);
            return response;
        }

        public object Post(UserRequest request)
        {
            User user;
            UserResponse Response = new UserResponse();

            if (request.Login.IsNullOrEmpty())
            {
                throw new HttpError(HttpStatusCode.BadRequest, "Bad Request")
                {
                    Response = new ErrorResponse
                    {
                        ResponseStatus = new ResponseStatus
                        {
                            Errors = new List<ResponseError> {
                                new ResponseError {
                                    ErrorCode = "LoginIsNull",
                                    FieldName = "Login",
                                    Message = "'Login' should not be empty."
                                }
                            }
                        }
                    }
                };
            }

            user = Response.Post(request);

            if(user == null)
            {
                throw new HttpError(HttpStatusCode.InternalServerError, "Internal Server Error")
                {
                    Response = new ErrorResponse
                    {
                        ResponseStatus = new ResponseStatus
                        {
                            Errors = new List<ResponseError> {
                                new ResponseError {
                                    ErrorCode = "CannotCreate",
                                    Message = "Cannot Create new user"
                                }
                            }
                        }
                    }
                };
            }

            string response = JsonConvert.SerializeObject(user, Formatting.Indented);
           

            base.Response.StatusCode = (int)HttpStatusCode.Created;
            return response;
        }

        public object Delete(UserRequest request)
        {
            User user;
            UserResponse Response = new UserResponse();

            if (request.Login.IsNullOrEmpty())
            {
                throw new HttpError(HttpStatusCode.BadRequest, "Bad Request")
                {
                    Response = new ErrorResponse
                    {
                        ResponseStatus = new ResponseStatus
                        {
                            Errors = new List<ResponseError> {
                                new ResponseError {
                                    ErrorCode = "LoginIsNull",
                                    FieldName = "Login",
                                    Message = "'Login' should not be empty."
                                }
                            }
                        }
                    }
                };
            }

            user = Response.Post(request);

            if (user == null)
            {
                throw new HttpError(HttpStatusCode.InternalServerError, "Internal Server Error")
                {
                    Response = new ErrorResponse
                    {
                        ResponseStatus = new ResponseStatus
                        {
                            Errors = new List<ResponseError> {
                                new ResponseError {
                                    ErrorCode = "CannotDelete",
                                    Message = "Cannot delete user"
                                }
                            }
                        }
                    }
                };
            }

            string response = JsonConvert.SerializeObject(user, Formatting.Indented);

            base.Response.StatusCode = (int)HttpStatusCode.OK;
            return response;
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
            var posts = Response.Get(request);
            string response = JsonConvert.SerializeObject(posts, Formatting.Indented);
            return response;
        }
        public object Post(UserPostsRequest request)
        {
            UserPostsResponse Response = new UserPostsResponse();
            var resp = Response.Post(request);
            if (resp == 200)
            {
                base.Response.StatusCode = (int)HttpStatusCode.OK;
                return Response;
            }
            else
            {
                base.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Response;
            }
        }
    }

    public class UserPostService : Service
    {
        public object Get(UserPostRequest request)
        {
            UserPostResponse Response = new UserPostResponse();
            var posts = Response.Get(request);
            if (posts == null)
            {
                base.Response.StatusCode = (int)HttpStatusCode.NoContent;
                return Response;
            }
            string response = JsonConvert.SerializeObject(posts, Formatting.Indented);
            return response;
        }

        public object Patch(UserPostRequest request)
        {
            UserPostResponse Response = new UserPostResponse();
            var posts = Response.Patch(request);
            if (posts == null)
            {
                base.Response.StatusCode = (int)HttpStatusCode.NoContent;
                return Response;
            }
            string response = JsonConvert.SerializeObject(posts, Formatting.Indented);
            return response;
        }
    }
}