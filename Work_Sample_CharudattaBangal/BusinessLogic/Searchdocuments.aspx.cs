using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Xml;
using System.IO;
using System.Configuration;
using Newtonsoft.Json.Linq;
using System.Web.Services;
using System.Collections;
using omnisearch1;
using static System.Net.WebRequestMethods;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;
using System.Web.Services.Description;
using System.Runtime.Remoting.Lifetime;

namespace omnisearch
{
    public partial class Searchdocuments : System.Web.UI.Page
    {
        Call_omnisearch Call_Omnisearch = new Call_omnisearch();
        string request = string.Empty;
        string resp1 = string.Empty;
        APIRequest reqObj = new APIRequest();
        string response = string.Empty;
        Dictionary<string, string> imgIndex = new Dictionary<string, string>();
        readonly string url = ConfigurationManager.AppSettings["SearchAPIUrl"];
        Response resp = new Response();
        Mpn mpnobj = new Mpn();
        Endorsement endorsement = new Endorsement();
        BasicDetails_Endorsement basicDetails_Endorsement = new BasicDetails_Endorsement();
        List<string> endtcards = new List<String>();
        BasicDetails basicDetails = new BasicDetails();
        Endt_Ecard ecards = new Endt_Ecard();
        HashSet<string> endorsementsSet = new HashSet<string>();
        List<SearchResponse> endorsementObjects = new List<SearchResponse>();
        MultiTreeData multiTreeData = new MultiTreeData();
        List<string> docReq = new List<string>();
        int statusCode = 0;

        List<string> docReqEcards = new List<string>();

        List<string> endorsementDocuments = new List<string>();

        bool isValidatedFlag = true;

        protected void Page_Load(object sender, EventArgs e)
        {
            docReq.Add("EE_Endt_CD_Statement");
            docReq.Add("EE_Endt_Annexure");
            docReq.Add("EE_Endt_Schedule");

            docReqEcards.Add("Endt_Ecard");


            endorsementDocuments.Add("EE_Endt_CD_Statement");
            endorsementDocuments.Add("EE_Endt_Annexure");
            endorsementDocuments.Add("EE_Endt_Schedule");

            
        }

        protected void parameterDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList dropDownList1 = (DropDownList)sender;

            try
            {
                if (DropDownList1.SelectedValue == "param0")
                {
                    searchButton.Visible = false;
                    searchResultLabel.Visible = false;
                }

                if (DropDownList1.SelectedValue == "param1")
                {
                    masterPolicyLabel.Visible = true;
                    masterPolicyTextBox.Visible = true;
                    searchButton.Visible = true;
                    searchResultLabel.Visible = true;
                    ClearButton.Visible = true;
                    clearResultLabel.Visible = true;
                }
                else
                {
                    masterPolicyLabel.Visible = false;
                    masterPolicyTextBox.Visible = false;
                }

                if (DropDownList1.SelectedValue == "param2") // Change "param1" to the value corresponding to "Master Policy"
                {
                    EndorsementLabel.Visible = true;
                    EndorsementTextbox.Visible = true;
                    searchButton.Visible = true;
                    searchResultLabel.Visible = true;
                    ClearButton.Visible = true;
                    clearResultLabel.Visible = true;
                }
                else
                {
                    EndorsementLabel.Visible = false;
                    EndorsementTextbox.Visible = false;
                }
                if (DropDownList1.SelectedValue == "param3")
                {
                    policynumberLabel.Visible = true;
                    PolicyNumbertextbox.Visible = true;
                    searchButton.Visible = true;
                    searchResultLabel.Visible = true;
                    ClearButton.Visible = true;
                    clearResultLabel.Visible = true;
                }
                else
                {
                    policynumberLabel.Visible = false;
                    PolicyNumbertextbox.Visible = false;
                }
                if (DropDownList1.SelectedValue == "param4")
                {
                    Employeelabel.Visible = true;
                    EmployeeTextbox.Visible = true;
                    masterPolicyLabel.Visible = true;
                    masterPolicyTextBox.Visible = true;
                    searchButton.Visible = true;
                    searchResultLabel.Visible = true;
                    ClearButton.Visible = true;
                    clearResultLabel.Visible = true;
                }
                else
                {
                    Employeelabel.Visible = false;
                    EmployeeTextbox.Visible = false;
                }
            }
            catch { }
        }

