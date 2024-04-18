using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPortal.ViewModels.Common
{
    public interface ITrackable
    {
        DateTime Created { get; set; }
        DateTime Updated { get; set; }
        string CreatedBy { get; set; }
        string UpdatedBy { get; set; }
    }
}
