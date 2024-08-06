using ClassLibrary.DTOs.UserFoodDTOs;
using Microsoft.AspNetCore.Mvc;
using TrainingTrackAPI.API.ServicesResponseMapping.UserFoods;
using TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.UserFoodServices;

namespace TrainingTrackAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserFoodsController : ControllerBase
    {
        private readonly IGetAllUserFoodsService _getAllUserFoodsService;
        private readonly IGetUserFoodService _getUserFoodService;
        private readonly ICreateUserFoodService _createUserFoodService;
        private readonly IUpdateUserFoodService _updateUserFoodService;
        private readonly IDeleteUserFoodService _deleteUserFoodService;

        public UserFoodsController(IGetAllUserFoodsService getAllUserFoodsService,
            IGetUserFoodService getUserFoodService ,ICreateUserFoodService createUserFoodService,
            IUpdateUserFoodService updateUserFoodService,
            IDeleteUserFoodService deleteUserFoodService)
        {
            _getAllUserFoodsService = getAllUserFoodsService;
            _getUserFoodService = getUserFoodService;
            _createUserFoodService = createUserFoodService;
            _updateUserFoodService = updateUserFoodService;
            _deleteUserFoodService = deleteUserFoodService;
        }

        [HttpGet("getAllUserFoodsForUser")]
        public async Task<IActionResult> getAllUserFoodsForUser(int userId)
        {
            try
            {
                return HttpResponseGetAllUserFoodsMapping.MapToHttpResponse(await _getAllUserFoodsService.GetAllUserFoodsAsync(userId));
            }
            catch(Exception e)
            {
                return StatusCode(500, $"Error: {e.Message}");
            }
        }

        [HttpGet("getUserFood")]
        public async Task<IActionResult> getUserFood(int id)
        {
            try
            {
                return HttpResponseGetUserFoodMapping.MapToHttpResponse(await _getUserFoodService.GetUserFoodAsync(id));
            }
            catch(Exception e)
            {
                return StatusCode(500, $"Error: {e.Message}");
            }
        }

        [HttpPost("createUserFood")]
        public async Task<IActionResult> createUserFood([FromBody]CreateUserFoodDTO request)
        {
            try
            {
                return HttpResponseCreateUserFoodMapping.MapToHttpResponse(await _createUserFoodService.CreateUserFoodAsync(request));
            }
            catch(Exception e)
            {
                return StatusCode(500, $"Error: {e.Message}");
            }
        }

        [HttpPut("updateUserFood")]
        public async Task<IActionResult> updateUserFood([FromBody]UpdateUserFoodDTO request)
        {
            try
            {
                return HttpResponseUpdateUserFoodMapping.MapToHttpResponse(await _updateUserFoodService.UpdateUserFoodAsync(request));
            }
            catch(Exception e)
            {
                return StatusCode(500, $"Error: {e.Message}");
            }
        }

        [HttpDelete("deleteUserFood")]
        public async Task<IActionResult> deleteUserFood(int id)
        {
            try
            {
                return HttpResponseDeleteUserFoodMapping.MapToHttpResponse(await _deleteUserFoodService.DeleteUserFoodAsync(id));
            }
            catch(Exception e)
            {
                return StatusCode(500, $"Error: {e.Message}");
            }
        }


    }
}
