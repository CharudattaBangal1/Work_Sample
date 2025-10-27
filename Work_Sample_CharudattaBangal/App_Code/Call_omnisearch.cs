using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Collections;
using System.Data;
using Newtonsoft.Json;
using System.Web.Script.Serialization;


namespace omnisearch
{
    public class Call_omnisearch
    {
        public Boolean isActive = true;
        public string LogType = string.Empty;
        public static string GlobalId;
        public string CategoryId = string.Empty;
        public string DocumentId = string.Empty;
        public string ReferenceId = string.Empty;
        public string DocumentIndex = string.Empty;
        public string ImageIndex = string.Empty;
        public string FileName = string.Empty;
        public string Status = string.Empty;

        public OmniSearchResponse omniSearchResponse(OmniSearch omniSearch)
        {
            OmniSearchResponse omniSearchResponse = new OmniSearchResponse();
            DAL dal = new DAL();
            Errlog errlog = new Errlog();
            DataSet ds = new DataSet();
            APIRequest api = new APIRequest();
            Hashtable ht = new Hashtable();
            string searchURL = "xxx";
            searchDataClassParam dataClassParam = new searchDataClassParam();
            string docsearchparamid = string.Empty;
            string req = string.Empty;
            string res = string.Empty;
            string parameter = string.Empty;
            searchres searchres = new searchres();
            string Response = string.Empty;
            Searchdocuments searchdocuments = new Searchdocuments();





            try
            {

                req = new JavaScriptSerializer().Serialize(omniSearch);
                //searchdocuments.PerformSearch(req);

                
                //api.callApi(searchURL, req, ref res);

                Response = Response.Replace("200_", "");
                //searchres.search(Response);

            }
            catch

            {
               
            }


            
          

            return omniSearchResponse;
        }


    }

   
}