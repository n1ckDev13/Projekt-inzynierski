using ClassLibrary.Responses.DietPlan;
using Microsoft.AspNetCore.Mvc;
using TrainingTrackAPI.Application.UseCases.DietPlansUseCases;

namespace TrainingTrackAPI.API.ServicesResponseMapping.DietPlans
{
    public class HttpResponseGetDietPlanMapping
    {
        public static IActionResult MapToHttpResponse(GetDietPlanResponse response)
        {
            if (response.isSuccess)
            {
                return new OkObjectResult(new
                {
                    isSuccess = response.isSuccess,
                    message = response.Message,
                    dietPlan = response.dietPlan,
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
                    dietPlan = response.dietPlan,
                    details = response.Details
                })
                {
                    StatusCode = statusCode
                };
            }
        }
    }
}
