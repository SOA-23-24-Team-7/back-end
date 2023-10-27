using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.API.Dtos
{
    public class CommentUpdateDto
    {
        public long Id { get; set; }
        public string Text { get; set; }
    }
}
