using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace omnisearch1
{
    public class DownloadResponse
    {
        public string GlobalId { get; set; }
        public string OmniDocImageIndex { get; set; }
        public string FileName { get; set; }
        public string ByteArray { get; set; }
        public List<Error> Error { get; set; }
    }

    public class Error
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }

    public class DwdResponse
    {
        public List<DownloadResponse> DownloadResponse { get; set; }
    }

}