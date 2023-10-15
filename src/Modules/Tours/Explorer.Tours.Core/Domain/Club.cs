using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
    public class Club : Entity
    {
        public long OwnerId { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public string Image { get; init; }
        public Club(long ownerId, string name, string description, string image)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Invalid Name.");
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Invalid Description.");
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Invalid Image.");
            Name = name;
            Description = description;
            Image = image;
            OwnerId = ownerId;
        }
    }
}
