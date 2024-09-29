using M.Blog.BLL.DTOs.UserDTO;
using M.Blog.BLL.Interfaces;
using M.Blog.DAL.Entities;
using M.Blog.DAL.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace M.Blog.BLL.Services
{
    internal class UserService(IUnitOfWork uow, JWTService jwtService) : IUserService
    {
        public async Task CreateAsync(NewUserDTO dto)
        {
            User user = new()
            {
                Name = dto.UserName,
                Email = dto.Email,

            };
            var passHash = new PasswordHasher<User>().HashPassword(user, dto.Password);
            user.PasswordHash = passHash;
            var role = await uow.RoleRepository.GetBy(role => role.Name == "User");
            user.Roles.Add(role);

            await uow.UserRepository.Create(user);
            await uow.Save();
        }
        public async Task DeleteAsync(int id)
        {
            var user = await uow.UserRepository.GetBy(user => user.Id == id);
            if (user == null)
                throw new Exception("User not found");

            await uow.UserRepository.Delete(user);
            await uow.Save();
        }


        public async Task UpdateAsync(UpdateUserDTO dto)
        {
            var user = await uow.UserRepository.GetBy(user => user.Id == dto.Id);
            if (user == null)
                throw new Exception("User not found");

            if(dto.NewUserEmail != null) 
                user.Email = dto.NewUserEmail;
            if(dto.NewUserName != null) 
                user.Name = dto.NewUserName;
            if(dto.NewUserPassword != null)
                user.PasswordHash = new PasswordHasher<User>().HashPassword(user, dto.NewUserPassword);

            await uow.UserRepository.Update(user);
            await uow.Save();
        }
        public async Task<string> LoginAsync(string email, string passWord)
        {
            var user = await uow.UserRepository.GetBy(user => user.Email == email);

            var userRoles = await uow.RoleRepository.GetManyBy(role => role.Users.Contains(user));
            user.Roles.AddRange(userRoles);


            var result = new PasswordHasher<User>()
                 .VerifyHashedPassword(user, user.PasswordHash, passWord);
            if (result == PasswordVerificationResult.Success)
            {

                return jwtService.GenerateToken(user);
            }
            else
            {
                throw new Exception("UnAuthorized");
            }
        }

        public async Task<UserDTO> GetAsync(int id)
        {
            var user = await uow.UserRepository.GetBy(user => user.Id == id);
            if (user == null)
                throw new Exception("User not found");

            UserDTO dto = new() 
            { 
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
            };
            return dto;
        }

        public async Task<IEnumerable<UserDTO>> GetAllAsync()
        {
            List<UserDTO> dtos = new();
            var users = await uow.UserRepository.GetAll();

            foreach (var user in users)
            {
                dtos.Add(await GetAsync(user.Id));
            }
            return dtos;
        }
    }
}
