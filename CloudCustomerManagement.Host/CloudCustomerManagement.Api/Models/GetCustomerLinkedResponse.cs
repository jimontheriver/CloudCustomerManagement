using System.Collections.Generic;
using Newtonsoft.Json;

namespace CloudCustomerManagement.Api.Models
{
    public class GetCustomerLinkedResponse: LinkableEntityBase
    {
        public long SourceId { get; set; }
        public string Name { get; set; }
        [JsonProperty("industryCodes")]
        public virtual ICollection<string> IndustryCodes { get; set; }

    }
}