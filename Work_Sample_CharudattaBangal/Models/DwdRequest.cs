using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace omnisearch
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class DownloadRequest
    {
        public string GlobalId { get; set; }
        public string OmniDocImageIndex { get; set; }
        public string FileName { get; set; }
    }

    public class DwdRequest
    {
        public List<DownloadRequest> DownloadRequest { get; set; }
        public string Identifier { get; set; }
        public string SourceSystemName { get; set; }
    }



}