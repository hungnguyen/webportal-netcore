using System.Collections.Generic;

namespace WebPortal.Api.Models
{
    public class GraphData
    {
        public IList<string> Labels { get; set; } = new List<string>();
        public IList<string> Values { get; set; } = new List<string>();
    }
}
