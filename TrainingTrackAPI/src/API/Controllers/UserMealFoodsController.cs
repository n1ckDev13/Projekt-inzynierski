using ClassLibrary.DTOs.UserMealFoodDTOs;
using Microsoft.AspNetCore.Mvc;
using TrainingTrackAPI.API.ServicesResponseMapping.UserMealFoods;
using TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.UserMealFoodServices;

namespace TrainingTrackAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserMealFoodsController : ControllerBase
    {
        private readonly IGetAllUserMealFoodsService _getAllUserMealFoodsService;
        private readonly IGetUserMealFoodService _getUserMealFoodService;
        private readonly ICreateUserMealFoodService _createUserMealFoodService;
        private readonly IUpdateUserMealFoodService _updateUserMealFoodService;
        private readonly IDeleteUserMealFoodService _deleteUserMealFoodService;

        public UserMealFoodsController(
            IGetAllUserMealFoodsService getAllUserMealFoodsService,
            IGetUserMealFoodService getUserMealFoodService,
            ICreateUserMealFoodService createUserMealFoodService,
            IUpdateUserMealFoodService updateUserMealFoodService,
            IDeleteUserMealFoodService deleteUserMealFoodService)
        {
            _getAllUserMealFoodsService = getAllUserMealFoodsService;
            _getUserMealFoodService = getUserMealFoodService;
            _createUserMealFoodService = createUserMealFoodService;
            _updateUserMealFoodService = updateUserMealFoodService;
            _deleteUserMealFoodService = deleteUserMealFoodService;
        }

        [HttpGet("getAllUserMealFoodsForMeal")]
        public async Task<IActionResult> getAllUserMealFoodsForMeal(int mealId)
        {
            try
            {
                return HttpResponseGetAllUserMealFoodsMapping.MapToHttpResponse(await _getAllUserMealFoodsService.GetAllUserMealFoodAsync(mealId));
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error: {e.Message}");
            }
        }

        [HttpGet("getUserMealFood")]
        public async Task<IActionResult> getUserMealFood(int id)
        {
            try
            {
                return HttpResponseGetUserMealFoodMapping.MapToHttpResponse(await _getUserMealFoodService.GetUserMealFoodAsync(id));
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error: {e.Message}");
            }
        }

        [HttpPost("createUserMealFood")]
        public async Task<IActionResult> createUserMealFood([FromBody]CreateUserMealFoodDTO request)
        {
            try
            {
                return HttpResponseCreateUserMealFoodMapping.MapToHttpResponse(await _createUserMealFoodService.CreateUserMealFoodAsync(request));
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error: {e.Message}");
            }
        }

        [HttpPut("updateUserMealFood")]
        public async Task<IActionResult> updateUserMealFood([FromBody]UpdateUserMealFoodDTO request)
        {
            try
            {
                return HttpResponseUpdateUserMealFoodMapping.MapToHttpResponse(await _updateUserMealFoodService.UpdateMealFoodAsync(request));
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error: {e.Message}");
            }
        }

        [HttpDelete("deleteUserMealFood")]
        public async Task<IActionResult> deleteUserMealFood(int id)
        {
            try
            {
                return HttpResponseDeleteUserMealFoodMapping.MapToHttpResponse(await _deleteUserMealFoodService.DeleteUserMealFoodAsync(id));

            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error: {e.Message}");
            }
        }
    }
}
