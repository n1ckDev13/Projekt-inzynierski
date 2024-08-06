using Newtonsoft.Json.Linq;
using RestSharp;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using ClassLibrary.DTOs.DietPlanDTO;
using ClassLibrary.DTOs.FoodDTOs;
using ClassLibrary.DTOs.MealDTO;
using ClassLibrary.DTOs.MealFoodsDTOs;
using ClassLibrary.DTOs.UserDTOs;
using ClassLibrary.DTOs.UserFoodDTOs;
using ClassLibrary.DTOs.UserMealFoodDTOs;
using ClassLibrary.Responses.DietPlan;
using ClassLibrary.Responses.Meal;
using ClassLibrary.Responses.MealFood;
using ClassLibrary.Responses.User;
using ClassLibrary.Responses.UserFood;
using ClassLibrary.Responses.UserMealFood;
using Microsoft.IdentityModel.Tokens;

namespace ApiServiceAccess
{
    // All the code in this file is included in all platforms.
    public interface IApiAccess
    {
        event EventHandler UnauthorizedRequest;
        string GetAuthToken();
        void SetAuthToken(string token);
        bool ValidateToken(string token);
        UserCredentialsDTO GetUserCredentials();
        


        //Food
        Task<List<GetAllFoodsDTO>> GetAllFoods();
        Task<GetAllFoodsDTO> GetFood(int id);

        //User
        UserCredentialsDTO GetUserDataFromToken(string token);
        Task<LoginUserResponse> LoginUser(LoginUserDTO details);
        Task<RegisterUserResponse> RegisterUser(RegisterUserDTO details);
        Task<UpdateUserResponse> UpdateUserData(UpdateUserDTO details);
        Task<DeactivateUserResponse> DeactivateUser(DeactivateUserDTO details);

        //DietPlan
        Task<List<GetAllDietPlansDTO>> GetAllDietPlans(int userId);
        Task<GetAllDietPlansDTO> GetDietPlan(int planId);
        Task<CreateDietPlanResponse> CreateDietPlan(CreateDietPlanDTO details);
        Task<UpdateDietPlanResponse> UpdateDietPlan(UpdateDietPlanDTO details);
        Task<DisableDietPlanResponse> DisableDietPlan(int dietPlanId);

        //Meal
        Task<List<GetAllMealsDTO>> GetAllMeals(int dietPlanId);
        Task<GetAllMealsDTO> GetMeal(int mealId);
        Task<CreateMealResponse> CreateMeal(CreateMealDTO details);
        Task<UpdateMealResponse> UpdateMeal(UpdateMealDTO details);
        Task<DeleteMealResponse> DeleteMeal(int mealId);

        //MealFood
        Task<List<GetAllMealFoodsDTO>> GetAllMealFoods(int mealId);
        Task<GetAllMealFoodsDTO> GetMealFood(int mealId);
        Task<CreateMealFoodResponse> CreateMealFood(CreateMealFoodDTO details);
        Task<UpdateMealFoodResponse> UpdateMealFood(UpdateMealFoodDTO details);
        Task<DeleteMealFoodResponse> DeleteMealFood(int Id);

        //UserMealFood
        Task<List<GetAllUserMealFoodDTO>> GetAllUserMealFoods(int mealId);
        Task<GetAllUserMealFoodDTO> GetUserMealFood(int Id);
        Task<CreateUserMealFoodResponse> CreateUserMealFood(CreateUserMealFoodDTO details);
        Task<UpdateUserMealFoodResponse> UpdateUserMealFood(UpdateUserMealFoodDTO details);
        Task<DeleteUserMealFoodResponse> DeleteUserMealFood(int Id);

        //UserFoods
        Task<List<GetAllUserFoodDTO>> GetAllUserFoods(int userId);
        Task<GetAllUserFoodDTO> GetUserFood(int Id);
        Task<CreateUserFoodResponse> CreateUserFood(CreateUserFoodDTO details);
        Task<UpdateUserFoodResponse> UpdateUserFood(UpdateUserFoodDTO details);
        Task<DeleteUserFoodResponse> DeleteUserFood(int Id);

    }

