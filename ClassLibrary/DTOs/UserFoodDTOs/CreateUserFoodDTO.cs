namespace ClassLibrary.DTOs.UserFoodDTOs
{
    public class CreateUserFoodDTO
    {
        public int UserId { get; set; }

        public string FoodName { get; set; } = null!;

        public decimal ProteinPer100g { get; set; }

        public decimal CarbsPer100g { get; set; }

        public decimal FatPer100g { get; set; }
    }
}
