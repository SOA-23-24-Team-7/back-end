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

        public string Title { get; init; }
        public string Description { get; init; }
        public DateTime Date { get; init; }
        public List<string>? Pictures { get; init; }
        public BlogStatus Status { get; init; }

        public Blog() { }

        public Blog(int id, string title, string description, DateTime date, List<string>? pictures, BlogStatus status)
        {

            if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentException("Title ne sme biti prazan ili null.\n");
            }


            Title = title;
            Description = description;
            Date = date;
            Pictures = pictures;
            Status = status;
        }
    }
}
