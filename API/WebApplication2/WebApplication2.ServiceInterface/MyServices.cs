using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack;
using WebApplication2;
using WebApplication2.ServiceModel;

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
            return new RegistrationResponse { Result = "User {0} ({1} {2}) successfully registered!".Fmt(request.Login, request.Firstname, request.Lastname) };
        }
    }

    public class UsersService: Service
    {
        public object Get(UserRequest request)
        {
            if (request.Login.IsNullOrEmpty())
            {
                return new UserResponse { Result = "Returning all users list" };
            }
            return new UserResponse { Result = "User {0} data...".Fmt(request.Login) };
        }

        //modyfikacja danych uzytkownika

        public object Patch(UserRequest request)
        {
            return new UserResponse { Result = "Modifiying user {0} name to {1}".Fmt(request.Login, request.Firstname) };
        }
    }
}