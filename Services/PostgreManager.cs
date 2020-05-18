using Npgsql;
using System;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using PostgreSQLExample.Models;

namespace PostgreSQLExample.Services
{
    public class PostgreManager : IPostgreManager
    {
        private static NpgsqlConnection _connection = null;
        private static string _connectionString;

        public PostgreManager(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("PostgreSQLConnection");
        }

        internal static NpgsqlConnection GetConnection
        {
            get
            {
                if (_connection == null)
                {
                    _connection = new NpgsqlConnection();
                    _connection.ConnectionString = _connectionString;
                    _connection.Open();
                }

                return _connection;
            }

        }

        public Post AddPost(Post post)
        {
            var command = new NpgsqlCommand("INSERT INTO Posts(Message, OperationDate) VALUES (@message,@operationDate)", GetConnection);

            command.Parameters.AddWithValue("@message", post.Message);

            command.Parameters.AddWithValue("@operationDate", DateTime.Now);

            command.ExecuteNonQuery();

            return post;
        }

        public bool DeletePost(int id)
        {
            var command = new NpgsqlCommand(string.Format("delete from Posts where Id = @postId"), GetConnection);

            command.Parameters.AddWithValue("@postId", id);

            if (command.ExecuteNonQuery() == 0)
            {
                return false;
            }

            return true;
            
        }

        public IEnumerable<Category> GetCategories()
        {
            var command = new NpgsqlCommand("SELECT * FROM Categories", GetConnection);
            NpgsqlDataReader reader = command.ExecuteReader();


            List<Category> categories = new List<Category>();

            while (reader.Read())
            {
                Category category = new Category();
                category.Id = Convert.ToInt32(reader["Id"]);
                category.Name = reader["Name"].ToString();

                categories.Add(category);
            }

            reader.Close();

            return categories;
        }

        public Post GetPostById(int id)
            {
            var command = new NpgsqlCommand("SELECT * FROM Posts WHERE Id = @postId", GetConnection);

            command.Parameters.AddWithValue("@postId", id);

            var reader = command.ExecuteReader();

            Post post = new Post();

            while (reader.Read())
            {
                post.Id = Convert.ToInt32(reader["Id"]);
                post.Message = reader["Message"].ToString();
            }

            reader.Close();

            return post;
        }

        public IEnumerable<Post> GetPostByCategoryId(int id)
        {
            var command = new NpgsqlCommand("SELECT * FROM Posts INNER JOIN PostCategories ON Posts.Id=PostCategories.PostId WHERE CategoryId = @categoryId", GetConnection);

            command.Parameters.AddWithValue("@categoryId", id);

            var reader = command.ExecuteReader();

            List<Post> posts = new List<Post>();
           

            while (reader.Read())
            {
                Post post = new Post();
                post.Id = Convert.ToInt32(reader["Id"]);
                post.Message = reader["Message"].ToString();
                posts.Add(post);
            }

            reader.Close();

            return posts;
        }

        public IEnumerable<Post> GetPosts()
        {
            var command = new NpgsqlCommand("SELECT * FROM Posts", GetConnection);
            var reader = command.ExecuteReader();


            List<Post> posts = new List<Post>();

            while (reader.Read())
            {
                Post post = new Post();
                post.Id = Convert.ToInt32(reader["Id"]);
                post.Message = reader["Message"].ToString();

                posts.Add(post);
                
            }

            reader.Close();

            return posts;
        }

        public Post UpdatePost(int id,Post post)
        {
            var command = new NpgsqlCommand("UPDATE Posts set Message = @message,OperationDate=@operationDate where Id= @postId", GetConnection);

            command.Parameters.AddWithValue("@postId", id);
            command.Parameters.AddWithValue("@message", post.Message);
            command.Parameters.AddWithValue("@operationDate", post.OperationDate);

            command.ExecuteNonQuery();


            return post;
        }


    }
}

