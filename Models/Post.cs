using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostgreSQLExample.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime OperationDate { get; set; }
    }
}
