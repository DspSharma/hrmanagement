using Hrmanagement.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrmanagement.Core.DTO.DtoInput
{
    public class UserMemoInput
    {
        public int id { get; set; }

        public int UserId { get; set; }
      
        public string? Title { get; set; } = null!;
        public string? Description { get; set; } = null!;

        [Url(ErrorMessage = "Invalid URL format")]
        public string Url { get; set; }
        public bool AvailableForPublic { get; set; }
        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

    }
}
