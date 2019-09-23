using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Google.Apis.Auth.OAuth2;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CustomerManagement.Library.Repositories
{
    public class RepositoryConfiguration : IRepositoryConfiguration
    {
        public RepositoryConfiguration()
        {
            var fileName = Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS");
            var credentialContent = System.IO.File.ReadAllText(fileName);
            var credentials = JObject.Parse(credentialContent);
            ProjectId = (string)credentials["project_id"];
            Namespace = "customers";
        }

        public string ProjectId { get; set; }
        public string Namespace { get; set; }
    }
}
