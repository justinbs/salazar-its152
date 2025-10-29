using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using BlogDataLibrary.Database;
using BlogDataLibrary.Models;

namespace BlogDataLibrary.Data
{
    // INTERFACE lives in the same file
    public interface ISqlData
    {
        Task<IEnumerable<ListPostModel>> ListPostsAsync();
        Task<PostModel?> ShowPostDetailsAsync(int id);
        Task CreatePostAsync(PostModel post);
        Task<int?> ValidateUserAsync(string username, string password);
        Task<int?> RegisterUserAsync(UserModel user);
    }

    // IMPLEMENTATION
    public class SqlData : ISqlData
    {
        private readonly ISqlDataAccess _db;
        private readonly string _connName = "BlogDB";

        public SqlData(ISqlDataAccess db) => _db = db;

        public async Task<IEnumerable<ListPostModel>> ListPostsAsync() =>
            await _db.LoadData<ListPostModel, dynamic>("dbo.spPosts_List", new { }, _connName);

        public async Task<PostModel?> ShowPostDetailsAsync(int id) =>
            (await _db.LoadData<PostModel, dynamic>("dbo.spPosts_Get", new { Id = id }, _connName)).FirstOrDefault();

        public async Task CreatePostAsync(PostModel post) =>
            await _db.SaveData("dbo.spPosts_Create",
                new { post.UserId, post.Title, post.Body, post.DateCreated }, _connName);

        public async Task<int?> ValidateUserAsync(string username, string password)
        {
            var rows = await _db.LoadData<UserModel, dynamic>(
                "dbo.spUsers_Authenticate",
                new { Username = username, Password = password },
                _connName);

            return rows.FirstOrDefault()?.Id;
        }

        public async Task<int?> RegisterUserAsync(UserModel user)
        {
            var rows = await _db.LoadData<dynamic, dynamic>(
                "dbo.spUsers_Register",
                new { user.Username, user.Password, user.FirstName, user.LastName },
                _connName);

            var first = rows.FirstOrDefault();
            return first == null ? 0 : (int)first.NewId;
        }
        public async Task<PostModel?> GetPostAsync(int id)
        {
            var rows = await _db.LoadData<PostModel, dynamic>(
                "dbo.spPosts_Get",
                new { Id = id },
                _connName);

            return rows.FirstOrDefault();
        }
    }
}
