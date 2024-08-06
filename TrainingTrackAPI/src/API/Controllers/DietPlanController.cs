using ClassLibrary.DTOs.DietPlanDTO;
using Microsoft.AspNetCore.Mvc;
using TrainingTrackAPI.API.ServicesResponseMapping.DietPlans;
using TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.DietPlanServices;

namespace TrainingTrackAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DietPlanController : ControllerBase
    {
        private readonly ICreateDietPlanService _createDietPlanService;
        private readonly IUpdateDietPlanService _updateDietPlanService;
        private readonly IDisableDietPlanService _disableDietPlanService;
        private readonly IGetAllDietPlansService _getAllDietPlansService;
        private readonly IGetDietPlanService _getDietPlanService;

        public DietPlanController(ICreateDietPlanService createDietPlanService,
            IUpdateDietPlanService updateDietPlanService,
            IDisableDietPlanService disableDietPlanService,
            IGetAllDietPlansService getAllDietPlansService,
            IGetDietPlanService getDietPlanService)
        {
            _createDietPlanService = createDietPlanService;
            _updateDietPlanService = updateDietPlanService;
            _disableDietPlanService = disableDietPlanService;
            _getAllDietPlansService = getAllDietPlansService;
            _getDietPlanService = getDietPlanService;
        }

        [HttpGet("getAllDietPlansForUser")]
        public async Task<IActionResult> getAllDietPlansForUser(int userId)
        {
            try
            {
                return HttpResponseGetAllDietPlansMapping.MapToHttpResponse(await _getAllDietPlansService.GetAllDietPlansAsync(userId));
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error: {e.Message}");
            }
        }


        [HttpGet("getDietPlan")]
        public async Task<IActionResult> getDietPlan(int id)
        {
            try
            {
                return HttpResponseGetDietPlanMapping.MapToHttpResponse(await _getDietPlanService.GetDietPlanAsync(id));
            }
            catch(Exception e)
            {
                return StatusCode(500, $"Error: {e.Message}");
            }
        }

        [HttpPost("createDietPlan")]
        public async Task<IActionResult> createDietPlan([FromBody]CreateDietPlanDTO request)
        {
            try
            {

                return HttpResponseCreateDietPlanMapping.MapToHttpResponse(await _createDietPlanService.CreateDietPlanAsync(request));

            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error: {e.Message}");
            }
        }

        [HttpPut("updateDietPlan")]
        public async Task<IActionResult> updateDietPlan([FromBody]UpdateDietPlanDTO request)
        {
            try
            {
                return HttpResponseUpdateDietPlanMapping.MapToHttpResponse(await _updateDietPlanService.UpdateDietPlanAsync(request));
            }
            catch(Exception e)
            {
                return StatusCode(500, $"Error: {e.Message}");
            }
        }

        [HttpPut("disableDietPlan")]
        public async Task<IActionResult> disableDietPlan(int id)
        {
            try
            {
                return HttpResponseDisableDietPlanMapping.MapToHttpResponse(await _disableDietPlanService.DisableDietPlanAsync(id));
            }
            catch(Exception e)
            {
                return StatusCode(500, $"Error: {e.Message}");
            }
        }
    }
}
