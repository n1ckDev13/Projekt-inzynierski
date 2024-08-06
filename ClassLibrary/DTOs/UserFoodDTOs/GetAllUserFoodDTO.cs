namespace ClassLibrary.DTOs.UserFoodDTOs
{
    public class GetAllUserFoodDTO
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string FoodName { get; set; } = null!;

        public decimal CaloriesPer100g { get; set; }

        public decimal ProteinPer100g { get; set; }

        public decimal CarbsPer100g { get; set; }

        public decimal FatPer100g { get; set; }
    }
}
