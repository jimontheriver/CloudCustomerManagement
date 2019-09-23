using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloudCustomerManagement.Api.Models
{
    public abstract class LinkableEntityBase
    {
        [NotMapped]
        public IEnumerable<Link> Links { get; set; } = new List<Link>();
    }
}
