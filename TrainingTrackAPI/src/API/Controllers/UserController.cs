using ClassLibrary.DTOs.UserDTOs;
using Microsoft.AspNetCore.Mvc;
using TrainingTrackAPI.API.ServicesResponseMapping.Users;
using TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.UserServices;

namespace TrainingTrackAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        
        private readonly IGetAllUsersService _getAllUsersService;
        private readonly ILoginUserService _loginUserService;
        private readonly IRegisterUserService _registerUserService;
        private readonly IUpdateUserService _updateUserService;
        private readonly IDeactivateUserService _deactivateUserService;
        

        public UserController( IRegisterUserService registerUserService, 
            IGetAllUsersService getAllUsersService, ILoginUserService loginUserService,
            IUpdateUserService updateUserService, IDeactivateUserService deactivateUserService)
        {
           
            _getAllUsersService = getAllUsersService;
            _loginUserService = loginUserService;
            _registerUserService = registerUserService;
            _updateUserService = updateUserService;
            _deactivateUserService = deactivateUserService;
            
        }

        [HttpGet("getAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {

               return HttpResponseGetAllUsersMapping.MapToHttpResponse(await _getAllUsersService.GetAllUsersAsync());

            } catch (Exception e)
            {
                return StatusCode(500, $"Error: {e.Message}");
            }
            
        }

        [HttpPost("loginUser")]
        public async Task<IActionResult> GetUser([FromBody]LoginUserDTO request)
        {
            try
            {
                return HttpResponseLoginUserMapping.MapToHttpResponse(await _loginUserService.LoginUserAsync(request));
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error: {e.Message}");
            }
        }

        [HttpPost("registerUser")]
        public async Task<IActionResult> RegisterUser([FromBody]RegisterUserDTO request)
        {
            try
            {

               return HttpResponseRegisterUserMapping.MapToHttpResponse(await _registerUserService.RegisterUserAsync(request));
                
            }
            catch(Exception e) 
            {
                return StatusCode(500, $"Error: {e.Message}");
            }
        }

        [HttpPost("updateUserData")]
        public async Task<IActionResult> UpdateUser([FromBody]UpdateUserDTO request)
        {
            try
            {
                return HttpResponseUpdateUserMapping.MapToHttpResponse(await _updateUserService.UpdateUserAsync(request));
            }
            catch(Exception e)
            {
                return StatusCode(500, $"Error : {e.Message}");
            }
        }

        

        [HttpPut("deactivateUserAccount")]
        public async Task<IActionResult> deactivateUser([FromBody] DeactivateUserDTO request)
        {
            try
            {

                return HttpResponseDeactivateUserMapping.MapToHttpResponse(await _deactivateUserService.DeactivateUserAsync(request));

            }
            catch(Exception e)
            {
                return StatusCode(500, $"Error : {e.Message}");
            }
        }

    }
}
