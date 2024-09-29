
using M.Blog.BLL.DTOs.PostDTO;
using M.Blog.BLL.Interfaces;
using M.Blog.DAL.Entities;
using M.Blog.DAL.Interfaces;

namespace M.Blog.BLL.Services
{
    internal class PostService(IUnitOfWork uow) : IPostService
    {
        public async Task CreateAsync(NewPostDTO dto)
        {
            Post post = new()
            {
                Title = dto.Title,
                Content = dto.Content,
                Created = DateTime.Now,
            };

            await uow.PostRepository.Create(post);
            var user = await uow.UserRepository.GetBy(u => u.Id == dto.AuthorId);
            user.Posts.Add(post);

            await uow.Save();
        }

        public async Task<PostDTO> GetAsync(int id)
        {
            var post = await uow.PostRepository.GetBy(p => p.Id == id);
            var postDTO = new PostDTO
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                Author = (await uow.UserRepository.GetBy(u => u.Posts.Contains(post))).Name
            };
            return postDTO;
        }

        public async Task<IEnumerable<PostDTO>> GetAllAsync()
        {
            
            List<PostDTO> dtos = new();
            var posts = await uow.PostRepository.GetAll();
            foreach (var post in posts) 
            {
                dtos.Add(await GetAsync(post.Id));
            }
            return dtos;
        }
        
        public async Task DeleteAsync(int id)
        {
            var post= await uow.PostRepository.GetBy(p => p.Id == id);
            if (post == null)
                throw new Exception("Post not found");

            await uow.PostRepository.Delete(post);
            await uow.Save();
        }



        public async Task UpdateAsync(EditPostDTO dto)
        {
            var post = await uow.PostRepository.GetBy(p => p.Id == dto.PostId);
            if (post == null)
                throw new Exception("Post not found");

            post.Content = dto.NewContent;
            post.Updated = DateTime.Now;
            await uow.PostRepository.Update(post);
            await uow.Save();
        }
    }
}