        private string CreateRequest()
        {
            string time = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            Root root = new Root();
            DataClassParam dataClassParam = new DataClassParam();

            Metadata metadata = new Metadata();
            SearchRequest searchRequest = new SearchRequest();
            Sender sender = new Sender();
            root.SearchRequest = new List<SearchRequest>();
            searchRequest.DataClassParam = new List<DataClassParam>();

            sender.LogicalID = "BaNCS";
            sender.TaskID = "Quote";
            sender.ReferenceID = $"{Guid.NewGuid().ToString("N").Substring(0, 20)}";
            sender.CreationDateTime = $"{time}";
            sender.TODID = $"{Guid.NewGuid().ToString("N").Substring(0, 20)}";

            metadata.Sender = sender;

            searchRequest.CategoryID = "1009";
            searchRequest.DocumentID = null;
            searchRequest.ReferenceID = "";
            searchRequest.FileName = "";
            searchRequest.Description = "";

            ////If input is employee number
            if (DropDownList1.SelectedValue.ToString() == "param4")
            {
                dataClassParam.DocSearchParamId = "31";
                dataClassParam.Value = EmployeeTextbox.Text;
                searchRequest.DataClassParam.Add(dataClassParam);
                dataClassParam = new DataClassParam();
                dataClassParam.DocSearchParamId = "30";
                dataClassParam.Value = masterPolicyTextBox.Text;
                searchRequest.DataClassParam.Add(dataClassParam);
            }
            //Policy Number
            else if (DropDownList1.SelectedValue.ToString() == "param3")
            {
                dataClassParam.DocSearchParamId = "2";
                dataClassParam.Value = PolicyNumbertextbox.Text;
                searchRequest.DataClassParam.Add(dataClassParam);
            }
            //Endrosement Number
            else if (DropDownList1.SelectedValue.ToString() == "param2")
            {
                dataClassParam.DocSearchParamId = "25";
                dataClassParam.Value = EndorsementTextbox.Text;
                searchRequest.DataClassParam.Add(dataClassParam);
            }
            //Master policy number
            else if (DropDownList1.SelectedValue.ToString() == "param1")
            {
                dataClassParam.DocSearchParamId = "30";
                dataClassParam.Value = masterPolicyTextBox.Text;
                searchRequest.DataClassParam.Add(dataClassParam);
            }
            //dataClassParam.DocSearchParamId = "1";
            //dataClassParam.Value = "1122385019456";
            //searchRequest.DataClassParam.Add(dataClassParam);
            root.SourceSystemName = "BaNCS";
            root.SearchOperator = "AND";
            root.Metadata = metadata;
            root.SearchRequest.Add(searchRequest);

            string req = new JavaScriptSerializer().Serialize(root);

            return req;
        }

        protected void searchButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string selectedParameter = DropDownList1.SelectedValue;

                string searchValue = string.Empty;
                string searchValue1 = string.Empty;
                string searchResult = string.Empty;
                //treeNodes1.children = new List<TreeNode1>();

