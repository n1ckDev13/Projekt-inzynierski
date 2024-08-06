using ClassLibrary.DTOs.UserDTOs;
using ClassLibrary.Responses.User;
using TrainingTrackAPI.Application.Interfaces.RepositoryInterfaces;
using TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.UserUseCaseInterfaces;

namespace TrainingTrackAPI.Application.UseCases.UserUseCases
{
    public class GetAllUsersUseCase : IGetAllUsersUseCase
    {
        private readonly IUsersRepository _repo;

        public GetAllUsersUseCase(IUsersRepository repo)
        {
            _repo = repo;
        }

        public async Task<GetAllUsersResponse> GetAllUsersAsync()
        {
            try
            {
                var userList = await _repo.GetAllAsync();

                if (userList is null)
                    return new GetAllUsersResponse(false, "No data returned.", null, null);

                var userDTOs = userList.Select(user => new GetAllUsersDTO
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    UserMail = user.UserMail,
                    Password = user.Password,
                    IsActive = user.IsActive,
                    ProfilePicture = user.ProfilePicture
                }).ToList();

                return new GetAllUsersResponse(true, "Data returned.", userDTOs, null);
            }
            catch (Exception e) 
            {
                var errors = new List<string>();
                errors.Add(e.Message);
                return new GetAllUsersResponse(false, "Database error.", null, errors);
            }
        }
    }
}
