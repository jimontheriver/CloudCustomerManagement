using Newtonsoft.Json;
using System.Collections.Generic;

namespace CustomerManagement.Library.Models
{
    public class GetCustomerResponse
    {
        public long Id { get; set; }
        public long SourceId { get; set; }
        public string Name { get; set; }
        [JsonProperty("industryCodes")]
        public virtual ICollection<string> IndustryCodes { get; set; }

    }
}
