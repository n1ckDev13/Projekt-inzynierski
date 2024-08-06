using ClassLibrary.Responses.User;
using Microsoft.AspNetCore.Mvc;
using TrainingTrackAPI.Application.UseCases.UserUseCases;

namespace TrainingTrackAPI.API.ServicesResponseMapping.Users
{
    public class HttpResponseRegisterUserMapping
    {
        public static IActionResult MapToHttpResponse(RegisterUserResponse response)
        {
            if (response.isSuccess)
            {
                return new OkObjectResult(new
                {
                    isSuccess = response.isSuccess,
                    message = response.Message,
                    details = response.Details
                });
            }
            else
            {
                int statusCode;

                switch (response.Message)
                {
                    case "Błąd walidacji.":
                        statusCode = 400;
                        break;

                    case "Podany adres email jest już w użyciu.":
                        statusCode = 409;
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
