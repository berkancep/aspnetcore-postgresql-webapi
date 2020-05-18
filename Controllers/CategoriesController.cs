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
    public class CategoriesController : ControllerBase
    {
        private readonly IPostgreManager _postgreManager;
        public CategoriesController(IPostgreManager postgreManager)
        {
            _postgreManager = postgreManager;
        }

        [HttpGet]
        public IEnumerable<Category> Get()
        {
            return _postgreManager.GetCategories();
        }
    }
}