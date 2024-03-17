using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.API.Dtos
{
    public class BlogReportCreateDto
    {
        public int UserId { get; set; }
        public int BlogId { get; set; }
        public string Reason { get; set; }
    }
}
