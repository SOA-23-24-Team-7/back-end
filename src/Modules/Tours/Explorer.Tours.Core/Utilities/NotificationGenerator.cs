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
        public string GenerateRejected(string name)
        {
            return "Your request for publishing " + name + " has been REJECTED. ";
        }
        public string GenerateAccepted(string name)
        {
            return "Your request for publishing - " + name + " has been ACCEPTED.";
        }
    }
}