                if (selectedParameter == "param1")
                {
                    searchValue = masterPolicyTextBox.Text.Trim();
                    if(searchValue=="")
                    {
                        //searchButton.Enabled = false;
                    }
                }
                else if (selectedParameter == "param2")
                {
                    searchValue = EndorsementTextbox.Text.Trim();
                }
                else if (selectedParameter == "param3")
                {
                    searchValue = PolicyNumbertextbox.Text.Trim();
                }
                else if (selectedParameter == "param4")
                {
                    searchValue = EmployeeTextbox.Text.Trim();
                    searchValue1 = masterPolicyTextBox.Text.Trim();
                }
                if (selectedParameter == "param1") // MasterPolicy Number
                {
                    if (searchValue=="" || searchValue.Equals(null))
                    {
                        //string message = "Validation failed. Please check your input.";
                        //string script = $@"<script type='text/javascript'>alert('{message}');</script>";
                        //ClientScript.RegisterStartupScript(this.GetType(), "alert", script);


                        isValidatedFlag = false;

                        Response.Write(
                            "<script LANGUAGE='JavaScript' >alert('Please enter Master Policy Number')</script>"
                        );
                    }
                    else
                    {
                        endorsement.Ecards = new List<Endt_Ecard>();

                        multiTreeData.myData = new List<TreeNode3>();

                        #region MasterPolicy_BasicDetails

                        TreeNode3 mpBasicDetails = new TreeNode3
                        {
                            id = "basicDetails",
                            text = "Policy Details",
                            isFolder = true,
                            icon = "jstree-folder",
                            children = new List<TreeNode3>()
                        };

                        request = CreateRequest();
                        reqObj.callApi(url, request, ref response,ref statusCode);
                        //response = response.Replace("200_", "");
                        if (statusCode == 200)
                        {
                            resp = new JavaScriptSerializer().Deserialize<Response>(response);

                            #region MasterPolicy
                            //if (DropDownList1.SelectedValue.ToString() == "30")
                            //{

                            
                            int count_2 = 0;
                            foreach (SearchResponse obj in resp.SearchResponse)
                            {
                                string descrip = obj.Description;
                                //int count = 0;
                                if (descrip == "")
                                {
                                    foreach (var dclass in obj.DataClassParam)
                                    {
                                        if (
                                            dclass.DocSearchParamId == "25"
                                            && (dclass.Value != "" || dclass.Value != null)
                                        )
                                        {
                                            //From here I can access all the unique endorsement numbers
                                            endorsementsSet.Add(dclass.Value);
                                            endorsementObjects.Add(obj);
                                        }

                                        if (
                                            dclass.DocSearchParamId == "15"
                                            && dclass.Value == "EE_MPH_Schedule"
                                        )
                                        {
                                            //schedule = obj.FileName;
                                            basicDetails.schedule = obj.FileName;
                                            imgIndex.Add(obj.OmniDocImageIndex, obj.FileName);

                                            string hyperlinkText = $"<a href='javascript:void(0);' onclick='DownloadFile({obj.OmniDocImageIndex},{obj.FileName})'>{dclass.Value}:- {obj.FileName}</a>";

                                            //string hyperlinkText = $"Searchdocuments.aspx?id={obj.OmniDocImageIndex}";

                                            TreeNode3 treeNode2 = new TreeNode3
                                            {
                                                id = obj.OmniDocImageIndex,
                                                //text = $"{dclass.Value}:- {obj.FileName}",
                                                icon = "jstree-file",
                                                url = hyperlinkText,
                                                text = hyperlinkText,
                                                isFolder = false
                                            };

                                            mpBasicDetails.children.Add(treeNode2);
                                        }
                                        else if (
                                            dclass.DocSearchParamId == "15"
                                            && dclass.Value == "EE_MPH_Annexure"
                                        )
                                        {
                                            //annexure = obj.FileName;
                                            basicDetails.annexure = obj.FileName;
                                            imgIndex.Add(obj.OmniDocImageIndex, obj.FileName);

                                            string hyperlinkText = $"<a href='javascript:void(0);' onclick='DownloadFile({obj.OmniDocImageIndex},{obj.FileName})'>{dclass.Value}:- {obj.FileName}</a>";


                                            TreeNode3 treeNode2 = new TreeNode3
                                            {
                                                id = obj.OmniDocImageIndex,
                                                icon = "jstree-file",
                                                url = hyperlinkText,
                                                text = hyperlinkText,
                                                isFolder = false
                                            };

                                            mpBasicDetails.children.Add(treeNode2);
                                        }
                                        else if (
                                            dclass.DocSearchParamId == "15"
                                            && dclass.Value == "EE_MPH_CD_Statement"
                                        )
                                        {
                                            //cd_statement = obj.FileName;
                                            basicDetails.cd_statement = obj.FileName;
                                            imgIndex.Add(obj.OmniDocImageIndex, obj.FileName);

                                            string hyperlinkText = $"<a href='javascript:void(0);' onclick='DownloadFile({obj.OmniDocImageIndex},{obj.FileName})'>{dclass.Value}:- {obj.FileName}</a>";

                                            TreeNode3 treeNode2 = new TreeNode3
                                            {
                                                id = obj.OmniDocImageIndex,
                                                icon = "jstree-file",
                                                url = hyperlinkText,
                                                text = hyperlinkText,
                                                isFolder = false
                                            };

                                            mpBasicDetails.children.Add(treeNode2);
                                        }
                                        else if (
                                            dclass.DocSearchParamId == "15"
                                            && dclass.Value == "Receipt_Letter"
                                        )
                                        {
                                            //Receipt_Letter = obj.FileName;
                                            basicDetails.Receipt_Letter = obj.FileName;
                                            imgIndex.Add(obj.OmniDocImageIndex, obj.FileName);

                                            string hyperlinkText = $"<a href='javascript:void(0);' onclick='DownloadFile({obj.OmniDocImageIndex},{obj.FileName})'>{dclass.Value}:- {obj.FileName}</a>";

                                            TreeNode3 treeNode2 = new TreeNode3
                                            {
                                                id = obj.OmniDocImageIndex,
                                                icon = "jstree-file",
                                                url = hyperlinkText,
                                                text = hyperlinkText,
                                                isFolder = false
                                            };

                                            mpBasicDetails.children.Add(treeNode2);
                                        }
                                        else if (
                                            dclass.DocSearchParamId == "15"
                                            && dclass.Value == "Quote_Slip"
                                        )
                                        {
                                            //Quote_Slip = obj.FileName;
                                            basicDetails.Quote_Slip = obj.FileName;
                                            imgIndex.Add(obj.OmniDocImageIndex, obj.FileName);

                                            string hyperlinkText = $"<a href='javascript:void(0);' onclick='DownloadFile({obj.OmniDocImageIndex},{obj.FileName})'>{dclass.Value}:- {obj.FileName}</a>";

                                            TreeNode3 treeNode2 = new TreeNode3
                                            {
                                                id = obj.OmniDocImageIndex,
                                                icon = "jstree-file",
                                                url = hyperlinkText,
                                                text = hyperlinkText,
                                                isFolder = false
                                            };

                                            mpBasicDetails.children.Add(treeNode2);
                                        }
                                        else if (
                                            dclass.DocSearchParamId == "15"
                                            && dclass.Value == "Performa_Invoice"
                                        )
                                        {
                                            //Performa_Invoice = obj.FileName;
                                            basicDetails.Performa_Invoice = obj.FileName;
                                            imgIndex.Add(obj.OmniDocImageIndex, obj.FileName);

                                            string hyperlinkText = $"<a href='javascript:void(0);' onclick='DownloadFile({obj.OmniDocImageIndex},{obj.FileName})'>{dclass.Value}:- {obj.FileName}</a>";

                                            TreeNode3 treeNode2 = new TreeNode3
                                            {
                                                id = obj.OmniDocImageIndex,
                                                icon = "jstree-file",
                                                url = hyperlinkText,
                                                text = hyperlinkText,
                                                isFolder = false
                                            };

                                            mpBasicDetails.children.Add(treeNode2);
                                        }
                                        else if (
                                            dclass.DocSearchParamId == "15" && dclass.Value == "HCN"
                                        )
                                        {
                                            //HCN = obj.FileName;
                                            basicDetails.HCN = obj.FileName;
                                            imgIndex.Add(obj.OmniDocImageIndex, obj.FileName);

                                            string hyperlinkText = $"<a href='javascript:void(0);' onclick='DownloadFile({obj.OmniDocImageIndex},{obj.FileName})'>{dclass.Value}:- {obj.FileName}</a>";

                                            TreeNode3 treeNode2 = new TreeNode3
                                            {
                                                id = obj.OmniDocImageIndex,
                                                icon = "jstree-file",
                                                url = hyperlinkText,
                                                text = hyperlinkText,
                                                isFolder = false
                                            };

                                            mpBasicDetails.children.Add(treeNode2);
                                        }
                                        else if (
                                            dclass.DocSearchParamId == "15" && dclass.Value == "GST"
                                        )
                                        {
                                            //GST = obj.FileName;
                                            basicDetails.GST = obj.FileName;
                                            imgIndex.Add(obj.OmniDocImageIndex, obj.FileName);

                                            string hyperlinkText = $"<a href='javascript:void(0);' onclick='DownloadFile({obj.OmniDocImageIndex},{obj.FileName})'>{dclass.Value}:- {obj.FileName}</a>";

                                            TreeNode3 treeNode2 = new TreeNode3
                                            {
                                                id = obj.OmniDocImageIndex,
                                                icon = "jstree-file",
                                                url = hyperlinkText,
                                                text = hyperlinkText,
                                                isFolder = false
                                            };

                                            mpBasicDetails.children.Add(treeNode2);
                                        }
                                        else if (
                                            dclass.DocSearchParamId == "15" && dclass.Value == "PAN"
                                        )
                                        {
                                            //PAN = obj.FileName;
                                            basicDetails.PAN = obj.FileName;
                                            imgIndex.Add(obj.OmniDocImageIndex, obj.FileName);

                                            string hyperlinkText = $"<a href='javascript:void(0);' onclick='DownloadFile({obj.OmniDocImageIndex},{obj.FileName})'>{dclass.Value}:- {obj.FileName}</a>";

                                            TreeNode3 treeNode2 = new TreeNode3
                                            {
                                                id = obj.OmniDocImageIndex,
                                                icon = "jstree-file",
                                                url = hyperlinkText,
                                                text = hyperlinkText,
                                                isFolder = false
                                            };

                                            mpBasicDetails.children.Add(treeNode2);
                                        }
                                    }
                                }
                                //}

                            }

                            #endregion
                        
                        
                        //TreeData[] endtfolders = new TreeData[endorsementsSet.Count];

                        #region Endorsement_BasicDetails
                        if (endorsementObjects.Count != 0 || endorsementObjects != null)
                        {
                            TreeNode3 endtDet = new TreeNode3
                            {
                                id = "endorsementDetails",
                                text = "Endorsement Details",
                                isFolder = true,
                                icon = "jstree-folder",
                                children = new List<TreeNode3>()
                            };

                            TreeNode3 endorsementFolder = null;
                            TreeNode3 temp_basicDetail = null;
                            TreeNode3 temp_Ecards = null;
                            List<TreeNode3> endorsementFolders = new List<TreeNode3>();
                            int count_1 = 0;
                            foreach (SearchResponse obj in endorsementObjects)
                            {
                                string endorsementNum = obj.DataClassParam.FirstOrDefault(
                                    d => d.DocSearchParamId == "25"
                                )?.Value;

                                if (!string.IsNullOrEmpty(endorsementNum))
                                {
                                    // Check if a folder for this endorsement number already exists
                                    /*TreeNode3 endorsementFolder = endorsementFolders.FirstOrDefault(
                                        f => f.text == endorsementNum
                                    );*/
                                    endorsementFolder = endtDet.children.FirstOrDefault(
                                        f => f.text == endorsementNum
                                    );

                                    if (endorsementFolder == null)
                                    {
                                        // Create a new folder for this endorsement number
                                        endorsementFolder = new TreeNode3
                                        {
                                            id = "endorsementNumber_" + (count_1++.ToString()),
                                            text = endorsementNum,
                                            icon = "jstree-folder",
                                            isFolder = true,
                                            children = new List<TreeNode3>()
                                        };

                                        //endorsementFolders.Add(endorsementFolder);

                                        TreeNode3 basicDetails1 = new TreeNode3
                                        {
                                            id = "basicDetails_" + (count_1++.ToString()),
                                            text = "Policy Details",
                                            icon = "jstree-folder",
                                            isFolder = true,
                                            children = new List<TreeNode3>()
                                        };

                                        TreeNode3 eCards = new TreeNode3
                                        {
                                            id = "eCards_" + (count_1++.ToString()),
                                            text = "ECards",
                                            icon = "jstree-folder",
                                            isFolder = true,
                                            children = new List<TreeNode3>()
                                        };

                                        endorsementFolder.children.Add( basicDetails1 );
                                        endorsementFolder.children.Add(eCards);

                                        endtDet.children.Add(endorsementFolder);
                                    }
                                    // Now, add the documents to this folder

                                    int searchId = 15;

                                    int count = 0;

                                    string searchKey = "DocSearchParamId";
                                    //int searchValue_1 = 15;

                                    JArray jsonArray = JArray.Parse(JsonConvert.SerializeObject(obj.DataClassParam));

                                    JObject result1 = jsonArray.Children<JObject>()
    .FirstOrDefault(objs1 => objs1[searchKey] != null && (int)objs1[searchKey] == 25);

                                    //if ((string)result1["Value"].ToString() == endorsementNum)
                                    //{
                                    JObject result = jsonArray.Children<JObject>()
                    .FirstOrDefault(objs1 => objs1[searchKey] != null && (int)objs1[searchKey] == searchId);

                                        if (result != null)
                                        {
                                            string value = (string)result["Value"];
                                            if (docReq.Contains(value))
                                            {
                                                imgIndex.Add(obj.OmniDocImageIndex, obj.FileName);

                                                //TreeNode3 treeNode2 = new TreeNode3
                                                //{
                                                //    id = obj.OmniDocImageIndex,
                                                //    text = $"{value}:- {obj.FileName}_{endorsementNum}",
                                                //    icon = "jstree-file",
                                                //    isFolder = false
                                                //};
                                            string hyperlinkText = $"<a href='javascript:void(0);' onclick='DownloadFile({obj.OmniDocImageIndex},{obj.FileName})'>{value}:- {obj.FileName}</a>";

                                            TreeNode3 treeNode2 = new TreeNode3
                                            {
                                                id = obj.OmniDocImageIndex,
                                                icon = "jstree-file",
                                                url = hyperlinkText,
                                                text = hyperlinkText,
                                                isFolder = false
                                            };
                                            TreeNode3 temp_endorsementFolder = endtDet.children.FirstOrDefault(f => f.text == endorsementNum);

                                                temp_basicDetail = temp_endorsementFolder.children.FirstOrDefault(
                                            f => f.text == "Policy Details");

                                                temp_basicDetail.children.Add(treeNode2);
                                            }
                                            else if (docReqEcards.Contains(value))
                                            {
                                                imgIndex.Add(obj.OmniDocImageIndex, obj.FileName);
                                            //TreeNode3 treeNode2 = new TreeNode3
                                            //{
                                            //    id = obj.OmniDocImageIndex,
                                            //    text = $"{value}:- {obj.FileName}_{endorsementNum}",
                                            //    icon = "jstree-file",
                                            //    isFolder = false
                                            //};
                                            string hyperlinkText = $"<a href='javascript:void(0);' onclick='DownloadFile({obj.OmniDocImageIndex},{obj.FileName})'>{value}:- {obj.FileName}</a>";

                                            TreeNode3 treeNode2 = new TreeNode3
                                            {
                                                id = obj.OmniDocImageIndex,
                                                icon = "jstree-file",
                                                url = hyperlinkText,
                                                text = hyperlinkText,
                                                isFolder = false
                                            };
                                            TreeNode3 temp_endorsementFolder = endtDet.children.FirstOrDefault(f => f.text == endorsementNum);

                                                temp_Ecards = temp_endorsementFolder.children.FirstOrDefault(
                                            f => f.text == "ECards");

                                                temp_Ecards.children.Add(treeNode2);

                                            }

                                        }
                                    //}



                                    //else
                                    //{
                                    //    continue;
                                    //}
                                }
                            }
                            //endorsementFolder.children.Add(temp_basicDetail);
                            //endorsementFolder.children.Add(temp_Ecards);
                            multiTreeData.myData.Add(mpBasicDetails);
                            multiTreeData.myData.Add(endtDet);

                        }
                            #endregion


                            #endregion
                        }
                        else
                        {
                            Response.Write("<script LANGUAGE='JavaScript' >alert('Received error from underlying service')</script>");
                            isValidatedFlag = false;
                        }
                    }
                }
                else if (selectedParameter == "param2")
                {
                    if (string.IsNullOrEmpty(searchValue))
                    {
                        Response.Write(
                            "<script LANGUAGE='JavaScript' >alert('Please enter Endorsement Number')</script>"
                        );
                        isValidatedFlag = false;
                    }
                    else
                    {
                        request = CreateRequest();
                        reqObj.callApi(url, request, ref response,ref statusCode);
                        //response = response.Replace("200_", "");
                        if(statusCode==200)
                        {
                        resp = new JavaScriptSerializer().Deserialize<Response>(response);
                        endorsement.Ecards = new List<Endt_Ecard>();


                        #region Endorsement Number
                        basicDetails_Endorsement.EE_Endt_Schedule = new List<string>();
                        basicDetails_Endorsement.EE_Endt_Annexure = new List<string>();
                        basicDetails_Endorsement.EE_Endt_CD_Statement = new List<string>();

                        int count_3 = 0;
                        multiTreeData.myData = new List<TreeNode3>();
                        TreeNode3 EndorsementNumberBasicDetails = new TreeNode3
                        {
                            id = "basicDetails",
                            text = "Policy Details",
                            isFolder = true,
                            icon = "jstree-folder",
                            children = new List<TreeNode3>()
                        };

                        TreeNode3 EndorsementNumberFolders = new TreeNode3
                        {
                            id = "endorsementFolder",
                            text = "Endorsement Folder",
                            isFolder = true,
                            icon = "jstree-folder",
                            children = new List<TreeNode3>()
                        };

                        foreach (SearchResponse obj in resp.SearchResponse)
                        {
                            string descrip = obj.Description;
                            if (descrip == "")
                            {
                                foreach (var dclass in obj.DataClassParam)
                                {
                                    if (
                                        dclass.DocSearchParamId == "15"
                                        && dclass.Value == "EE_Endt_Schedule"
                                    )
                                    {
                                        basicDetails_Endorsement.EE_Endt_Schedule.Add(obj.FileName);
                                        imgIndex.Add(obj.OmniDocImageIndex, obj.FileName);

                                        string hyperlinkText = $"<a href='javascript:void(0);' onclick='DownloadFile({obj.OmniDocImageIndex},{obj.FileName})'>{dclass.Value}:- {obj.FileName}</a>";

                                        TreeNode3 treeNode2 = new TreeNode3
                                        {
                                            id = obj.OmniDocImageIndex,
                                            text = hyperlinkText,
                                            icon = "jstree-file",
                                            isFolder = false
                                        };
                                        EndorsementNumberBasicDetails.children.Add(treeNode2);
                                    }
                                    else if (
                                        dclass.DocSearchParamId == "15"
                                        && dclass.Value == "EE_Endt_Annexure"
                                    )
                                    {
                                        basicDetails_Endorsement.EE_Endt_Annexure.Add(obj.FileName);
                                        imgIndex.Add(obj.OmniDocImageIndex, obj.FileName);

                                        string hyperlinkText = $"<a href='javascript:void(0);' onclick='DownloadFile({obj.OmniDocImageIndex},{obj.FileName})'>{dclass.Value}:- {obj.FileName}</a>";

                                        TreeNode3 treeNode2 = new TreeNode3
                                        {
                                            id = obj.OmniDocImageIndex,
                                            text = hyperlinkText,
                                            icon = "jstree-file",
                                            isFolder = false
                                        };
                                        EndorsementNumberBasicDetails.children.Add(treeNode2);
                                    }
                                    else if (
                                        dclass.DocSearchParamId == "15"
                                        && dclass.Value == "EE_Endt_CD_Statement"
                                    )
                                    {
                                        basicDetails_Endorsement.EE_Endt_CD_Statement.Add(
                                            obj.FileName
                                        );
                                        imgIndex.Add(obj.OmniDocImageIndex, obj.FileName);

                                        string hyperlinkText = $"<a href='javascript:void(0);' onclick='DownloadFile({obj.OmniDocImageIndex},{obj.FileName})'>{dclass.Value}:- {obj.FileName}</a>";

                                        TreeNode3 treeNode2 = new TreeNode3
                                        {
                                            id = obj.OmniDocImageIndex,
                                            text = hyperlinkText,
                                            icon = "jstree-file",
                                            isFolder = false
                                        };
                                        EndorsementNumberBasicDetails.children.Add(treeNode2);
                                    }
                                    else if (
                                        dclass.DocSearchParamId == "15"
                                        && dclass.Value == "Endt_Card"
                                    )
                                    {
                                        ecards.Endt_ECards = obj.FileName;
                                        endorsement.Ecards.Add(ecards);
                                        ecards = new Endt_Ecard();
                                        imgIndex.Add(obj.OmniDocImageIndex, obj.FileName);

                                        string hyperlinkText = $"<a href='javascript:void(0);' onclick='DownloadFile({obj.OmniDocImageIndex},{obj.FileName})'>{dclass.Value}:- {obj.FileName}</a>";

                                        TreeNode3 treeNode2 = new TreeNode3
                                        {
                                            id = obj.OmniDocImageIndex,
                                            text = hyperlinkText,
                                            icon = "jstree-file",
                                            isFolder = false
                                        };
                                        EndorsementNumberFolders.children.Add(treeNode2);
                                    }
                                }
                            }
                        }
                        multiTreeData.myData.Add(EndorsementNumberBasicDetails);
                        multiTreeData.myData.Add(EndorsementNumberFolders);
                            #endregion
                        }
                        else
                        {
                            Response.Write("<script LANGUAGE='JavaScript' >alert('Received error from underlying service')</script>");
                            isValidatedFlag = false;
                        }
                    }
                }
                else if (selectedParameter == "param3")
                {
                    if (string.IsNullOrEmpty(searchValue))
                    {
                        Response.Write(
                            "<script LANGUAGE='JavaScript' >alert('Please enter Policy Number')</script>"
                        );
                        isValidatedFlag = false;
                    }
                    else
                    {
                        request = CreateRequest();
                        reqObj.callApi(url, request, ref response,ref statusCode);
                        //response = response.Replace("200_", "");
                        if (statusCode == 200)
                        {


                            resp = new JavaScriptSerializer().Deserialize<Response>(response);
                            endorsement.Ecards = new List<Endt_Ecard>();

                            //Latest Code

                            #region Policy Number

                            TreeNode3 EndorsementNumberFolders_1 = new TreeNode3
                            {
                                id = "endorsementFolder",
                                text = "Endorsement Folder",
                                isFolder = true,
                                icon = "jstree-folder",
                                children = new List<TreeNode3>()
                            };
                            int count_3 = 0;
                            multiTreeData.myData = new List<TreeNode3>();
                            foreach (SearchResponse obj in resp.SearchResponse)
                            {
                                string descrip = obj.Description;
                                if (descrip == "")
                                {
                                    foreach (var dclass in obj.DataClassParam)
                                    {
                                        if (
                                            dclass.DocSearchParamId == "15"
                                            && dclass.Value == "MPH_Ecard"
                                        )
                                        {
                                            basicDetails.MPH_Card = obj.FileName;
                                            imgIndex.Add(obj.OmniDocImageIndex, obj.FileName);

                                            string hyperlinkText = $"<a href='javascript:void(0);' onclick='DownloadFile({obj.OmniDocImageIndex},{obj.FileName})'>{dclass.Value}:- {obj.FileName}</a>";

                                            TreeNode3 treeNode2 = new TreeNode3
                                            {
                                                id = obj.OmniDocImageIndex,
                                                text = hyperlinkText,
                                                icon = "jstree-file",
                                                isFolder = false
                                            };
                                            EndorsementNumberFolders_1.children.Add(treeNode2);
                                        }
                                        else if (
                                            dclass.DocSearchParamId == "15"
                                            && dclass.Value == "Endt_Ecard"
                                        )
                                        {
                                            ecards.Endt_ECards = obj.FileName;
                                            endorsement.Ecards.Add(ecards);
                                            ecards = new Endt_Ecard();
                                            imgIndex.Add(obj.OmniDocImageIndex, obj.FileName);


                                            string hyperlinkText = $"<a href='javascript:void(0);' onclick='DownloadFile({obj.OmniDocImageIndex},{obj.FileName})'>{dclass.Value}:- {obj.FileName}</a>";

                                            TreeNode3 treeNode2 = new TreeNode3
                                            {
                                                id = obj.OmniDocImageIndex,
                                                text = hyperlinkText,
                                                icon = "jstree-file",
                                                isFolder = false
                                            };
                                            EndorsementNumberFolders_1.children.Add(treeNode2);

                                        }
                                    }

                                }
                            }
                            #endregion
                            multiTreeData.myData.Add(EndorsementNumberFolders_1);
                        }
                        else
                        {
                            Response.Write("<script LANGUAGE='JavaScript' >alert('Received error from underlying service')</script>");
                            isValidatedFlag = false;
                        }
                        }
                    
                }
                else if (selectedParameter == "param4")
                {
                    if (string.IsNullOrEmpty(searchValue))
                    {
                        Response.Write(
                            "<script LANGUAGE='JavaScript' >alert('Please enter Employee ID')</script>"
                        );
                        isValidatedFlag = false;
                    }
                    if (string.IsNullOrEmpty(searchValue1))
                    {
                        Response.Write(
                            "<script LANGUAGE='JavaScript' >alert('Please enter Master Policy Number')</script>"
                        );
                        isValidatedFlag = false;
                    }
                    else
                    {
                        request = CreateRequest();
                        reqObj.callApi(url, request, ref response,ref statusCode);
                        //response = response.Replace("200_", "");
                        if (statusCode == 200)
                        {
                            resp = new JavaScriptSerializer().Deserialize<Response>(response);
                            endorsement.Ecards = new List<Endt_Ecard>();

                            TreeNode3 EndorsementNumberFolders_2 = new TreeNode3
                            {
                                id = "endorsementFolder",
                                text = "Endorsement Folder",
                                isFolder = true,
                                icon = "jstree-folder",
                                children = new List<TreeNode3>()
                            };

                            #region Employee Id
                            multiTreeData.myData = new List<TreeNode3>();
                            foreach (SearchResponse obj in resp.SearchResponse)
                            {
                                string descrip = obj.Description;
                                if (descrip == null)
                                {
                                    foreach (var dclass in obj.DataClassParam)
                                    {
                                        if (
                                            dclass.DocSearchParamId == "15"
                                            && dclass.Value == "MPH_Ecard"
                                        )
                                        {
                                            basicDetails.MPH_Card = obj.FileName;
                                            imgIndex.Add(obj.OmniDocImageIndex, obj.FileName);

                                            string hyperlinkText = $"<a href='javascript:void(0);' onclick='DownloadFile({obj.OmniDocImageIndex},{obj.FileName})'>{dclass.Value}:- {obj.FileName}</a>";

                                            TreeNode3 treeNode2 = new TreeNode3
                                            {
                                                id = obj.OmniDocImageIndex,
                                                text = hyperlinkText,
                                                icon = "jstree-file",
                                                isFolder = false
                                            };
                                            EndorsementNumberFolders_2.children.Add(treeNode2);
                                        }
                                        else if (
                                            dclass.DocSearchParamId == "15"
                                            && dclass.Value == "Endt_Ecard"
                                        )
                                        {
                                            ecards.Endt_ECards = obj.FileName;
                                            endorsement.Ecards.Add(ecards);
                                            ecards = new Endt_Ecard();
                                            imgIndex.Add(obj.OmniDocImageIndex, obj.FileName);

                                            string hyperlinkText = $"<a href='javascript:void(0);' onclick='DownloadFile({obj.OmniDocImageIndex},{obj.FileName})'>{dclass.Value}:- {obj.FileName}</a>";

                                            TreeNode3 treeNode2 = new TreeNode3
                                            {
                                                id = obj.OmniDocImageIndex,
                                                text = hyperlinkText,
                                                icon = "jstree-file",
                                                isFolder = false
                                            };
                                            EndorsementNumberFolders_2.children.Add(treeNode2);
                                        }
                                    }

                                }
                            }
                            #endregion
                            multiTreeData.myData.Add(EndorsementNumberFolders_2);
                        }
                        else
                        {
                            Response.Write("<script LANGUAGE='JavaScript' >alert('Received error from underlying service')</script>");
                            isValidatedFlag = false;
                        }
                    }
                }

