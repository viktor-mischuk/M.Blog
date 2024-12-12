
using M.Blog.BLL.DTOs.TagDTO;
using M.Blog.BLL.Interfaces;
using M.Blog.DAL.Entities;
using M.Blog.DAL.Interfaces;

namespace M.Blog.BLL.Services
{
    internal class TagService(IUnitOfWork uow) : ITagService
    {
        public async Task CreateAsync(string name)
        {
            Tag tag = new() { Name = name};
            await uow.TagRepository.Create(tag);
            await uow.Save();
        }

        public async Task DeleteAsync(int id)
        {
            var tag = await uow.TagRepository.GetBy(t => t.Id == id);
            await uow.TagRepository.Delete(tag);
            await uow.Save();
        }

        public async Task<IEnumerable<TagDTO>> GetAllAsync()
        {
            List<TagDTO> dtos = new();
            var tags = await uow.TagRepository.GetAll();

            foreach (var tag in tags)
            {
                dtos.Add(await GetAsync(tag.Id));
            }
            return dtos;
        }

        public async Task<TagDTO> GetAsync(int id)
        {
            var tag = await uow.TagRepository.GetBy(t => t.Id == id);
            return new()
            {
                Id = tag.Id,
                Name = tag.Name,
            };
        }

        public async Task UpdateAsync(UpdateTagDTO dto)
        {
            var tag = await uow.TagRepository.GetBy(t => t.Id == dto.Id);
            tag.Name = dto.NewName;
            await uow.TagRepository.Update(tag);
            await uow.Save();
        }
    }
}
