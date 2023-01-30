using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asset_Management.ViewModel
{
    public class RequestListResponse
    {
        public long Id { get; set; }
        public long PicId { get; set; }
        public string FullName { get; set; }
        public string Specification { get; set; }
        public DateTime RequestDate { get; set; }
        public string Status { get; set; }
    }
}
