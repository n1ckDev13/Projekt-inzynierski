using System.Text.Json.Serialization;
using ClassLibrary.JsonCustomConverters;

namespace ClassLibrary.DTOs.MealDTO
{
    public class CreateMealDTO
    {

        public int DietPlanId { get; set; }

        [JsonConverter(typeof(CustomTimeOnlyConverter))]
        public TimeOnly TimeOfEating { get; set; }

        public string MealName { get; set; } = null!;
    }
}