    public class ApiAccess : IApiAccess
    {
        private const string baseUrl = "http://10.0.2.2:5042/api";
        private const string FoodEndpoint = "Food";
        private const string UserEndpoint = "User";
        private const string DietPlanEndpoint = "DietPlan";
        private const string MealEndpoint = "Meals";
        private const string MealFoodEndpoint = "MealFoods";
        private const string UserMealFoodEndpoint = "UserMealFoods";
        private const string UserFoodEndpoint = "UserFoods";

        public event EventHandler UnauthorizedRequest;
        public UserCredentialsDTO UserCredentials;

        private const string secretKey = "2C6A9B2CA6F45E0EF7605F1BD7DABE5E4C53F36A6E443C6D4A1F9B5C3B02C1FA";

        public UserCredentialsDTO GetUserCredentials()
        {
            return this.UserCredentials;
        }
        
        private void OnUnauthorizedRequest()
        {
            UnauthorizedRequest?.Invoke(this, EventArgs.Empty);
        }

        public string GetAuthToken()
        {
            return Preferences.Get("AuthToken", defaultValue: null);
        }

        public void SetAuthToken(string token)
        {
            Preferences.Set("AuthToken", token);
        }

        public bool ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //Foods

        public async Task<List<GetAllFoodsDTO>> GetAllFoods()
        {
            var client = new RestClient(baseUrl);
            var request = new RestRequest($"{FoodEndpoint}/getAllFoods");

            try
            {
                if (!ValidateToken(GetAuthToken()))
                {
                    OnUnauthorizedRequest();
                    return null;
                }

                var response = await client.ExecuteAsync(request);

                if (response.IsSuccessful)
                {
                   
                    var content = response.Content;
                    var json = JToken.Parse(content);

                    if ((bool)json["isSuccess"] == false)
                    {

                        return null;
                    }

                    var foodList = (JArray)json["foodsList"];

                    var foodListDTO = foodList.ToObject<List<GetAllFoodsDTO>>();

                    return foodListDTO;
                }

               
            }
            catch (Exception e)
            {
                
                return null;
            }

            return null;
        }


        public async Task<GetAllFoodsDTO> GetFood(int id)
        {
            

            var client = new RestClient(baseUrl);
            var request = new RestRequest($"{FoodEndpoint}/getFood?id={id}");

            try
            {
                if (!ValidateToken(GetAuthToken()))
                {
                    OnUnauthorizedRequest();
                    return null;
                }

                var response = await client.ExecuteAsync(request);

                if (response.IsSuccessful)
                {
                   
                    var content = response.Content;
                    var json = JToken.Parse(content);


                    if ((bool)json["isSuccess"] == false)
                    {

                        return null;
                    }

                    
                    var food = (JObject)json["food"];

                    
                    var foodDTO = food.ToObject<GetAllFoodsDTO>();

                    return foodDTO;
                }


            }
            catch (Exception e)
            {
                return null;
            }

            return null;
        }

        //User

