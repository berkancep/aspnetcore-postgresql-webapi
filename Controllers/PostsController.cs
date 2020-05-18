using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PostgreSQLExample.Models;
using PostgreSQLExample.Services;

namespace PostgreSQLExample.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostgreManager _postgreManager;
        public PostsController(IPostgreManager postgreManager)
        {
            _postgreManager = postgreManager;
        }

        [HttpGet]
        public IEnumerable<Post> Get()
        {
            return _postgreManager.GetPosts();
        }

        [HttpGet("{id}")]
        public ActionResult<Post> Get(int id)
        {
            return _postgreManager.GetPostById(id);
        }

        [HttpPost]
        public ActionResult Post(Post post)
        {
            _postgreManager.AddPost(post);

            return CreatedAtAction("Get", new { id = post.Id }, post);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, Post post)
        {
            _postgreManager.UpdatePost(id, post);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _postgreManager.DeletePost(id);
            return NoContent();
        }

        [HttpGet("Categories/{id}")]
        public IEnumerable<Post> CategoriesPosts(int id)
        {
            return _postgreManager.GetPostByCategoryId(id);

        }

    }
}