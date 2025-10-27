<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResponsePage.aspx.cs" Inherits="omnisearch.ResponsePage" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <%-- <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>--%>
    <%--<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/jstree/3.3.11/themes/default/style.min.css" />--%>
<%--<script src="https://cdnjs.cloudflare.com/ajax/libs/jstree/3.3.11/jstree.min.js"></script>--%>

     <script src="/js/jquery-3.6.0.min.js"></script>
   <link rel="stylesheet" href="/css/a.css" />
    <script src="/Scripts/jstree.min.js"></script>
   
    <title>Omnidocsearch</title>

    <style type="text/css">
        #commonStyles {
            width: 100%;
        }
    </style>

    <style type="text/css">
        .container {
            padding: 16px;
        }

        h2 {
            text-align: center;
            color: black;
        }

        body {
            font-family: Arial, Helvetica, sans-serif;
        }

        form {
            border: 3px solid #f1f1f1;
        }

        input[type=text], input[type=password] {
            padding: 12px 50px;
            margin: 31px 0 8px 0;
            display: inline-block;
            border: 1px solid #ccc;
            box-sizing: border-box;
        }

        .button {
            background-color: #4CAF50; /* Green */
            border: none;
            color: white;
            padding: 6px 60px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: 16px;
            margin: 4px 2px;
            transition-duration: 0.4s;
            cursor: pointer;
            width: 27%;
        }

        .button1 {
            background-color: #CC2321;
            color: black;
            border: 2px solid #96053c;
            border-radius: 12px;
            width: 27%;
        }

            .button1:hover {
                background-color: #CC2321;
                color: white;
            }

        .button3 {
            background-color: #CC2321;
            color: white;
            border: 2px solid #96053c;
            border-radius: 12px;
            width: 27%;
        }

            .button3:hover {
                background-color: white;
                color: black;
            }
        /* Styling for the treeview nodes */
        .treeview .node {
            position: relative;
            padding-left: 20px;
            margin-bottom: 5px;
        }

        /* Styling for the treeview expand/collapse icons */
        .treeview .icon {
            position: absolute;
            left: 0;
            top: 0;
            width: 16px;
            height: 16px;
            background-color: #ccc;
        }

        /* Styling for the treeview expand icon */
        .treeview .expand-icon:before {
            content: '+';
            display: inline-block;
            transform: rotate(45deg);
            text-align: center;
            line-height: 16px;
        }

        /* Styling for the treeview collapse icon */
        .treeview .collapse-icon:before {
            content: '-';
            display: inline-block;
            text-align: center;
            line-height: 16px;
        }

        /* Styling for the treeview leaf nodes */
        .treeview .leaf-node {
            margin-left: 20px;
        }
    </style>
    <style>
        table {
            border-collapse: collapse;
        }

        th, td {
            border: 1px solid #ddd;
            padding: 8px;
        }
    </style>
</head>
<body>

    <header>
        <nav style="background-color: #cc2321;">

            <div class="limiter">
                <%--<div id="header-con">--%>
                <div class="Company-logo">

                    <img src="Images/Birla_Head.png" alt="Aditya Birla Health" id="empclientheader" runat="server" style="width: 100%; height: 117px" />
                    <div style="background-color: #cc2321;">
                    </div>
                </div>
            </div>
        </nav>
    </header>
    <form id="form1" runat="server">
    <asp:HiddenField ID="HiddenField1" runat="server" ClientIDMode="Static" />
    <div id="tree"></div>