                //searchResultLabel.Text = searchResult;
                if(isValidatedFlag)
                {
                    searchResultLabel.Visible = true;
                    mpnobj.basicDetails = basicDetails;
                    endorsement.basicDetails = basicDetails_Endorsement;
                    mpnobj.endorsements = new List<Endorsement>();
                    mpnobj.endorsements.Add(endorsement);
                    //string resp12 = new JavaScriptSerializer().Serialize(correctReqStruc);
                    string resp12 = new JavaScriptSerializer().Serialize(multiTreeData);
                    resp1 = new JavaScriptSerializer().Serialize(mpnobj);

                    //XmlDocument xmlDocument = (XmlDocument)JsonConvert.DeserializeXmlNode(
                    //    resp1,
                    //    "root"
                    //);

                    //string filePath = Server.MapPath("~/xmlresponse.xml");
                    //xmlDocument.Save(filePath);

                    string resultantJSON = resp12;
                    Session["resultantJSON"] = resultantJSON;

                    Response.Redirect("ResponsePage.aspx");

                }
                //searchResultLabel.Visible = true;
                //mpnobj.basicDetails = basicDetails;
                //endorsement.basicDetails = basicDetails_Endorsement;
                //mpnobj.endorsements = new List<Endorsement>();
                //mpnobj.endorsements.Add(endorsement);
                ////string resp12 = new JavaScriptSerializer().Serialize(correctReqStruc);
                //string resp12 = new JavaScriptSerializer().Serialize(multiTreeData);
                //resp1 = new JavaScriptSerializer().Serialize(mpnobj);

