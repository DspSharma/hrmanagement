using Hrmanagement.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrmanagement.Core.DTO.DtoInput
{
    public class ApiCredentialsInput
    {
        //public int Id { get; set; }
        //public int ProjectId { get; set; }
        //public string Description { get; set; }
        //public string ApiKey { get; set; }
        //public string ClientId { get; set; }
        //public string ClientSecret { get; set; }
        //public int AllowLimit { get; set; }
        //public int ConsumedLimit { get; set; }
        //public string Status { get; set; }
        //public DateTime CreatedAt { get; set; }
        //public DateTime UpdatedAt { get; set; }
        public int Id { get; set; }

        public int ProjectId { get; set; }

        public string Service { get; set; }

        public string Description { get; set; }

        public string ApiHost { get; set; }

        public string ApiKey { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }

        public string Password { get; set; }
        public int AllowLimit { get; set; }
        public int ConsumedLimit { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
