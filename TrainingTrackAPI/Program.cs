using ClassLibrary.JsonCustomConverters;
using FluentValidation;
using TrainingTrackAPI.Application.Interfaces.RepositoryInterfaces;
using TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.DietPlanServices;
using TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.FoodServices;
using TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.MacroService;
using TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.MealFoodServices;
using TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.MealServices;
using TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.UserFoodServices;
using TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.UserMealFoodServices;
using TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.UserServices;
using TrainingTrackAPI.Application.Interfaces.UnitOfWorkInterface;
using TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.DietPlansUseCaseInterfaces;
using TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.FoodsUseCaseInterfaces;
using TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.MealFoodsUseCaseInterfaces;
using TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.MealsUseCaseInterfaces;
using TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.UserFoodUseCaseInterfaces;
using TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.UserMealFoodUseCaseInterfaces;
using TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.UserUseCaseInterfaces;
using TrainingTrackAPI.Application.Services;
using TrainingTrackAPI.Application.Services.DietPlanServices;
using TrainingTrackAPI.Application.Services.FoodsServices;
using TrainingTrackAPI.Application.Services.MealFoodServices;
using TrainingTrackAPI.Application.Services.MealServices;
using TrainingTrackAPI.Application.Services.UserFoodServices;
using TrainingTrackAPI.Application.Services.UserMealFoodServices;
using TrainingTrackAPI.Application.Services.UserServices;
using TrainingTrackAPI.Application.UseCases.DietPlansUseCases;
using TrainingTrackAPI.Application.UseCases.FoodUseCases;
using TrainingTrackAPI.Application.UseCases.MealFoodUseCases;
using TrainingTrackAPI.Application.UseCases.MealUseCases;
using TrainingTrackAPI.Application.UseCases.UserFoodUseCases;
using TrainingTrackAPI.Application.UseCases.UserMealFoodUseCases;
using TrainingTrackAPI.Application.UseCases.UserUseCases;
using TrainingTrackAPI.Infrastructure.Data.Respositories.RespositoryImplementations;
using TrainingTrackAPI.Infrastructure.DbContexts;
using TrainingTrackAPI.Infrastructure.Persistance;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new CustomTimeOnlyConverter());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TrainingTrackDbContext>();

//Use Cases

//User use cases
builder.Services.AddScoped<IGetAllUsersUseCase, GetAllUsersUseCase>();
builder.Services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
builder.Services.AddScoped<ILoginUserUseCase, LoginUserUseCase>();
builder.Services.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();
builder.Services.AddScoped<IDeactivateUserUseCase, DeactivateUserUseCase>();


//Foods use cases
builder.Services.AddScoped<IGetAllFoodsUseCase, GetAllFoodsUseCase>();
builder.Services.AddScoped<IGetFoodUseCase, GetFoodUseCase>();

//Diet Plans use cases
builder.Services.AddScoped<IGetAllDietPlansUseCase, GetAllDietPlansUseCase>();
builder.Services.AddScoped<IGetDietPlanUseCase, GetDietPlanUseCase>();
builder.Services.AddScoped<ICreateDietPlanUseCase, CreateDietPlanUseCase>();
builder.Services.AddScoped<IUpdateDietPlanUseCase, UpdateDietPlanUseCase>();
builder.Services.AddScoped<IDisableDietPlanUseCase, DisableDietPlanUseCase>();

//Meals use cases
builder.Services.AddScoped<IGetAllMealsUseCase, GetAllMealsUseCase>();
builder.Services.AddScoped<IGetMealUseCase, GetMealUseCase>();
builder.Services.AddScoped<ICreateMealUseCase, CreateMealUseCase>();
builder.Services.AddScoped<IUpdateMealUseCase, UpdateMealUseCase>();
builder.Services.AddScoped<IDeleteMealUseCase, DeleteMealUseCase>();

//MealFoods use cases
builder.Services.AddScoped<IGetAllMealFoodsUseCase, GetAllMealFoodsUseCase>();
builder.Services.AddScoped<IGetMealFoodUseCase, GetMealFoodUseCase>();
builder.Services.AddScoped<ICreateMealFoodUseCase, CreateMealFoodUseCase>();
builder.Services.AddScoped<IUpdateMealFoodUseCase, UpdateMealFoodUseCase>();
builder.Services.AddScoped<IDeleteMealFoodUseCase, DeleteMealFoodUseCase>();

//UserFoods use cases
builder.Services.AddScoped<IGetAllUserFoodsUseCase, GetAllUserFoodsUseCase>();
builder.Services.AddScoped<IGetUserFoodUseCase, GetUserFoodUseCase>();
builder.Services.AddScoped<ICreateUserFoodUseCase, CreateUserFoodUseCase>();
builder.Services.AddScoped<IUpdateUserFoodUseCase, UpdateUserFoodUseCase>();
builder.Services.AddScoped<IDeleteUserFoodUseCase, DeleteUserFoodUseCase>();

//UserMealFoods use cases
builder.Services.AddScoped<IGetAllUserMealFoodsUseCase, GetAllUserMealFoodsUseCase>();
builder.Services.AddScoped<IGetUserMealFoodUseCase, GetUserMealFoodUseCase>();
builder.Services.AddScoped<ICreateUserMealFoodUseCase, CreateUserMealFoodUseCase>();
builder.Services.AddScoped<IUpdateUserMealFoodUseCase, UpdateUserMealFoodUseCase>();
builder.Services.AddScoped<IDeleteUserMealFoodUseCase, DeleteUserMealFoodUseCase>();

