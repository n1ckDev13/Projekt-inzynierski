using ClassLibrary.Responses.MealFood;
using Microsoft.AspNetCore.Mvc;
using TrainingTrackAPI.Application.UseCases.MealFoodUseCases;

namespace TrainingTrackAPI.API.ServicesResponseMapping.MealFoods
{
    public class HttpResponseGetAllMealFoodsMapping
    {
        public static IActionResult MapToHttpResponse(GetAllMealFoodsResponse response)
        {
            if (response.isSuccess)
            {
                return new OkObjectResult(new
                {
                    isSuccess = response.isSuccess,
                    message = response.Message,
                    mealFoodsList = response.MealFoodsList,
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
                    mealFoodsList = response.MealFoodsList,
                    details = response.Details
                })
                {
                    StatusCode = statusCode
                };
            }
        }
    }
}
