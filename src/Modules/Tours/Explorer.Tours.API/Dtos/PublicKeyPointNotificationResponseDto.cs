using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class PublicKeyPointNotificationResponseDto
    {
        public long Id { get; init; }
        public long RequestId { get; init; }
        public long AuthorId { get; init; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public bool IsAccepted { get; set; }
        public string? Comment { get; set; }
    }
}
