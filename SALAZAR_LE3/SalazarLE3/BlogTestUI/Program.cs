using BlogDataLibrary.Data;
using BlogDataLibrary.Database;
using BlogDataLibrary.Models;
using Microsoft.Extensions.Configuration;

namespace BlogTestUI
{
    internal class Program
    {
        private static int? _currentUserId;

        static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var dataAccess = new SqlDataAccess(config);
            var db = new SqlData(dataAccess);

             Authenticate(db);
             if (_currentUserId is null) return;

            // Register(db);
            // AddPost(db);
            // ListPosts(db);
             ShowPostDetails(db);
        }

        static void Authenticate(ISqlData db)
        {
            Console.Write("Username: ");
            var u = Console.ReadLine()?.Trim() ?? "";
            Console.Write("Password: ");
            var p = Console.ReadLine() ?? "";

            var id = db.ValidateUserAsync(u, p).GetAwaiter().GetResult();
            _currentUserId = id;
            Console.WriteLine(id is null ? "Invalid credentials." : $"Welcome (UserId={id})");
        }

        static void Register(ISqlData db)
        {
            Console.Write("Username: "); var u = Console.ReadLine()?.Trim() ?? "";
            Console.Write("Password: "); var p = Console.ReadLine() ?? "";
            Console.Write("FirstName: "); var f = Console.ReadLine()?.Trim() ?? "";
            Console.Write("LastName: "); var l = Console.ReadLine()?.Trim() ?? "";

            var newId = db.RegisterAsync(new UserModel
            {
                Username = u,
                Password = p,
                FirstName = f,
                LastName = l
            }).GetAwaiter().GetResult();

            Console.WriteLine(newId > 0 ? $"Registered. New Id={newId}" : "Registration failed.");
        }

        static void AddPost(ISqlData db)
        {
            if (_currentUserId is null) { Console.WriteLine("Login first."); return; }

            Console.Write("Title: "); var t = Console.ReadLine() ?? "";
            Console.Write("Body: "); var b = Console.ReadLine() ?? "";

            db.CreatePostAsync(new PostModel
            {
                UserId = _currentUserId.Value,
                Title = t,
                Body = b,
                DateCreated = DateTime.UtcNow
            }).GetAwaiter().GetResult();

            Console.WriteLine("Post created.");
        }

        static void ListPosts(ISqlData db)
        {
            var posts = db.GetPostsAsync().GetAwaiter().GetResult();
            foreach (var x in posts)
                Console.WriteLine($"{x.Id,3} | {x.Title,-30} | {x.DateCreated:u}");
        }

        static void ShowPostDetails(ISqlData db)
        {
            Console.Write("Post Id: ");
            if (!int.TryParse(Console.ReadLine(), out var id)) { Console.WriteLine("Invalid."); return; }

            var p = db.GetPostAsync(id).GetAwaiter().GetResult();
            if (p is null) { Console.WriteLine("Not found."); return; }

            Console.WriteLine($"Id: {p.Id}\nTitle: {p.Title}\nDate: {p.DateCreated:u}\nBody:\n{p.Body}");
        }
    }
}
