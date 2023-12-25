using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos
{
    public class RegistrationConfirmationTokenDto
    {
        public long Id {  get; set; }
        public string RegistrationConfirmationToken { get; set; }
    }
}
