using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebPortal.Api.Policy
{
    public class JsonLowercaseNamingPolicy : JsonNamingPolicy
    {
        public override string ConvertName(string name) => name.ToLowerInvariant();
    }
}
