using BlogDataLibrary.Database;
using BlogDataLibrary.Models;

namespace BlogDataLibrary.Data
{
    public interface ISqlData
    {
        Task<IEnumerable<ListPostModel>> GetPostsAsync();
        Task<PostModel?> GetPostAsync(int id);
        Task CreatePostAsync(PostModel post);
        Task<int?> ValidateUserAsync(string username, string password);
    }

    public class SqlData : ISqlData
    {
        private readonly ISqlDataAccess _db;
        private readonly string _connName = "BlogDB";

        public SqlData(ISqlDataAccess db) => _db = db;

        public async Task<IEnumerable<ListPostModel>> GetPostsAsync() =>
            await _db.LoadData<ListPostModel, dynamic>("dbo.spPosts_List", new { }, _connName);

        public async Task<PostModel?> GetPostAsync(int id) =>
            (await _db.LoadData<PostModel, dynamic>("dbo.spPosts_Get", new { Id = id }, _connName)).FirstOrDefault();

        public async Task CreatePostAsync(PostModel post) =>
            await _db.SaveData("dbo.spPosts_Create",
                new { post.UserId, post.Title, post.Body, post.DateCreated }, _connName);

        public async Task<int?> ValidateUserAsync(string username, string password)
        {
            var rows = await _db.LoadData<UserModel, dynamic>("dbo.spUsers_Validate",
                new { Username = username, Password = password }, _connName);
            return rows.FirstOrDefault()?.Id;
        }
    }
}
