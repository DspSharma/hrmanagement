using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrmanagement.Data.Entities
{
    public class Project : BaseEntitiy
    {
        [Column(TypeName = "varchar(100)")]
        public string Title { get; set; } = null!;

        [Column(TypeName = "varchar(500)")]
        public string Description { get; set; } = null!;

        [Column(TypeName ="varchar(500)")]
        public string Url { get; set; } = null!;

        [Column(TypeName ="varchar(30)")]
        public string Status { get; set; } = null!;

    }
}
