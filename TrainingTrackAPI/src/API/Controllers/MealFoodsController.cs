using ClassLibrary.DTOs.MealFoodsDTOs;
using Microsoft.AspNetCore.Mvc;
using TrainingTrackAPI.API.ServicesResponseMapping.MealFoods;
using TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.MealFoodServices;

namespace TrainingTrackAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealFoodsController : ControllerBase
    {
        private readonly IGetAllMealFoodsService _getAllMealFoodsService;
        private readonly IGetMealFoodService _getMealFoodService;
        private readonly ICreateMealFoodService _createMealFoodService;
        private readonly IUpdateMealFoodService _updateMealFoodService;
        private readonly IDeleteMealFoodService _deleteMealFoodService;
        

        public MealFoodsController(IGetAllMealFoodsService getAllMealFoodsService
            ,IGetMealFoodService getMealFoodService
            ,ICreateMealFoodService createMealFoodService,
            IUpdateMealFoodService updateMealFoodService,
            IDeleteMealFoodService deleteMealFoodService)
        {
            _getAllMealFoodsService = getAllMealFoodsService;
            _getMealFoodService = getMealFoodService;
            _createMealFoodService = createMealFoodService;
            _updateMealFoodService = updateMealFoodService;
            _deleteMealFoodService = deleteMealFoodService;
        }

        [HttpGet("getAllMealFoodsForMeal")]
        public async Task<IActionResult> getAllMealFoodsForMeal(int mealId)
        {
            try
            {
                return HttpResponseGetAllMealFoodsMapping.MapToHttpResponse(await _getAllMealFoodsService.GetAllMealFoodsAsync(mealId));
            }
            catch(Exception e)
            {
                return StatusCode(500, $"Error: {e.Message}");
            }
        }

        [HttpGet("getMealFood")]
        public async Task<IActionResult> getMealFood(int id)
        {
            try
            {
                return HttpResponseGetMealFoodMapping.MapToHttpResponse(await _getMealFoodService.GetMealFoodAsync(id));
            }
            catch(Exception e)
            {
                return StatusCode(500, $"Error: {e.Message}");
            }
        }

        [HttpPost("createMealFood")]
        public async Task<IActionResult> createMealFood([FromBody]CreateMealFoodDTO request)
        {
            try
            {
                return HttpResponseCreateMealFoodMapping.MapToHttpResponse(await _createMealFoodService.CreateMealFoodAsync(request));
            }
            catch(Exception e)
            {
                return StatusCode(500, $"Error: {e.Message}");
            }
        }

        [HttpPut("updateMealFood")]
        public async Task<IActionResult> updateMealFood([FromBody]UpdateMealFoodDTO request)
        {
            try
            {
                return HttpResponseUpdateMealFoodMapping.MapToHttpResponse(await _updateMealFoodService.UpdateMealFoodAsync(request));
            }
            catch(Exception e)
            {
                return StatusCode(500, $"Error: {e.Message}");
            }
        }

        [HttpDelete("deleteMealFood")]
        public async Task<IActionResult> deleteMealFood(int id)
        {
            try
            {
                return HttpResponseDeleteMealFoodMapping.MapToHttpResponse(await _deleteMealFoodService.DeleteMealFoodAsync(id));
                
            }
            catch(Exception e)
            {
                return StatusCode(500, $"Error: {e.Message}");
            }
        }
    }
}
