using System.Text.Json.Serialization;
using ClassLibrary.JsonCustomConverters;

namespace ClassLibrary.DTOs.MealDTO
{
    public class UpdateMealDTO
    {
        public int Id { get; set; }

        [JsonConverter(typeof(CustomTimeOnlyConverter))]
        public TimeOnly TimeOfEating { get; set; }

        public string MealName { get; set; } = null!;
    }
}
