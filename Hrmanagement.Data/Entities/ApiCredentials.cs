using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrmanagement.Data.Entities
{
    public class ApiCredentials
    {
        [Key]
        public int Id { get; set; }
       
        public int ProjectId { get; set; }
        public Project Project { get; set; }

        [Column(TypeName ="Varchar(30)")]
        public string Service { get; set; }

        [Column(TypeName = "varchar(250)")]
        public string Description { get; set; }

        [Column(TypeName = "varchar(256)")]
        public string ApiHost { get; set; }

        [Column(TypeName = "varchar(256)")]
        public string ApiKey { get; set; }
        [Column(TypeName = "varchar(250)")]
        public string ClientId { get; set; }
        [Column(TypeName = "varchar(250)")]
        public string ClientSecret { get; set; }

        [Column(TypeName = "varchar(128)")]
        public string Password { get; set; }
        public int AllowLimit { get; set; }
        public int ConsumedLimit { get; set; }
        [Column(TypeName = "varchar(15)")]
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
