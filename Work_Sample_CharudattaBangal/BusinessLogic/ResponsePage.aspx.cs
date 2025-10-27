using Newtonsoft.Json;
using omnisearch1;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace omnisearch
{
    public partial class ResponsePage : System.Web.UI.Page
    {
        public string searchValue { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            searchValue = Session["resultantJSON"] as string;

            if (!string.IsNullOrEmpty(searchValue))
            {
                string jsonresponse = string.Empty;
                HiddenField1.Value = searchValue.ToString();


                // Deserialize the JSON response into a DataTable
                DataTable dataTable = JsonConvert.DeserializeObject<DataTable>(jsonresponse);

                // Bind the DataTable to the GridView control
                //jsonGridView.DataSource = dataTable;
                //jsonGridView.DataBind();
            }
        }



        protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Write("<script LANGUAGE='JavaScript' >alert('Please enter Master Policy Number')</script>");
        }

        [WebMethod]
        public static string DownloadFile(string id, string fileName)
        {
            DwdResponse dwdResponse = new DwdResponse();

            string stat_2 = string.Empty;

            int statCode = 0;

            DownloadResponse downloadResponse = new DownloadResponse();

            string url = ConfigurationManager.AppSettings["DownloadAPIUrl"];

            //string url = "https://abhiutilityuat.adityabirlahealth.com/OmniDocsSrchDwldAPI/Service.svc/OmniDocsDownloadAPI";
            string response = string.Empty;

            string pattern = @"DownloadFile\(\d+,(.*?)\)";

            Match match = Regex.Match(fileName, pattern);

            fileName = match.Groups[1].Value;

            Searchdocuments searchdocuments = new Searchdocuments();

            string request = searchdocuments.CreateDwdRequest(id, fileName);

            APIRequest aPIRequest = new APIRequest();

            aPIRequest.callApi(url, request, ref response,ref statCode);

            if (response == null)
            {
                stat_2 =  "Got no response from underlying service";
                return stat_2;
            }

            if(!response.Contains("Success"))
            {
                stat_2 = "Got no response from underlying service";
                return stat_2;
            }

            dwdResponse = JsonConvert.DeserializeObject<DwdResponse>(response);
            if(dwdResponse.DownloadResponse[0] == null)
            {
                stat_2 = "Document not found";
                return stat_2;
            }
            downloadResponse = dwdResponse.DownloadResponse[0];

            string receivedFileBytesString = downloadResponse.ByteArray;

            // Convert the received byte array string back to a byte array
            byte[] receivedFileBytes = Convert.FromBase64String(receivedFileBytesString);


            string receivedFileName = downloadResponse.FileName;

            // Path to the Downloads folder in C drive
            string downloadsFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");

            // Complete path to save the file in the Downloads folder
            string savePath = Path.Combine(downloadsFolder, receivedFileName);

            try
            {
                // Create the file using the received file name and content (byte array)
                System.IO.File.WriteAllBytes(savePath, receivedFileBytes);
                //Console.WriteLine("File created successfully in the Downloads folder.");
                ResponsePage responsePage1 = new ResponsePage();
                
                Page currentPage = HttpContext.Current.Handler as Page;
                if (currentPage != null)
                {
                    //HttpContext.Current.Response.Write("<script LANGUAGE='JavaScript' >alert('File is successfully downloaded in downloads folder')</script>");
                    //HttpContext.Current.Response.Write("File is successfully downloaded in downloads folder");
                }
                stat_2 = "Success";
                return stat_2;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                ResponsePage responsePage1 = new ResponsePage();

                //Page currentPage = HttpContext.Current.Handler as Page;
                //if (currentPage != null)
                //{
                //    HttpContext.Current.Response.Write($"<script LANGUAGE='JavaScript' >alert(Error Occured :- {ex.Message}')</script>");
                //}
                stat_2 = "An error occurred: " + ex.Message;
                return stat_2;
            }
        }
    }
}