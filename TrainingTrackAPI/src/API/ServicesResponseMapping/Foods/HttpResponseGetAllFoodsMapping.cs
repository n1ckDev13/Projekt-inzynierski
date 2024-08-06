using ClassLibrary.Responses.Food;
using Microsoft.AspNetCore.Mvc;
using TrainingTrackAPI.Application.UseCases.FoodUseCases;

namespace TrainingTrackAPI.API.ServicesResponseMapping.Foods
{
    public class HttpResponseGetAllFoodsMapping
    {
        public static IActionResult MapToHttpResponse(GetAllFoodsResponse response)
        {
            if (response.isSuccess)
            {
                return new OkObjectResult(new
                {
                    isSuccess = response.isSuccess,
                    message = response.Message,
                    foodsList = response.FoodList,
                    details = response.Details
                });
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
                    foodsList = response.FoodList,
                    details = response.Details
                })
                {
                    StatusCode = statusCode
                };
            }
        }
    }
}
