using ClassLibrary.Responses.Meal;
using Microsoft.AspNetCore.Mvc;
using TrainingTrackAPI.Application.UseCases.MealUseCases;

namespace TrainingTrackAPI.API.ServicesResponseMapping.Meals
{
    public class HttpResponseUpdateMealMapping
    {
        public static IActionResult MapToHttpResponse(UpdateMealResponse response)
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
                    case "Meal does not exist.":
                        statusCode = 404;
                        break;

                    case "Validation error.":
                        statusCode = 400;
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
