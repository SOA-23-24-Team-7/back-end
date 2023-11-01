using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Blog.Core.Domain
{
    public enum BlogStatus { Draft, Published, Closed };
    public class Blog : Entity
    {

        public string Title { get; private set; }
        public string Description { get; private set; }
        public DateTime Date { get; init; }
        public List<string>? Pictures { get; private set; }
        public BlogStatus Status { get; private set; }

        
        

        public Blog( string title, string description, DateTime date, List<string>? pictures, BlogStatus status)
        {
            if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentException("Title ne sme biti prazan ili null.\n");
            }


            Title = title;
            Description = description;
            Date = DateTime.UtcNow.Date;
            Pictures = pictures;
            Status = status;
        }

        public void UpdateBlog(string title, string description, List<string> pictures, BlogStatus status)
        {

            Title = title;
            Description = description;
            Pictures = pictures;
            Status = status;

        }
    }
}
