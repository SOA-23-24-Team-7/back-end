﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class PublicKeyPointNotificationCreateDto
    {
        public long Id { get; init; }
        public long RequestId { get; init; }
        public long AuthorId { get; init; }
        public string Description { get; set; }
        DateTime? Created { get; set; }

    }
}
