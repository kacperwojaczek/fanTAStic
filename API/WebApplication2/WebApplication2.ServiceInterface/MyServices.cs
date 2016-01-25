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

    public class UsersService : Service
    {
        public object Get(UserRequest request)
        {
            UserResponse Response = new UserResponse();
            UserErrorWrapper getResponse;

            if (request.Login.IsNullOrEmpty())
            {
                var usersList = Response.GetAll(request);
                if (usersList == null)
                {
                    throw new HttpError(HttpStatusCode.InternalServerError, "Internal Server Error")
                    {
                        Response = new ErrorResponse
                        {
                            ResponseStatus = new ResponseStatus
                            {
                                Errors = new List<ResponseError> {
                                            new ResponseError {
                                                ErrorCode = "ServerError",
                                                Message = "Server is unable to process request"
                                            }
                                        }
                            }
                        }
                    };
                }
                else
                {
                    base.Response.StatusCode = (int)HttpStatusCode.OK;
                    string users = JsonConvert.SerializeObject(usersList, Formatting.Indented);
                    return users;
                }
            }

            getResponse = Response.Get(request);

            if (getResponse.user == null)
            {
                if (getResponse.status == HttpStatusCode.NotFound)
                {
                    throw new HttpError(getResponse.status, "Not Found")
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
                else if (getResponse.status == HttpStatusCode.InternalServerError)
                {
                    throw new HttpError(getResponse.status, "Internal Server Error")
                    {
                        Response = new ErrorResponse
                        {
                            ResponseStatus = new ResponseStatus
                            {
                                Errors = new List<ResponseError> {
                                            new ResponseError {
                                                ErrorCode = "ServerError",
                                                Message = "Server is unable to process request"
                                            }
                                        }
                            }
                        }
                    };
                }
            }
            base.Response.StatusCode = (int)getResponse.status;
            string response = JsonConvert.SerializeObject(getResponse.user, Formatting.Indented);
            return response;
        }

        public object Patch(UserRequest request)
        {
            UserErrorWrapper getResponse;
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

            getResponse = Response.Patch(request);

            if (getResponse.user == null)
            {
                if (getResponse.status == HttpStatusCode.NotFound)
                {
                    throw new HttpError(getResponse.status, "Not Found")
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
                else if (getResponse.status == HttpStatusCode.InternalServerError)
                {
                    throw new HttpError(getResponse.status, "Internal Server Error")
                    {
                        Response = new ErrorResponse
                        {
                            ResponseStatus = new ResponseStatus
                            {
                                Errors = new List<ResponseError> {
                                new ResponseError {
                                    ErrorCode = "ServerError",
                                    Message = "Server is unable to process request"
                                }
                            }
                            }
                        }
                    };
                }
            }
            base.Response.StatusCode = (int)getResponse.status;
            string response = JsonConvert.SerializeObject(getResponse.user, Formatting.Indented);
            return response;
        }

        public object Post(UserRequest request)
        {
            UserErrorWrapper getResponse;
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

            getResponse = Response.Post(request);

            if (getResponse.user == null)
            {
                if (getResponse.status == HttpStatusCode.InternalServerError)
                {
                    throw new HttpError(getResponse.status, "Internal Server Error")
                    {
                        Response = new ErrorResponse
                        {
                            ResponseStatus = new ResponseStatus
                            {
                                Errors = new List<ResponseError> {
                                new ResponseError {
                                    ErrorCode = "ServerError",
                                    Message = "Server is unable to process request"
                                }
                            }
                            }
                        }
                    };
                }
                else if (getResponse.status == HttpStatusCode.Conflict)
                {
                    throw new HttpError(getResponse.status, "Conflict")
                    {
                        Response = new ErrorResponse
                        {
                            ResponseStatus = new ResponseStatus
                            {
                                Errors = new List<ResponseError> {
                                new ResponseError {
                                    ErrorCode = "LoginConflict",
                                    Message = "User already exists"
                                }
                            }
                            }
                        }
                    };
                }
            }

            string response = JsonConvert.SerializeObject(getResponse.user, Formatting.Indented);
            base.Response.StatusCode = (int)getResponse.status;
            return response;
        }

        public object Delete(UserRequest request)
        {
            UserErrorWrapper getResponse;
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

            getResponse = Response.Delete(request);

            if (getResponse.user == null)
            {
                if (getResponse.status == HttpStatusCode.InternalServerError)
                {
                    throw new HttpError(getResponse.status, "Internal Server Error")
                    {
                        Response = new ErrorResponse
                        {
                            ResponseStatus = new ResponseStatus
                            {
                                Errors = new List<ResponseError> {
                                new ResponseError {
                                    ErrorCode = "ServerError",
                                    Message = "Server cannot process request"
                                }
                            }
                            }
                        }
                    };
                }
                else if (getResponse.status == HttpStatusCode.NotFound)
                {
                    throw new HttpError(getResponse.status, "Not Found")
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
                else if (getResponse.status == HttpStatusCode.Forbidden)
                {
                    throw new HttpError(getResponse.status, "Forbidden")
                    {
                        Response = new ErrorResponse
                        {
                            ResponseStatus = new ResponseStatus
                            {
                                Errors = new List<ResponseError> {
                                new ResponseError {
                                    ErrorCode = "Forbidden",
                                    Message = "Cannot delete a user that has posts"
                                }
                            }
                            }
                        }
                    };
                }
            }

            string response = JsonConvert.SerializeObject(getResponse.user, Formatting.Indented);

            base.Response.StatusCode = (int)getResponse.status;
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
            
            HttpStatusCode status = Response.Post(request);

            if(status == HttpStatusCode.Unauthorized)
            {
                throw new HttpError(status, "Unauthorized")
                {
                    Response = new ErrorResponse
                    {
                        ResponseStatus = new ResponseStatus
                        {
                            Errors = new List<ResponseError> {
                                new ResponseError {
                                    ErrorCode = "Unauthorized",
                                    Message = "Incorrect password"
                                }
                            }
                        }
                    }
                };
            }
            else if (status == HttpStatusCode.InternalServerError)
            {
                throw new HttpError(status, "Internal Server Error")
                {
                    Response = new ErrorResponse
                    {
                        ResponseStatus = new ResponseStatus
                        {
                            Errors = new List<ResponseError> {
                                new ResponseError {
                                    ErrorCode = "ServerError",
                                    Message = "Server cannot process request"
                                }
                            }
                        }
                    }
                };
            }
            else if (status == HttpStatusCode.NotFound)
            {
                throw new HttpError(status, "Not Found")
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

            return Response;
        }
    }
    public class UserPostsService : Service
    {
        public object Get(UserPostsRequest request)
        {
            UserPostsResponse Response = new UserPostsResponse();

            var posts = Response.GetAll(request);

            if (posts.userPosts == null)
            {
                if (posts.status == HttpStatusCode.InternalServerError)
                {
                    throw new HttpError(posts.status, "Internal Server Error")
                    {
                        Response = new ErrorResponse
                        {
                            ResponseStatus = new ResponseStatus
                            {
                                Errors = new List<ResponseError> {
                                            new ResponseError {
                                                ErrorCode = "ServerError",
                                                Message = "Server is unable to process request"
                                            }
                                        }
                            }
                        }
                    };
                }
                else if (posts.status == HttpStatusCode.NoContent)
                {
                    base.Response.StatusCode = (int)posts.status;
                    return null;
                }
                else if (posts.status == HttpStatusCode.NotFound)
                {
                    throw new HttpError(posts.status, "Not Found")
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
            }
            base.Response.StatusCode = (int)posts.status;
            string allPosts = JsonConvert.SerializeObject(posts.userPosts, Formatting.Indented);
            return allPosts;
        }
        public object Post(UserPostsRequest request)
        {
            UserPostsResponse Response = new UserPostsResponse();
            var post = Response.Post(request);

            if (post.userPost == null)
            {
                if (post.status == HttpStatusCode.InternalServerError)
                {
                    throw new HttpError(post.status, "Internal Server Error")
                    {
                        Response = new ErrorResponse
                        {
                            ResponseStatus = new ResponseStatus
                            {
                                Errors = new List<ResponseError> {
                                            new ResponseError {
                                                ErrorCode = "ServerError",
                                                Message = "Server is unable to process request"
                                            }
                                        }
                            }
                        }
                    };
                }
                else if (post.status == HttpStatusCode.NotFound)
                {
                    throw new HttpError(post.status, "Not Found")
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
            }
            base.Response.StatusCode = (int)post.status;
            string allPosts = JsonConvert.SerializeObject(post.userPost, Formatting.Indented);
            return allPosts;

        }
    }
    public class PostsService : Service
    {
        public object Get(PostsRequest request)
        {
            var Response = new PostResponse();

            if (request.Id.IsNullOrEmpty())
            {
                var posts = Response.GetAll(request);

                if (posts.userPosts == null)
                {
                    if (posts.status == HttpStatusCode.InternalServerError)
                    {
                        throw new HttpError(posts.status, "Internal Server Error")
                        {
                            Response = new ErrorResponse
                            {
                                ResponseStatus = new ResponseStatus
                                {
                                    Errors = new List<ResponseError> {
                                            new ResponseError {
                                                ErrorCode = "ServerError",
                                                Message = "Server is unable to process request"
                                            }
                                        }
                                }
                            }
                        };
                    }
                    else if (posts.status == HttpStatusCode.NoContent)
                    {
                        base.Response.StatusCode = (int)posts.status;
                        return null;
                    }
                }

                base.Response.StatusCode = (int)posts.status;
                string allPosts = JsonConvert.SerializeObject(posts.userPosts, Formatting.Indented);
                return allPosts;
            }
            else
            {
                var posts = Response.Get(request);

                if (posts.userPost == null)
                {
                    if (posts.status == HttpStatusCode.InternalServerError)
                    {
                        throw new HttpError(posts.status, "Internal Server Error")
                        {
                            Response = new ErrorResponse
                            {
                                ResponseStatus = new ResponseStatus
                                {
                                    Errors = new List<ResponseError> {
                                            new ResponseError {
                                                ErrorCode = "ServerError",
                                                Message = "Server is unable to process request"
                                            }
                                        }
                                }
                            }
                        };
                    }
                    else if (posts.status == HttpStatusCode.NotFound)
                    {
                        throw new HttpError(posts.status, "Not Found")
                        {
                            Response = new ErrorResponse
                            {
                                ResponseStatus = new ResponseStatus
                                {
                                    Errors = new List<ResponseError> {
                                        new ResponseError {
                                            ErrorCode = "NotFound",
                                            Message = "Post not found"
                                        }
                                    }
                                }
                            }
                        };
                    }
                }

                base.Response.StatusCode = (int)posts.status;
                string allPosts = JsonConvert.SerializeObject(posts.userPost, Formatting.Indented);
                return allPosts;
            }
        }
        public object Put(PostsRequest request)
        {
            var Response = new PostResponse();
            var posts = Response.Put(request);

            if (posts.userPost == null)
            {
                if (posts.status == HttpStatusCode.InternalServerError)
                {
                    throw new HttpError(posts.status, "Internal Server Error")
                    {
                        Response = new ErrorResponse
                        {
                            ResponseStatus = new ResponseStatus
                            {
                                Errors = new List<ResponseError> {
                                            new ResponseError {
                                                ErrorCode = "ServerError",
                                                Message = "Server is unable to process request"
                                            }
                                        }
                            }
                        }
                    };
                }
                else if (posts.status == HttpStatusCode.NotFound)
                {
                    throw new HttpError(posts.status, "Not Found")
                    {
                        Response = new ErrorResponse
                        {
                            ResponseStatus = new ResponseStatus
                            {
                                Errors = new List<ResponseError> {
                                        new ResponseError {
                                            ErrorCode = "NotFound",
                                            Message = "Post not found"
                                        }
                                    }
                            }
                        }
                    };
                }
            }

            base.Response.StatusCode = (int)posts.status;
            string allPosts = JsonConvert.SerializeObject(posts.userPost, Formatting.Indented);
            return allPosts;

        }

    }
}