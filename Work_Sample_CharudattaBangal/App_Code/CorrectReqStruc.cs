using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace omnisearch
{
    public class Child
    {
        public string id { get; set; }
        public string text { get; set; }
        //public List<Child> children { get; set; }
        public string icon { get; set; }
    }

    public class CorrectReqStruc
    {
        public string id { get; set; }
        public string text { get; set; }
        public List<Child> children { get; set; }
    }

}