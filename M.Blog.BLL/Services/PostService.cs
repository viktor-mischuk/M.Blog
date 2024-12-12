
using M.Blog.BLL.DTOs.CommentDTO;
using M.Blog.BLL.DTOs.PostDTO;
using M.Blog.BLL.DTOs.TagDTO;
using M.Blog.BLL.Interfaces;
using M.Blog.DAL.Entities;
using M.Blog.DAL.Interfaces;

namespace M.Blog.BLL.Services
{
    internal class PostService(IUnitOfWork uow) : IPostService
    {
        public  async Task<List<string>> GetAllTags()
        { 
            var tags = await uow.TagRepository.GetAll();

            return tags.Select(s => s.Name).ToList(); ; 
        }
        
        public async Task CreateAsync(NewPostDTO dto)
        {
            Post post = new()
            {
                Title = dto.Title,
                Content = dto.Content,
                Created = DateTime.Now,
                
            };

            dto.Tags.ForEach(async s => 
                post.Tags.Add(
                    await uow.TagRepository.GetBy(t => t.Name == s)));

            //foreach (var tag in dto.Tags)
            //{
            //    post.Tags.Add(await uow.TagRepository.GetBy(t => t.Name == tag));
            //}


            await uow.PostRepository.Create(post);
            var user = await uow.UserRepository.GetBy(u => u.Email == dto.Email);
 
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
        public async Task<PostDTO> GetByTitleAsync(string title)
        {
            var post = await uow.PostRepository.GetBy(p => p.Title == title);
            var postDTO = new PostDTO
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                Author = (await uow.UserRepository.GetBy(u => u.Posts.Contains(post))).Name,
            };
            var tags = await uow.TagRepository.GetManyBy(tag => tag.Posts.Contains(post));
            var comments = (await uow.CommentRepository.GetManyBy(c => c.Post ==post )).Select(s => s).ToList();

            List<CommentDTO> commentsDTOs = new List<CommentDTO>();
            foreach (var comment in comments) 
            {
                var dto = new CommentDTO()
                {
                    Id = comment.Id,
                    Content = comment.Content,
                    AuthorName = (await uow.UserRepository.GetBy(u => u.Comments.Contains(comment))).Name,
                };
                commentsDTOs.Add(dto);
            }

            postDTO.Tags = tags.Select(s => s.Name).ToList();
            postDTO.Comments = commentsDTOs;
            return postDTO;
        }

        public async Task<IEnumerable<PostDTO>> GetAllAsync()
        {
            
            List<PostDTO> dtos = new();
            var posts = await uow.PostRepository.GetAll();

            
            foreach (var post in posts) 
            {
                var tags = await uow.TagRepository.GetManyBy(tag => tag.Posts.Contains(post));
                var dto = await GetAsync(post.Id);
                dto.Tags = tags.Select(s => s.Name).ToList();
                dtos.Add(dto);
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
