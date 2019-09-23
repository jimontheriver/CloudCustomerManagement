using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerManagement.Library.Entities
{
    [Table("Customers")]
    public class Customer
    {
        [Key]
        [Column("CustomerId")]
        public long Id { get; set; }
        public long SourceId { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public DateTimeOffset Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset Updated { get; set; }
        public string UpdatedBy { get; set; }
        [JsonProperty("industries")]
        public virtual ICollection<string> IndustryCodes { get; set; }
    }
}