<%--    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jstree/3.3.11/jstree.min.js"></script>--%>
    <script src="/js/jquery-3.6.0.min.js"></script>
    <script src="/Scripts/jstree.min.js"></script>
  
        <%--<script>
            $(document).ready(function () {
                var jsonDataString = document.getElementById("HiddenField1").value;
                console.log(jsonDataString);
                try {
                    var jsonData = JSON.parse(jsonDataString);
                    console.log(jsonData);
                    $('#tree').jstree({
                        "multiple": true,
                        "animation": 1,
                        'core': {
                            'data': jsonData.myData
                        }
                    });
                } catch (e) {
                    console.error("Error parsing JSON data:", e);
                }

                // AJAX call to invoke server-side C# method
                $('#tree').on('select_node.jstree', function (e, data) {
                    var selectedNode = data.node;
                    var nodeId = selectedNode.id;
                    var fileName = selectedNode.text;
                    $.ajax({
                        type: "POST",
                        url: "ResponsePage.aspx/DownloadFile", // Replace with your ASPX page and method name
                        data: JSON.stringify({ id: nodeId, fileName: fileName }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            console.log(response.d);
                            if (response.d === "Success") {
                                //alert("File created successfully in the Downloads folder.");
                                alert("File downloaded successfully. Please check in Download folder.");
                                console.log(response.d);
                                console.log("In if");
                            } else {
                                console.log("In else");
                                alert(response.d); // Display the error message
                                console.log(response.d);
                            }
                        },
                        error: function (xhr, status, error) {
                            alert("An error occurred while processing your request.");
                            //alert(response.d);
                            console.log("In error");
                        }
                    });
                });
            });
        </script>--%>


        <script>
            $(document).ready(function () {
                var jsonDataString = document.getElementById("HiddenField1").value;
                console.log(jsonDataString);
                try {
                    var jsonData = JSON.parse(jsonDataString);
                    console.log(jsonData);
                    $('#tree').jstree({
                        "multiple": true,
                        "animation": 1,
                        'core': {
                            'data': jsonData.myData
                        }
                    });
                } catch (e) {
                    console.error("Error parsing JSON data:", e);
                }

                // AJAX call to invoke API
                $('#tree').on('select_node.jstree', function (e, data) {
                    var selectedNode = data.node;
                    var nodeId = selectedNode.id;
                    var fileName = selectedNode.text;

                    var apiRequest = {
                        DownloadRequest: [{
                            GlobalId: "",
                            OmniDocImageIndex: nodeId,
                            FileName: fileName
                        }],
                        Identifier: "ByteArray",
                        SourceSystemName: "CRM"
                    };

                    $.ajax({
                        type: "POST",
                        url: "https://abhiutilityuat.adityabirlahealth.com/OmniDocsSrchDwldAPI/Service.svc/OmniDocsDownloadAPI", // Replace with your API endpoint
                        data: JSON.stringify(apiRequest),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (apiResponse) {
                            console.log(apiResponse);

                            var downloadResponse = apiResponse.DownloadResponse[0];

                            if (downloadResponse.Error[0].Code === "0") {
                                var byteArray = downloadResponse.ByteArray;
                                var fileType = getFileTypeFromFileName(fileName);

                                // Convert byteArray to Blob
                                var blob = new Blob([base64ToArrayBuffer(byteArray)], { type: fileType });

                                // Create a link element and trigger a download
                                var link = document.createElement('a');
                                link.href = URL.createObjectURL(blob);
                                link.download = fileName;
                                link.click();
                            } else {
                                console.log("Error:", downloadResponse.Error[0].Description);
                                alert("An error occurred: " + downloadResponse.Error[0].Description);
                            }
                        },
                        error: function (xhr, status, error) {
                            alert("An error occurred while processing your request.");
                            console.log("In error");
                        }
                    });
                });
            });

            function getFileTypeFromFileName(fileName) {
                // Implement logic to determine file type based on the file name
                // For example, you can check the file extension
                var fileExtension = fileName.split('.').pop();
                if (fileExtension === 'pdf') {
                    return 'application/pdf';
                } else {
                    // Add more file type checks as needed
                    return 'application/octet-stream';
                }
            }

            function base64ToArrayBuffer(base64) {
                var binaryString = window.atob(base64);
                var len = binaryString.length;
                var bytes = new Uint8Array(len);
                for (var i = 0; i < len; i++) {
                    bytes[i] = binaryString.charCodeAt(i);
                }
                return bytes.buffer;
            }
        </script>

</form>
</body>
</html>
