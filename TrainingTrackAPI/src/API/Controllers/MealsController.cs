using ClassLibrary.DTOs.MealDTO;
using Microsoft.AspNetCore.Mvc;
using TrainingTrackAPI.API.ServicesResponseMapping.Meals;
using TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.MealServices;

namespace TrainingTrackAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealsController : ControllerBase
    {
        private readonly IGetAllMealsService _getAllMealsService;
        private readonly IGetMealService _getMealService;
        private readonly ICreateMealService _createMealService;
        private readonly IUpdateMealService _updateMealService;
        private readonly IDeleteMealService _deleteMealService;
        

        public MealsController(IGetAllMealsService getAllMealsService,
            IGetMealService getMealService,
            ICreateMealService createMealService, 
            IUpdateMealService updateMealService,
            IDeleteMealService deleteMealService)
        {
            _getAllMealsService = getAllMealsService;
            _getMealService = getMealService;
            _createMealService = createMealService;
            _updateMealService = updateMealService;
            _deleteMealService = deleteMealService;
        }

        [HttpGet("getAllMealsForDietPlan")]
        public async Task<IActionResult> getAllMealsForDietPlan(int dietPlanId)
        {
            try
            {
                return HttpResponseGetAllMealsMappingcs.MapToHttpResponse(await _getAllMealsService.GetAllMealsAsync(dietPlanId));
            }
            catch(Exception e)
            {
                return StatusCode(500, $"Error: {e.Message}");
            }
        }

        [HttpGet("getMeal")]
        public async Task<IActionResult> getMeal(int id)
        {
            try
            {
                return HttpResponseGetMealMapping.MapToHttpResponse(await _getMealService.GetMealAsync(id));
            }
            catch(Exception e)
            {
                return StatusCode(500, $"Error: {e.Message}");
            }
        }

        [HttpPost("createMeal")]
        public async Task<IActionResult> createMeal([FromBody]CreateMealDTO request)
        {
            try
            {
                return HttpResponseCreateMealMapping.MapToHttpResponse(await _createMealService.CreateMealAsync(request));
            }
            catch(Exception e)
            {
                return StatusCode(500, $"Error: {e.Message}");
            }
        }

        [HttpPut("updateMeal")]
        public async Task<IActionResult> updateMeal([FromBody]UpdateMealDTO request)
        {
            try
            {
                return HttpResponseUpdateMealMapping.MapToHttpResponse(await _updateMealService.UpdateMealAsync(request));
            }
            catch(Exception e)
            {
                return StatusCode(500, $"Error: {e.Message}");
            }
        }

        [HttpDelete("deleteMeal")]
        public async Task<IActionResult> deleteMeal(int id)
        {
            try
            {
                return HttpResponseDeleteMealMapping.MapToHttpResponse(await _deleteMealService.DeleteMealAsync(id));
            }
            catch(Exception e) 
            {
                return StatusCode(500, $"Error: {e.Message}");
            }
        }
    }
}