                ////XmlDocument xmlDocument = (XmlDocument)JsonConvert.DeserializeXmlNode(
                ////    resp1,
                ////    "root"
                ////);

                ////string filePath = Server.MapPath("~/xmlresponse.xml");
                ////xmlDocument.Save(filePath);

                //string resultantJSON = resp12;
                //Session["resultantJSON"] = resultantJSON;

                //Response.Redirect("ResponsePage.aspx");
            }
        }
        protected void clearButton_Click(object sender, EventArgs e)
        {
            DropDownList1.SelectedValue = "param0";
            masterPolicyTextBox.Text = string.Empty;
            PolicyNumbertextbox.Text = string.Empty;
            EmployeeTextbox.Text = string.Empty;
            masterPolicyTextBox.Text = string.Empty;

            Response.Redirect("Searchdocuments.aspx");
        }

        protected void PolicyNumberValidator_ServerValidate(
            object source,
            ServerValidateEventArgs args
        )
        {
            args.IsValid = !string.IsNullOrWhiteSpace(PolicyNumbertextbox.Text);
        }

        


        [WebMethod]
        public static string DownloadFile(string id,string fileName)
        {
            DwdResponse dwdResponse = new DwdResponse();

            string status = string.Empty;

            DownloadResponse downloadResponse = new DownloadResponse();

            string url = ConfigurationManager.AppSettings["DownloadAPIUrl"];

            //string url = "https://abhiutilityuat.adityabirlahealth.com/OmniDocsSrchDwldAPI/Service.svc/OmniDocsDownloadAPI";
            string response = string.Empty;

            string pattern = @"DownloadFile\(\d+,(.*?)\)";

            Match match = Regex.Match(fileName, pattern);

            fileName = match.Groups[1].Value;

            Searchdocuments searchdocuments = new Searchdocuments();

            string request = searchdocuments.CreateDwdRequest(id,fileName);

            APIRequest aPIRequest = new APIRequest();

            //aPIRequest.callApi(url, request, ref response);
            //response = response.Replace("200_", "");
            //dwdResponse = new JavaScriptSerializer().Deserialize<DwdResponse>(response);
            dwdResponse = JsonConvert.DeserializeObject<DwdResponse>(response); 
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
                Console.WriteLine("File created successfully in the Downloads folder.");
                Searchdocuments searchdocuments1 = new Searchdocuments();
                Page currentPage = HttpContext.Current.Handler as Page;
                if (currentPage != null)
                {
                    searchdocuments1.MsgBox("File created successfully in the Downloads folder.", currentPage,currentPage );
                    HttpContext.Current.Response.Write("File created successfully in the Downloads folder.");
                }
                status = "File created successfully in the Downloads folder.";
                return status;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                Searchdocuments searchdocuments1 = new Searchdocuments();
                //Page currentPage = HttpContext.Current.Handler as Page;
                //if (currentPage != null)
                //{
                //    searchdocuments1.MsgBox("File created successfully in the Downloads folder.", currentPage, currentPage);
                //    currentPage.Response.Write("File created successfully in the Downloads folder.");
                //}
                status = "An error occurred: " + ex.Message;
                return status;
            }
        }

        public string CreateDwdRequest(string id, string fileName)
        {
            //string fileName = imgIndex[id];

            DownloadRequest request = new DownloadRequest();

            DwdRequest dwdRequest = new DwdRequest();

            List<DownloadRequest> DownloadRequest1 = new List<DownloadRequest>();

            request.GlobalId = "";

            request.FileName = fileName;

            request.OmniDocImageIndex = id;

            DownloadRequest1.Add(request);

            dwdRequest.Identifier = "ByteArray";

            dwdRequest.SourceSystemName = "CRM";

            dwdRequest.DownloadRequest = DownloadRequest1;

            string req = new JavaScriptSerializer().Serialize(dwdRequest);

            return req;
        }


        // Method for displaying a message box
        public void MsgBox(String ex, Page pg, Object obj)
        {
            string s = "<script lanugage='javascript'>alert('" + ex.Replace("\r\n", "\\n").Replace("'", "") + "');</script>";
            Type cstype = obj.GetType();

            ClientScriptManager cs = pg.ClientScript;
            cs.RegisterClientScriptBlock(cstype, s, s.ToString());
        }

    }
}