using ClassLibrary.Responses.Meal;
using Microsoft.AspNetCore.Mvc;
using TrainingTrackAPI.Application.UseCases.MealUseCases;

namespace TrainingTrackAPI.API.ServicesResponseMapping.Meals
{
    public class HttpResponseGetAllMealsMappingcs
    {
        public static IActionResult MapToHttpResponse(GetAllMealsResponse response)
        {
            if (response.isSuccess)
            {
                return new OkObjectResult(new
                {
                    isSuccess = response.isSuccess,
                    message = response.Message,
                    mealList = response.MealList,
                    details = response.Details
                });
            }
            else
            {
                int statusCode;

                switch (response.Message)
                {
                    case "Diet plan does not exist.":
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
                    mealList = response.MealList,
                    details = response.Details
                })
                {
                    StatusCode = statusCode
                };
            }
        }
    }
}
