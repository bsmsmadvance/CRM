using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models
{
    public interface ISoftDeleteEntity
    {
        bool IsDeleted { get; set; }
    }
}
