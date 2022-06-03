using System;
using System.ComponentModel.DataAnnotations;

namespace DukkantekTask.Service.Models.Requests
{
    public class SellProductRequest
    {
        [Required]
        public Guid Id { get; set; }
    }
}
