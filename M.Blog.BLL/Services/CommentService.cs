using M.Blog.BLL.DTOs.CommentDTO;
using M.Blog.BLL.Interfaces;
using M.Blog.DAL.Entities;
using M.Blog.DAL.Interfaces;

namespace M.Blog.BLL.Services
{
    internal class CommentService(IUnitOfWork uow) : ICommentService
    {
        public async Task CreateAsync(NewCommentDTO dto)
        {
            var post = await uow.PostRepository.GetBy(p => p.Id == dto.PostId);
            var user = await uow.UserRepository.GetBy(u => u.Id == dto.AuthorId);
            
            Comment comment = new()
            { 
                Content = dto.Content,
                CreatedDate = DateTime.Now,
                Post = post,
                User = user
            };
            
            await uow.CommentRepository.Create(comment);
            await uow.Save();
        }

        public async Task DeleteAsync(int id)
        {
            var comment = await uow.CommentRepository.GetBy(c => c.Id == id);
            await uow.CommentRepository.Delete(comment);
            await uow.Save();
        }

        public async Task<IEnumerable<CommentDTO>> GetAllAsync()
        {
            List<CommentDTO> dtos = new();
            var comments = await uow.CommentRepository.GetAll();

            foreach (var comment in comments)
            {
                dtos.Add(await GetAsync(comment.Id));
            }
            return dtos;
        }

        public async Task<CommentDTO> GetAsync(int id)
        {
            var comment = await uow.CommentRepository.GetBy(c => c.Id == id);
            return new()
                    {
                        Id = comment.Id,
                        Content = comment.Content,
                        CreatedDate = comment.CreatedDate,
                        AuthorName = (await uow.UserRepository.GetBy(u => u.Comments.Contains(comment))).Name,
                        PostTitle = (await uow.PostRepository.GetBy(p => p.Comments.Contains(comment))).Title
                    };
        }

        public async Task UpdateAsync(UpdateCommentDTO dto)
        {
            var comment = await uow.CommentRepository.GetBy(c => c.Id ==dto.Id);
            comment.Content = dto.NewContent;

            await uow.CommentRepository.Update(comment);
            await uow.Save();
        }
    }

}
