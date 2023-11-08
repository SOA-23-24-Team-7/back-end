using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Utilities
{
    public class NotificationGenerator
    {
        public NotificationGenerator() { }
        public string GenerateRejected(string name,string comment)
        {
            return "Your request for public " + name + " has been rejected. " + comment;
        }
        public string GenerateAccepted(string name)
        {
            return "Your request for public " + name + " has been accepted.";
        }
    }
}