        public UserCredentialsDTO GetUserDataFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            var idClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "nameid")?.Value;
            var userNameClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "unique_name")?.Value;
            var userMailClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "email")?.Value;

            return new UserCredentialsDTO
            {
                Id = int.Parse(idClaim),
                UserName = userNameClaim,
                UserMail = userMailClaim
            };
        }

        public async Task<LoginUserResponse> LoginUser(LoginUserDTO details)
        {
            

            var client = new RestClient(baseUrl);
            var request = new RestRequest($"{UserEndpoint}/loginUser");

            try
            {

                request.AddJsonBody(details);

                var response = await client.ExecutePostAsync(request);


                

                var loginUserResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<LoginUserResponse>(response.Content);

                if (loginUserResponse.isSuccess)
                {
                    var jsonResponse = JObject.Parse(response.Content);
                    var token = jsonResponse["token"]?.ToString();

                    SetAuthToken(token);

                    UserCredentials = GetUserDataFromToken(GetAuthToken());
                }


                return loginUserResponse;



            }
            catch (Exception e)
            {
                
                return null;
            }

            return null;
        }

        public async Task<RegisterUserResponse> RegisterUser(RegisterUserDTO details)
        {
            

            var client = new RestClient(baseUrl);
            var request = new RestRequest($"{UserEndpoint}/registerUser");

            try
            {

                request.AddJsonBody(details);

                var response = await client.ExecutePostAsync(request);


                

                var registerUserResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<RegisterUserResponse>(response.Content);



                return registerUserResponse;



            }
            catch (Exception e)
            {
                
                return null;
            }

            return null;
        }

        public async Task<UpdateUserResponse> UpdateUserData(UpdateUserDTO details)
        {
            

            var client = new RestClient(baseUrl);
            var request = new RestRequest($"{UserEndpoint}/updateUserData");

            try
            {
                if (!ValidateToken(GetAuthToken()))
                {
                    OnUnauthorizedRequest();
                    return null;
                }

                request.AddJsonBody(details);

                var response = await client.ExecutePostAsync(request);


                

                var updateUserResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<UpdateUserResponse>(response.Content);

                if (updateUserResponse.isSuccess)
                {
                    var jsonResponse = JObject.Parse(response.Content);
                    var token = jsonResponse["token"]?.ToString();

                    SetAuthToken(token);

                    UserCredentials = GetUserDataFromToken(GetAuthToken());
                }

                return updateUserResponse;



            }
            catch (Exception e)
            {
                
                return null;
            }

            return null;
        }

        public async Task<DeactivateUserResponse> DeactivateUser(DeactivateUserDTO details)
        {

            var client = new RestClient(baseUrl);
            var request = new RestRequest($"{UserEndpoint}/deactivateUserAccount");

            try
            {

                if (!ValidateToken(GetAuthToken()))
                {
                    OnUnauthorizedRequest();
                    return null;
                }

                request.AddJsonBody(details);

                var response = await client.ExecutePutAsync(request);


                

                var deactivateUserResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<DeactivateUserResponse>(response.Content);



                return deactivateUserResponse;



            }
            catch (Exception e)
            {
               
                return null;
            }

            return null;
        }

        //DietPlan

        public async Task<List<GetAllDietPlansDTO>> GetAllDietPlans(int userId)
        {
            

            var client = new RestClient(baseUrl);
            var request = new RestRequest($"{DietPlanEndpoint}/getAllDietPlansForUser?userId={userId}");

            try
            {
                if (!ValidateToken(GetAuthToken()))
                {
                    OnUnauthorizedRequest();
                    return null;
                }

                var response = await client.ExecuteAsync(request);

                if (response.IsSuccessful)
                {
                    

                    
                    var content = response.Content;
                    var json = JToken.Parse(content);


                    if ((bool)json["isSuccess"] == false)
                    {

                        return null;
                    }

                    
                    var dietPlansList = (JArray)json["dietPlansList"];

                   
                    var dietPlansListDTO = dietPlansList.ToObject<List<GetAllDietPlansDTO>>();

                    return dietPlansListDTO;
                }


            }
            catch (Exception e)
            {
                
                return null;
            }

            return null;
        }


        public async Task<GetAllDietPlansDTO> GetDietPlan(int planId)
        {
            

            var client = new RestClient(baseUrl);
            var request = new RestRequest($"{DietPlanEndpoint}/getDietPlan?id={planId}");

            try
            {
                if (!ValidateToken(GetAuthToken()))
                {
                    OnUnauthorizedRequest();
                    return null;
                }

                var response = await client.ExecuteAsync(request);

                if (response.IsSuccessful)
                {
                    

                    
                    var content = response.Content;
                    var json = JToken.Parse(content);


                    if ((bool)json["isSuccess"] == false)
                    {

                        return null;
                    }

                    
                    var dietPlan = (JObject)json["dietPlan"];

                    
                    var dietPlanDTO = dietPlan.ToObject<GetAllDietPlansDTO>();

                    return dietPlanDTO;
                }


            }
            catch (Exception e)
            {
                
                return null;
            }

            return null;
        }

        public async Task<CreateDietPlanResponse> CreateDietPlan(CreateDietPlanDTO details)
        {
            

            var client = new RestClient(baseUrl);
            var request = new RestRequest($"{DietPlanEndpoint}/createDietPlan");

            try
            {

                if (!ValidateToken(GetAuthToken()))
                {
                    OnUnauthorizedRequest();
                    return null;
                }

                request.AddJsonBody(details);

                var response = await client.ExecutePostAsync(request);


                

                var createDietPlanResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateDietPlanResponse>(response.Content);



                return createDietPlanResponse;



            }
            catch (Exception e)
            {
                
                return null;
            }

            return null;
        }


        public async Task<UpdateDietPlanResponse> UpdateDietPlan(UpdateDietPlanDTO details)
        {
            

            var client = new RestClient(baseUrl);
            var request = new RestRequest($"{DietPlanEndpoint}/updateDietPlan");

            try
            {

                if (!ValidateToken(GetAuthToken()))
                {
                    OnUnauthorizedRequest();
                    return null;
                }

                request.AddJsonBody(details);

                var response = await client.ExecutePutAsync(request);


                

                var updateDietPlanResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<UpdateDietPlanResponse>(response.Content);



                return updateDietPlanResponse;



            }
            catch (Exception e)
            {
                
                return null;
            }

            return null;
        }

        public async Task<DisableDietPlanResponse> DisableDietPlan(int dietPlanId)
        {
            

            var client = new RestClient(baseUrl);
            var request = new RestRequest($"{DietPlanEndpoint}/disableDietPlan?id={dietPlanId}");

            try
            {

                if (!ValidateToken(GetAuthToken()))
                {
                    OnUnauthorizedRequest();
                    return null;
                }



                var response = await client.ExecutePutAsync(request);


                

                var disableDietPlanResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<DisableDietPlanResponse>(response.Content);



                return disableDietPlanResponse;



            }
            catch (Exception e)
            {
                
                return null;
            }

            return null;
        }

        //Meal

        public async Task<List<GetAllMealsDTO>> GetAllMeals(int dietPlanId)
        {
            

            var client = new RestClient(baseUrl);
            var request = new RestRequest($"{MealEndpoint}/getAllMealsForDietPlan?dietPlanId={dietPlanId}");

            try
            {
                if (!ValidateToken(GetAuthToken()))
                {
                    OnUnauthorizedRequest();
                    return null;
                }

                var response = await client.ExecuteAsync(request);

                if (response.IsSuccessful)
                {
                   
                    var json = JToken.Parse(response.Content);

                    if ((bool)json["isSuccess"] == false)
                    {
                        return null;
                    }

                    
                    var mealsList = json["mealList"] as JArray;

                    if (mealsList == null)
                    {
                        return new List<GetAllMealsDTO>();
                    }

                    
                    var mealsListDTO = new List<GetAllMealsDTO>();

                    foreach (var meal in mealsList)
                    {
                        var id = (int)meal["id"];
                        var planId = (int)meal["dietPlanId"];
                        var timeOfEatingObj = meal["timeOfEating"];
                        var hour = (int)timeOfEatingObj["hour"];
                        var minute = (int)timeOfEatingObj["minute"];
                        var timeOfEating = new TimeOnly(hour, minute);
                        var mealName = (string)meal["mealName"];

                        var mealDTO = new GetAllMealsDTO
                        {
                            Id = id,
                            DietPlanId = planId,
                            TimeOfEating = timeOfEating,
                            MealName = mealName
                        };

                        mealsListDTO.Add(mealDTO);
                    }

                    return mealsListDTO;
                }


            }
            catch (Exception e)
            {
                
                return null;
            }

            return null;
        }


        public async Task<GetAllMealsDTO> GetMeal(int mealId)
        {
            

            var client = new RestClient(baseUrl);
            var request = new RestRequest($"{MealEndpoint}/getMeal?id={mealId}");

            try
            {
                if (!ValidateToken(GetAuthToken()))
                {
                    OnUnauthorizedRequest();
                    return null;
                }

                var response = await client.ExecuteAsync(request);

                if (response.IsSuccessful)
                {
                   
                    var json = JToken.Parse(response.Content);

                    if ((bool)json["isSuccess"] == false)
                    {
                        return null;
                    }

                    
                    var mealToken = json["meal"] as JObject;

                    if (mealToken == null)
                    {
                        return null;
                    }

                    
                    var timeOfEatingToken = mealToken["timeOfEating"] as JObject;
                    TimeOnly timeOfEating = default;
                    if (timeOfEatingToken != null)
                    {
                        int hour = (int?)timeOfEatingToken["hour"] ?? 0;
                        int minute = (int?)timeOfEatingToken["minute"] ?? 0;
                        timeOfEating = new TimeOnly(hour, minute);
                    }

                    
                    var mealDTO = new GetAllMealsDTO
                    {
                        Id = (int)mealToken["id"],
                        DietPlanId = (int)mealToken["dietPlanId"],
                        MealName = (string)mealToken["mealName"],
                        TimeOfEating = timeOfEating
                    };

                    return mealDTO;
                    
                }


            }
            catch (Exception e)
            {
                
                return null;
            }

            return null;
        }


        public async Task<CreateMealResponse> CreateMeal(CreateMealDTO details)
        {
            

            var client = new RestClient(baseUrl);
            var request = new RestRequest($"{MealEndpoint}/createMeal");

            try
            {

                if (!ValidateToken(GetAuthToken()))
                {
                    OnUnauthorizedRequest();
                    return null;
                }

                request.AddJsonBody(details);

                var response = await client.ExecutePostAsync(request);


                

                var createMealResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateMealResponse>(response.Content);



                return createMealResponse;



            }
            catch (Exception e)
            {
                
                return null;
            }

            return null;
        }

        public async Task<UpdateMealResponse> UpdateMeal(UpdateMealDTO details)
        {
            

            var client = new RestClient(baseUrl);
            var request = new RestRequest($"{MealEndpoint}/updateMeal");

            try
            {

                if (!ValidateToken(GetAuthToken()))
                {
                    OnUnauthorizedRequest();
                    return null;
                }

                request.AddJsonBody(details);

                var response = await client.ExecutePutAsync(request);


                

                var updateMealResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<UpdateMealResponse>(response.Content);



                return updateMealResponse;



            }
            catch (Exception e)
            {
                
                return null;
            }

            return null;
        }


        public async Task<DeleteMealResponse> DeleteMeal(int mealId)
        {
            

            var client = new RestClient(baseUrl);
            var request = new RestRequest($"{MealEndpoint}/deleteMeal?id={mealId}");

            try
            {

                if (!ValidateToken(GetAuthToken()))
                {
                    OnUnauthorizedRequest();
                    return null;
                }



                var response = await client.ExecuteDeleteAsync(request);


                

                var deleteMealResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<DeleteMealResponse>(response.Content);



                return deleteMealResponse;



            }
            catch (Exception e)
            {
                
                return null;
            }

            return null;
        }

        //MealFood


        public async Task<List<GetAllMealFoodsDTO>> GetAllMealFoods(int mealId)
        {
            

            var client = new RestClient(baseUrl);
            var request = new RestRequest($"{MealFoodEndpoint}/getAllMealFoodsForMeal?mealId={mealId}");

            try
            {
                if (!ValidateToken(GetAuthToken()))
                {
                    OnUnauthorizedRequest();
                    return null;
                }

                var response = await client.ExecuteAsync(request);

                if (response.IsSuccessful)
                {
                   
                    var content = response.Content;
                    var json = JToken.Parse(content);


                    if ((bool)json["isSuccess"] == false)
                    {

                        return null;
                    }

                    
                    var mealFoodsList = (JArray)json["mealFoodsList"];

                    
                    var mealFoodsListDTO = mealFoodsList.ToObject<List<GetAllMealFoodsDTO>>();

                    return mealFoodsListDTO;
                }


            }
            catch (Exception e)
            {
                
                return null;
            }

            return null;
        }


        public async Task<GetAllMealFoodsDTO> GetMealFood(int Id)
        {
            

            var client = new RestClient(baseUrl);
            var request = new RestRequest($"{MealFoodEndpoint}/getMealFood?id={Id}");

            try
            {
                if (!ValidateToken(GetAuthToken()))
                {
                    OnUnauthorizedRequest();
                    return null;
                }

                var response = await client.ExecuteAsync(request);

                if (response.IsSuccessful)
                {
                    
                    var content = response.Content;
                    var json = JToken.Parse(content);


                    if ((bool)json["isSuccess"] == false)
                    {

                        return null;
                    }

                    
                    var mealFood = (JObject)json["mealFood"];

                    
                    var mealFoodDTO = mealFood.ToObject<GetAllMealFoodsDTO>();

                    return mealFoodDTO;
                }


            }
            catch (Exception e)
            {
                
                return null;
            }

            return null;
        }


        public async Task<CreateMealFoodResponse> CreateMealFood(CreateMealFoodDTO details)
        {
            

            var client = new RestClient(baseUrl);
            var request = new RestRequest($"{MealFoodEndpoint}/createMealFood");

            try
            {

                if (!ValidateToken(GetAuthToken()))
                {
                    OnUnauthorizedRequest();
                    return null;
                }

                request.AddJsonBody(details);

                var response = await client.ExecutePostAsync(request);


                

                var createMealFoodResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateMealFoodResponse>(response.Content);



                return createMealFoodResponse;



            }
            catch (Exception e)
            {
                
                return null;
            }

            return null;
        }


        public async Task<UpdateMealFoodResponse> UpdateMealFood(UpdateMealFoodDTO details)
        {
            

            var client = new RestClient(baseUrl);
            var request = new RestRequest($"{MealFoodEndpoint}/updateMealFood");

            try
            {

                if (!ValidateToken(GetAuthToken()))
                {
                    OnUnauthorizedRequest();
                    return null;
                }

                request.AddJsonBody(details);

                var response = await client.ExecutePutAsync(request);


                

                var updateMealFoodResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<UpdateMealFoodResponse>(response.Content);



                return updateMealFoodResponse;



            }
            catch (Exception e)
            {
                
                return null;
            }

            return null;
        }


        public async Task<DeleteMealFoodResponse> DeleteMealFood(int Id)
        {
            

            var client = new RestClient(baseUrl);
            var request = new RestRequest($"{MealFoodEndpoint}/deleteMealFood?id={Id}");

            try
            {

                if (!ValidateToken(GetAuthToken()))
                {
                    OnUnauthorizedRequest();
                    return null;
                }



                var response = await client.ExecuteDeleteAsync(request);


                

                var deleteMealFoodResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<DeleteMealFoodResponse>(response.Content);



                return deleteMealFoodResponse;



            }
            catch (Exception e)
            {
                
                return null;
            }

            return null;
        }


        //UserMealFood


        public async Task<List<GetAllUserMealFoodDTO>> GetAllUserMealFoods(int mealId)
        {
            

            var client = new RestClient(baseUrl);
            var request = new RestRequest($"{UserMealFoodEndpoint}/getAllUserMealFoodsForMeal?mealId={mealId}");

            try
            {
                if (!ValidateToken(GetAuthToken()))
                {
                    OnUnauthorizedRequest();
                    return null;
                }

                var response = await client.ExecuteAsync(request);

                if (response.IsSuccessful)
                {
                    
                    var content = response.Content;
                    var json = JToken.Parse(content);


                    if ((bool)json["isSuccess"] == false)
                    {

                        return null;
                    }

                    
                    var userMealFoodsList = (JArray)json["userMealFoodsList"];

                    
                    var userMealFoodsListDTO = userMealFoodsList.ToObject<List<GetAllUserMealFoodDTO>>();

                    return userMealFoodsListDTO;
                }


            }
            catch (Exception e)
            {
                
                return null;
            }

            return null;
        }


        public async Task<GetAllUserMealFoodDTO> GetUserMealFood(int Id)
        {
            

            var client = new RestClient(baseUrl);
            var request = new RestRequest($"{UserMealFoodEndpoint}/getUserMealFood?id={Id}");

            try
            {
                if (!ValidateToken(GetAuthToken()))
                {
                    OnUnauthorizedRequest();
                    return null;
                }

                var response = await client.ExecuteAsync(request);

                if (response.IsSuccessful)
                {
                    
                    var content = response.Content;
                    var json = JToken.Parse(content);


                    if ((bool)json["isSuccess"] == false)
                    {

                        return null;
                    }

                    
                    var userMealFood = (JObject)json["userMealFood"];

                    
                    var userMealFoodDTO = userMealFood.ToObject<GetAllUserMealFoodDTO>();

                    return userMealFoodDTO;
                }


            }
            catch (Exception e)
            {
                
                return null;
            }

            return null;
        }


        public async Task<CreateUserMealFoodResponse> CreateUserMealFood(CreateUserMealFoodDTO details)
        {
            

            var client = new RestClient(baseUrl);
            var request = new RestRequest($"{UserMealFoodEndpoint}/createUserMealFood");

            try
            {

                if (!ValidateToken(GetAuthToken()))
                {
                    OnUnauthorizedRequest();
                    return null;
                }

                request.AddJsonBody(details);

                var response = await client.ExecutePostAsync(request);


               

                var createUserMealFoodResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateUserMealFoodResponse>(response.Content);



                return createUserMealFoodResponse;



            }
            catch (Exception e)
            {
                
                return null;
            }

            return null;
        }


        public async Task<UpdateUserMealFoodResponse> UpdateUserMealFood(UpdateUserMealFoodDTO details)
        {
            

            var client = new RestClient(baseUrl);
            var request = new RestRequest($"{UserMealFoodEndpoint}/updateUserMealFood");

            try
            {

                if (!ValidateToken(GetAuthToken()))
                {
                    OnUnauthorizedRequest();
                    return null;
                }

                request.AddJsonBody(details);

                var response = await client.ExecutePutAsync(request);


                

                var updateUserMealFoodResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<UpdateUserMealFoodResponse>(response.Content);



                return updateUserMealFoodResponse;



            }
            catch (Exception e)
            {
                
                return null;
            }

            return null;
        }

        public async Task<DeleteUserMealFoodResponse> DeleteUserMealFood(int Id)
        {
            

            var client = new RestClient(baseUrl);
            var request = new RestRequest($"{UserMealFoodEndpoint}/deleteUserMealFood?id={Id}");

            try
            {
                if (!ValidateToken(GetAuthToken()))
                {
                    OnUnauthorizedRequest();
                    return null;
                }



                var response = await client.ExecuteDeleteAsync(request);


                

                var deleteUserMealFoodResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<DeleteUserMealFoodResponse>(response.Content);



                return deleteUserMealFoodResponse;



            }
            catch (Exception e)
            {
                
                return null;
            }

            return null;
        }



        //UserFoods


        public async Task<List<GetAllUserFoodDTO>> GetAllUserFoods(int userId)
        {
           

            var client = new RestClient(baseUrl);
            var request = new RestRequest($"{UserFoodEndpoint}/getAllUserFoodsForUser?userId={userId}");

            try
            {
                if (!ValidateToken(GetAuthToken()))
                {
                    OnUnauthorizedRequest();
                    return null;
                }

                var response = await client.ExecuteAsync(request);

                if (response.IsSuccessful)
                {
                   
                    var content = response.Content;
                    var json = JToken.Parse(content);


                    if ((bool)json["isSuccess"] == false)
                    {

                        return null;
                    }

                    
                    var userFoodsList = (JArray)json["userFoodList"];

                    
                    var userFoodsListDTO = userFoodsList.ToObject<List<GetAllUserFoodDTO>>();

                    return userFoodsListDTO;
                }


            }
            catch (Exception e)
            {
                
                return null;
            }

            return null;
        }

        public async Task<GetAllUserFoodDTO> GetUserFood(int Id)
        {
            

            var client = new RestClient(baseUrl);
            var request = new RestRequest($"{UserFoodEndpoint}/getUserFood?id={Id}");

            try
            {
                if (!ValidateToken(GetAuthToken()))
                {
                    OnUnauthorizedRequest();
                    return null;
                }

                var response = await client.ExecuteAsync(request);

                if (response.IsSuccessful)
                {
                    
                    var content = response.Content;
                    var json = JToken.Parse(content);


                    if ((bool)json["isSuccess"] == false)
                    {

                        return null;
                    }

                    
                    var userFood = (JObject)json["userFood"];

                    
                    var userFoodDTO = userFood.ToObject<GetAllUserFoodDTO>();

                    return userFoodDTO;
                }


            }
            catch (Exception e)
            {
                
                return null;
            }

            return null;
        }

        public async Task<CreateUserFoodResponse> CreateUserFood(CreateUserFoodDTO details)
        {
            

            var client = new RestClient(baseUrl);
            var request = new RestRequest($"{UserFoodEndpoint}/createUserFood");

            try
            {

                if (!ValidateToken(GetAuthToken()))
                {
                    OnUnauthorizedRequest();
                    return null;
                }

                request.AddJsonBody(details);

                var response = await client.ExecutePostAsync(request);


                

                var createUserFoodResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateUserFoodResponse>(response.Content);



                return createUserFoodResponse;



            }
            catch (Exception e)
            {
                
                return null;
            }

            return null;
        }

        public async Task<UpdateUserFoodResponse> UpdateUserFood(UpdateUserFoodDTO details)
        {
            

            var client = new RestClient(baseUrl);
            var request = new RestRequest($"{UserFoodEndpoint}/updateUserFood");

            try
            {

                if (!ValidateToken(GetAuthToken()))
                {
                    OnUnauthorizedRequest();
                    return null;
                }

                request.AddJsonBody(details);

                var response = await client.ExecutePutAsync(request);


                

                var updateUserFoodResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<UpdateUserFoodResponse>(response.Content);



                return updateUserFoodResponse;



            }
            catch (Exception e)
            {
                
                return null;
            }

            return null;
        }


        public async Task<DeleteUserFoodResponse> DeleteUserFood(int Id)
        {
            

            var client = new RestClient(baseUrl);
            var request = new RestRequest($"{UserFoodEndpoint}/deleteUserFood?id={Id}");

            try
            {

                if (!ValidateToken(GetAuthToken()))
                {
                    OnUnauthorizedRequest();
                    return null;
                }



                var response = await client.ExecuteDeleteAsync(request);


                

                var deleteUserFoodResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<DeleteUserFoodResponse>(response.Content);



                return deleteUserFoodResponse;



            }
            catch (Exception e)
            {
                
                return null;
            }

            return null;
        }












    }


}
