using System.Collections.Generic;

namespace WebPortal.Api.Models
{
    public class ReportRequest
    {
        public string PropertyId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public IList<string> Metrics { get; set; }
        public IList<string> Dimensions { get; set; }
    }
}
