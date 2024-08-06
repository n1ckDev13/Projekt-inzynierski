using ClassLibrary.Responses.UserMealFood;
using Microsoft.AspNetCore.Mvc;
using TrainingTrackAPI.Application.UseCases.UserMealFoodUseCases;

namespace TrainingTrackAPI.API.ServicesResponseMapping.UserMealFoods
{
    public class HttpResponseGetUserMealFoodMapping
    {
        public static IActionResult MapToHttpResponse(GetUserMealFoodResponse response)
        {
            if (response.isSuccess)
            {
                return new OkObjectResult(new
                {
                    isSuccess = response.isSuccess,
                    message = response.Message,
                    userMealFood = response.userMealFood,
                    details = response.Details
                });
            }
            else
            {
                int statusCode;

                switch (response.Message)
                {
                    case "UserMealFood does not exist.":
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
                    userMealFood = response.userMealFood,
                    details = response.Details
                })
                {
                    StatusCode = statusCode
                };
            }
        }
    }
}
