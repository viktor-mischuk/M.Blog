using M.Blog.BLL.DTOs.RoleDTO;
using M.Blog.BLL.Interfaces;
using M.Blog.DAL.Entities;
using M.Blog.DAL.Interfaces;

namespace M.Blog.BLL.Services
{
    internal class RoleService(IUnitOfWork uow) : IRoleService
    {
        public async Task CreateAsync(NewRoleDTO dto)
        {
            Role role = new() 
            {
                Name = dto.Name,
                Description = dto.Description,
            };
            await uow.RoleRepository.Create(role);
            await uow.Save();
        }

        public async Task DeleteAsync(int id)
        {
            var role = await uow.RoleRepository.GetBy(r => r.Id == id);
            if (role == null)
                throw new Exception("User not found");

            await uow.RoleRepository.Delete(role);
            await uow.Save();
        }

        public async Task<IEnumerable<RoleDTO>> GetAllAsync()
        {
            List<RoleDTO> dtos = new();
            var roles = await uow.RoleRepository.GetAll();

            foreach (var role in roles)
            {
                dtos.Add(await GetAsync(role.Id));
            }
            return dtos;
        }

        public async Task<RoleDTO> GetAsync(int id)
        {
            var role = await uow.RoleRepository.GetBy(r => r.Id == id);

            if (role == null)
                throw new Exception("User not found");


            RoleDTO dto = new()
            {
                Id = role.Id,
                Name = role.Name,
                Description= role.Description,
            };
            return dto;
        }

        public async Task<RoleDTO> GetByNameAsync(string name)
        {
            var role = await uow.RoleRepository.GetBy(r => r.Name == name);

            if (role == null)
                throw new Exception("User not found");


            RoleDTO dto = new()
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description,
            };
            return dto;
        }

        public async Task UpdateAsync(EditRoleDTO dto)
        {
            var role = await uow.RoleRepository.GetBy(r => r.Id == dto.Id);
            if (role == null)
                throw new Exception("Role not found");

            if (dto.Name != null)
                role.Name = dto.Name;
            if (dto.Description != null)
                role.Description = dto.Description;

            await uow.RoleRepository.Update(role);
            await uow.Save();
        }
    }
}