//Services

//User services

builder.Services.AddScoped<IGetAllUsersService, GetAllUsersService>();
builder.Services.AddScoped<IRegisterUserService, RegisterUserService>();
builder.Services.AddScoped<IEncryptionService, EncryptionService>();
builder.Services.AddScoped<RegisterUserValidationService>();
builder.Services.AddScoped<ILoginUserService, LoginUserService>();
builder.Services.AddScoped<IVerifyPasswordService, VerifyPasswordService>();
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
builder.Services.AddScoped<IUpdateUserService, UpdateUserService>();
builder.Services.AddScoped<UpdateUserValidationService>();
builder.Services.AddScoped<IDeactivateUserService, DeactivateUserService>();
builder.Services.AddScoped<DeactivateUserValidationService>();      


//Foods services
builder.Services.AddScoped<IGetAllFoodsService, GetAllFoodsService>();
builder.Services.AddScoped<IGetFoodService, GetFoodService>();


//Diet Plans services
builder.Services.AddScoped<IGetAllDietPlansService, GetAllDietPlansService>();
builder.Services.AddScoped<IGetDietPlanService, GetDietPlanService>();
builder.Services.AddScoped<ICreateDietPlanService, CreateDietPlanService>();
builder.Services.AddScoped<DietPlanValidationService>();
builder.Services.AddScoped<IUpdateDietPlanService, UpdateDietPlanService>();
builder.Services.AddScoped<UpdateDietPlanValidationService>();
builder.Services.AddScoped<IDisableDietPlanService, DisableDietPlanService>();

//Meals services
builder.Services.AddScoped<IGetAllMealsService, GetAllMealsService>();
builder.Services.AddScoped<IGetMealService, GetMealService>();
builder.Services.AddScoped<ICreateMealService, CreateMealService>();
builder.Services.AddScoped<CreateMealValidationService>();
builder.Services.AddScoped<IUpdateMealService, UpdateMealService>();
builder.Services.AddScoped<UpdateMealValidationService>();
builder.Services.AddScoped<IDeleteMealService, DeleteMealService>();

//MealFoods services
builder.Services.AddScoped<IGetAllMealFoodsService, GetAllMealFoodsService>();
builder.Services.AddScoped<IGetMealFoodService, GetMealFoodService>();
builder.Services.AddScoped<ICreateMealFoodService, CreateMealFoodService>();
builder.Services.AddScoped<CreateMealFoodValidationService>();
builder.Services.AddScoped<IUpdateMealFoodService, UpdateMealFoodService>();
builder.Services.AddScoped<UpdateMealFoodValidationService>();
builder.Services.AddScoped<IDeleteMealFoodService, DeleteMealFoodService>();

//UserFoods services
builder.Services.AddScoped<IGetAllUserFoodsService, GetAllUserFoodsService>();
builder.Services.AddScoped<IGetUserFoodService, GetUserFoodService>();
builder.Services.AddScoped<ICreateUserFoodService, CreateUserFoodService>();
builder.Services.AddScoped<CreateUserFoodValidationService>();
builder.Services.AddScoped<IUpdateUserFoodService, UpdateUserFoodService>();
builder.Services.AddScoped<UpdateUserFoodValidationService>();
builder.Services.AddScoped<IDeleteUserFoodService, DeleteUserFoodService>();

//UserMealFoods services
builder.Services.AddScoped<IGetAllUserMealFoodsService, GetAllUserMealFoodsService>();
builder.Services.AddScoped<IGetUserMealFoodService, GetUserMealFoodService>();
builder.Services.AddScoped<ICreateUserMealFoodService, CreateUserMealFoodService>();
builder.Services.AddScoped<CreateUserMealFoodValidationService>();
builder.Services.AddScoped<IUpdateUserMealFoodService, UpdateUserMealFoodService>();
builder.Services.AddScoped<UpdateUserMealFoodValidationService>();
builder.Services.AddScoped<IDeleteUserMealFoodService, DeleteUserMealFoodService>();

//Calculate Macros service
builder.Services.AddScoped<ICalculateMacrosService, CalculateMacrosService>();

//Calculate total calories and macros service
builder.Services.AddScoped<ICalculateTotalCaloriesAndMacrosService, CalculateTotalCaloriesAndMacrosService>();

//Calculate macros to calories service
builder.Services.AddScoped<ICalculateMacrosToCaloriesService, CalculateMacrosToCaloriesService>();

//Repos

//generic
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

//readonly
builder.Services.AddScoped(typeof(IReadOnlyRepository<>), typeof(ReadOnlyRepository<>));

//user
builder.Services.AddScoped<IUsersRepository, UsersRepository>();

//food
builder.Services.AddScoped<IFoodsRepository, FoodsRepository>();
//diet plan
builder.Services.AddScoped<IDietPlansRepository, DietPlansRepository>();

//meals
builder.Services.AddScoped<IMealsRepository, MealsRepository>();
//meal foods
builder.Services.AddScoped<IMealFoodsRepository, MealFoodsRepository>();
//user meal foods
builder.Services.AddScoped<IUserMealFoodsRepository, UserMealFoodsRepository>();
//user foods
builder.Services.AddScoped<IUserFoodsRepository, UserFoodsRepository>();

//Unit of work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
