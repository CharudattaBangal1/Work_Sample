using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace omnisearch
{
    public class Mpn
    {
        public BasicDetails basicDetails { get; set; }
        public List<Endorsement> endorsements { get; set; }
    }

    public class Endt_Ecard
    {
        public string Endt_ECards { get; set; }
    }

    public class Endorsement
    {
        public BasicDetails_Endorsement basicDetails { get; set; }

        //public List<List<string>> Endt_Ecard { get; set; }

        public List<Endt_Ecard> Ecards { get; set; }
    }

    public class BasicDetails_Endorsement
    {
        public List<string> EE_Endt_Schedule { get; set; }
        public List<string> EE_Endt_Annexure { get; set; }
        public List<string> EE_Endt_CD_Statement { get; set; }
    }

    public class BasicDetails
    {
       public string schedule {get;set;}
        public string annexure {get;set;}
        public string cd_statement {get;set;}
        public string Receipt_Letter {get;set;}
        public string Quote_Slip {get;set;}
        public string Performa_Invoice {get;set;}
        public string HCN {get;set;}
        public string GST {get;set;}
        public string PAN {get;set;}
        public string MPH_Card {get;set;}
        public string Endt_Card {get;set;}
    }
}