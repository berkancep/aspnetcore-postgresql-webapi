using PostgreSQLExample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostgreSQLExample.Services
{
    public interface IPostgreManager
    {
        public IEnumerable<Category> GetCategories();
        public IEnumerable<Post> GetPosts();
        public Post GetPostById(int id);
        public IEnumerable<Post> GetPostByCategoryId(int id);
        public Post AddPost(Post post);
        public Post UpdatePost(int id,Post post);
        public bool DeletePost(int id);
    }
}
