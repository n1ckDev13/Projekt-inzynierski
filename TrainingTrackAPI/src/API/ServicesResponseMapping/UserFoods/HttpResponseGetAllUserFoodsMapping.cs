using ClassLibrary.Responses.UserFood;
using Microsoft.AspNetCore.Mvc;
using TrainingTrackAPI.Application.UseCases.UserFoodUseCases;

namespace TrainingTrackAPI.API.ServicesResponseMapping.UserFoods
{
    public class HttpResponseGetAllUserFoodsMapping
    {
        public static IActionResult MapToHttpResponse(GetAllUserFoodsResponse response)
        {
            if (response.isSuccess)
            {
                return new OkObjectResult(new
                {
                    isSuccess = response.isSuccess,
                    message = response.Message,
                    userFoodList = response.UserFoodsList,
                    details = response.Details
                });
            }
            else
            {
                int statusCode;

                switch (response.Message)
                {
                    case "User does not exist.":
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
                    userFoodList = response.UserFoodsList,
                    details = response.Details
                })
                {
                    StatusCode = statusCode
                };
            }
        }
    }
}
