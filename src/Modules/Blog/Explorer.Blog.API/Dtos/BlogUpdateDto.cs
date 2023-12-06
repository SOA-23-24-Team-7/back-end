using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.API.Dtos
{

    public class BlogUpdateDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public BlogStatus Status { get; init; }
        public int AuthorId { get; set; }
        public BlogVisibilityPolicy VisibilityPolicy { get; set; }
    }
}
