using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Utilities
{
    public class NotificationGenerator
    {
        public static string GenerateRejected(string name)
        {
            return "After thorough review of your request I decided to REJECT publishing of: " + name.ToUpper();
        }
        public static string GenerateAccepted(string name)
        {
            return "After thorough review of your request I decided to APPROVE publishing of: " + name.ToUpper();
        }

        public static string GenerateApprovalHeader()
        {
            return "Publishing approval";
        }

        public static string GenerateRejectionHeader()
        {
            return "Publishing rejection";
        }
    }
}
