﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileStorage;

namespace Base.DTOs
{
    public class FileWithIDDTO
    { 
        public Guid ID { get; set; }
        public FileDTO FileDTO { get; set; }
    }
}
