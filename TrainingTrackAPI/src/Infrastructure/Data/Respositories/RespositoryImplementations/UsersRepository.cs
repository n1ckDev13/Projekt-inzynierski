using ClassLibrary.DTOs.UserDTOs;
using Microsoft.EntityFrameworkCore;
using TrainingTrackAPI.Application.Interfaces.RepositoryInterfaces;
using TrainingTrackAPI.Domain.Entities;
using TrainingTrackAPI.Infrastructure.DbContexts;

namespace TrainingTrackAPI.Infrastructure.Data.Respositories.RespositoryImplementations
{
    public class UsersRepository : Repository<User>, IUsersRepository
    {

        public UsersRepository(TrainingTrackDbContext context) : base(context) 
        { 
            
        }

        public async Task<User> CheckIfAccountExists(string userMail)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.UserMail == userMail);
        }

        public async Task<bool> CheckIfMailIsUsed(string userMail)
        {
            return await _context.Users.AnyAsync(u => u.UserMail == userMail);
        }

        public async Task<User> CheckIfUserExists(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task DeactivateUserAsync(int id)
        {
            var user = await this.CheckIfUserExists(id);

            user.IsActive = false;
        }

        public async Task<NewUserTokenDataDTO> UpdateUserDataAsync(int id, UpdateUserDTO updateUserDTO)
        {
            var user = await this.CheckIfUserExists(id);

            user.UserName = updateUserDTO.UserName;
            user.UserMail = updateUserDTO.UserEmail;

            return new NewUserTokenDataDTO((user.Id).ToString(), user.UserName, user.UserMail);
            
        }

        public async Task<NewUserTokenDataDTO> UpdateUserPasswordAsync(int id, UpdateUserDTO updateUserDTO)
        {
            var user = await this.CheckIfUserExists(id);

            user.Password = updateUserDTO.NewPassword;

            return new NewUserTokenDataDTO((user.Id).ToString(), user.UserName, user.UserMail);
        }
    }
}
