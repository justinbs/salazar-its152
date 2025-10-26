using BlogDataLibrary.Models;

namespace BlogDataLibrary.Data
{
    public interface ISqlData1
    {
        Task CreatePostAsync(PostModel post);
        Task<PostModel?> GetPostAsync(int id);
        Task<IEnumerable<ListPostModel>> GetPostsAsync();
        Task<int> RegisterAsync(UserModel user);
        Task<int?> ValidateUserAsync(string username, string password);
    }
}