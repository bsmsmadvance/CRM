﻿using System;

namespace Base.DTOs
{
    public class BaseDTO
    {
        public Guid? Id { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? Updated { get; set; }
    }
}
