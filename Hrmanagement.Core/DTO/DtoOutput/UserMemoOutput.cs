using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrmanagement.Core.DTO.DtoOutput
{
    public class UserMemoOutput
    {
        public int id { get; set; }
        public int UserId { get; set; }

        public string? Title { get; set; } = null!;
        public string? Description { get; set; } = null!;
        public string Url { get; set; }
        public bool AvailableForPublic { get; set; }
        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
        public string? userName { get; set; }
    }
}
