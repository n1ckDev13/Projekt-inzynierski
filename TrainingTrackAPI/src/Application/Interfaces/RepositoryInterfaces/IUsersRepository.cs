using ClassLibrary.DTOs.UserDTOs;
using TrainingTrackAPI.Domain.Entities;

namespace TrainingTrackAPI.Application.Interfaces.RepositoryInterfaces
{
    public interface IUsersRepository : IRepository<User>
    {
        Task<bool> CheckIfMailIsUsed(string userMail);
        Task<User> CheckIfAccountExists(string userMail);
        Task<User> CheckIfUserExists(int id);
        Task<NewUserTokenDataDTO> UpdateUserDataAsync(int id, UpdateUserDTO updateUserDTO);
        Task<NewUserTokenDataDTO> UpdateUserPasswordAsync(int id, UpdateUserDTO updateUserDTO);
        Task DeactivateUserAsync(int id);
    }
}
