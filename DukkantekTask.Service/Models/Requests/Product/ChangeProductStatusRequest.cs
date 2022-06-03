using DukkantekTask.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace DukkantekTask.Service.Models.Requests
{
    public class ChangeProductStatusRequest
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public ProductStatusEnum Status { get; set; }
    }
}
