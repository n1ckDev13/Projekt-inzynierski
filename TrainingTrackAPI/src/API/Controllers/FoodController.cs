using Microsoft.AspNetCore.Mvc;
using TrainingTrackAPI.API.ServicesResponseMapping.Foods;
using TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.FoodServices;

namespace TrainingTrackAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController : ControllerBase
    {

        private readonly IGetAllFoodsService _getAllFoodService;
        private readonly IGetFoodService _getFoodService;

        public FoodController(IGetAllFoodsService getAllFoodService, IGetFoodService getFoodService)
        {
            _getAllFoodService = getAllFoodService;
            _getFoodService = getFoodService;
        }

        [HttpGet("getAllFoods")]
        public async Task<IActionResult> getAllFoods()
        {
            try
            {
                return HttpResponseGetAllFoodsMapping.MapToHttpResponse(await _getAllFoodService.GetAllFoodsAsync());
            }
            catch(Exception e)
            {
                return StatusCode(500, $"Error: {e.Message}");
            }
        }

        [HttpGet("getFood")]
        public async Task<IActionResult> getFood(int id)
        {
            try
            {
                return HttpResponseGetFoodMapping.MapToHttpResponse(await _getFoodService.GetFoodAsync(id));
            }
            catch(Exception e)
            {
                return StatusCode(500, $"Error {e.Message}");
            }
        }

    }
}
