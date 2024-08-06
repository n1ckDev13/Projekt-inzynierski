using ClassLibrary.Responses.User;
using Microsoft.AspNetCore.Mvc;
using TrainingTrackAPI.Application.UseCases.UserUseCases;

namespace TrainingTrackAPI.API.ServicesResponseMapping.Users
{
    public class HttpResponseLoginUserMapping
    {
        public static IActionResult MapToHttpResponse(LoginUserResponse response)
        {
            if (response.isSuccess)
            {
                return new OkObjectResult(new
                {
                    isSuccess = response.isSuccess,
                    message = response.Message,
                    token = response.Token,
                    details = response.Details
                });
            }
            else
            {
                int statusCode;

                switch (response.Message)
                {
                    case "Nieprawidłowy email lub hasło!":
                        statusCode = 401;
                        break;

                    case "To konto jest dezaktywowane.":
                        statusCode = 403;
                        break;

                    default:
                        statusCode = 500;
                        break;

                }

                return new ObjectResult(new
                {
                    isSuccess = response.isSuccess,
                    message = response.Message,
                    token = response.Token,
                    details = response.Details
                })
                {
                    StatusCode = statusCode
                };
            }
        }
    }
}
