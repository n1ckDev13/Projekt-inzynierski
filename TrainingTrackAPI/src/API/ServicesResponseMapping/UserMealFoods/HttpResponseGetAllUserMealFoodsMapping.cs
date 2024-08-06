using ClassLibrary.Responses.UserMealFood;
using Microsoft.AspNetCore.Mvc;
using TrainingTrackAPI.Application.UseCases.UserMealFoodUseCases;

namespace TrainingTrackAPI.API.ServicesResponseMapping.UserMealFoods
{
    public class HttpResponseGetAllUserMealFoodsMapping
    {
        public static IActionResult MapToHttpResponse(GetAllUserMealFoodsResponse response)
        {
            if (response.isSuccess)
            {
                return new OkObjectResult(new
                {
                    isSuccess = response.isSuccess,
                    message = response.Message,
                    userMealFoodsList = response.UserMealFoodsList,
                    details = response.Details
                });
            }
            else
            {
                int statusCode;

                switch (response.Message)
                {
                    case "Meal does not exist.":
                        statusCode = 404;
                        break;

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
                    userMealFoodsList = response.UserMealFoodsList,
                    details = response.Details
                })
                {
                    StatusCode = statusCode
                };
            }
        }
    }
}
