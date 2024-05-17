﻿using System.ComponentModel.DataAnnotations;

namespace Easy_Task.Domain.Entities
{
    public class BaseEntity
    {
        [Required]
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime ModifiedAt { get; set; } = DateTime.UtcNow;
       
    }
}
