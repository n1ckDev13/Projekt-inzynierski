using ClassLibrary.Responses.User;
using Microsoft.AspNetCore.Mvc;
using TrainingTrackAPI.Application.UseCases.UserUseCases;

namespace TrainingTrackAPI.API.ServicesResponseMapping.Users
{
    public class HttpResponseGetAllUsersMapping
    {
        public static IActionResult MapToHttpResponse(GetAllUsersResponse response)
        {
            if (response.isSuccess)
            {
                return new OkObjectResult(new
                {
                    isSuccess = response.isSuccess,
                    message = response.Message,
                    usersList = response.UsersList,
                    details = response.Details
                }) ;
            }
            else
            {
                int statusCode;

                switch (response.Message)
                {
                    case "No data returned.":
                        statusCode = 404;
                        break;

                    
                    case "Database error.":
                        statusCode = 500;
                        break;

                    default:
                        statusCode = 500;
                        break;

                }

                return new ObjectResult(new
                {
                    isSuccess = response.isSuccess,
                    message = response.Message,
                    details = response.Details
                })
                {
                    StatusCode = statusCode
                };
            }
        }
    }
}

