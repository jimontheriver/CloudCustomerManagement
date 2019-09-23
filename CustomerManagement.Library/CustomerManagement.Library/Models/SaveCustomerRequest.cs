using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerManagement.Library.Models
{
    public class SaveCustomerRequest
    {
        public long SourceId { get; set; }
        public string Name { get; set; }
        [JsonProperty("industryCodes")]
        public virtual ICollection<string> IndustryCodes { get; set; }

    }
}
