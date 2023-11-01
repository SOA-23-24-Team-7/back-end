using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
    public class Notification : Entity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public Notification(string name) {
            Name = name;    
        }
    }
}
