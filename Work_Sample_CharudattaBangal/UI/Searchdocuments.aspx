<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Searchdocuments.aspx.cs" Inherits="omnisearch.Searchdocuments" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head >
   

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

        .auto-style1 {
            width: 285px;
        }

        .auto-style6 {
            width: 372px;
            height: 132px;
        }
        .auto-style7 {
            width: 182px;
        }
        .auto-style8 {
            width: 182px;
            height: 26px;
        }
        .auto-style9 {
            width: 285px;
            height: 26px;
        }
    </style>
       <%-- </asp:Content>--%>
</head>
<body>
     
         <header>
            <nav  style="background-color: #cc2321;">

        <div class="limiter">
        <%--<div id="header-con">--%>
            <div class="Company-logo">
            
                    <img src="Images/Birla_Head.png" alt="Aditya Birla Health" id="empclientheader"  runat="server"  style="width:100%; height:117px" />
                      <div style="background-color: #cc2321;">

                </div>
                </div>
            </div>
              
    
        
         <%--<br />
        <br />--%>

                     
                </nav>
             </header>
    

   
    <form id="form1" runat="server" style="max-height : 100%; height:auto;">
        <div class="container-fluid light-bg row align-content-lg-center" style="background-color:#fff !important;","text-align:center;" >
            <br />
        
            <h2 align="center">Search Document</h2>
            <div align="center">
                <table class="auto-style6">
                    <tr>
                        <td class="auto-style8">
                                                      
                            <b />
          <label style="float:left; padding-right:10px; width: 236px; height: 36px;">Search Type :</label>
                             <td class="auto-style9">

    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True"  Height="35px" Width="221px" OnSelectedIndexChanged="parameterDropdown_SelectedIndexChanged">
       <asp:ListItem Value ="param0" Selected="True">Select</asp:ListItem> 
    <asp:ListItem Value="param1">MasterPolicy Number</asp:ListItem>
    <asp:ListItem Value="param2">Endorsement Number</asp:ListItem>
    <asp:ListItem Value="param3">Policy Number</asp:ListItem>
    <asp:ListItem Value="param4">Employee ID</asp:ListItem>
       
</asp:DropDownList>
                                  </td>
                            </td>
                    </tr>
                    <tr>
                        
                        <td class="auto-style7">
                            <br />
                            <b />
                           <%-- Height="38px" Width="257px"--%>
                          <%--  <br />--%>
                            <br />
                         <asp:Label ID="masterPolicyLabel" runat="server" Text="Enter Master Policy:" Visible="False" Width="300px"  ></asp:Label>
                           
                            <br />
                           
                   <asp:Label ID="EndorsementLabel" runat="server"  Text="Enter Endorsement Number:" Visible="False" Width="300px" ></asp:Label>

              <asp:Label ID="policynumberLabel" runat="server"  Text="Enter Policy Number:" Visible="False" Width="300px" ></asp:Label>
                            <%--Height="52px" Width="296px"--%>

                          <%--  <br />--%>
                            <br /><br />
            <asp:Label ID="Employeelabel" runat="server"  Text="Enter Employee ID:" Visible="False" Width="300px" ></asp:Label>
            
              <%--Height="34px" Width="273px"--%>
        <%--  <br />--%>
          
                           <%-- Height="43px" Width="272px"--%>
                  <%--  <br />     --%> 

                            
             
                            
                  
                                               
             
                            
                 
                                               
             
                            
                                </td>
                        <td class="auto-style1">
                                                      
                            <b />
            <asp:TextBox ID="masterPolicyTextBox" Height="44px" Width="230px" runat="server" style="float:right; padding-left:-10px;" class="form-control" Visible="False"></asp:TextBox>
        
            <asp:TextBox ID="EndorsementTextbox" runat="server" Visible="False" Height="44px" Width="230px" ></asp:TextBox>            <b />

            <asp:TextBox ID="PolicyNumbertextbox" runat="server" Visible="False" Height="44px" Width="230px"></asp:TextBox>
            <asp:TextBox ID="EmployeeTextbox" runat="server" Visible="False" Width="230px" Height="44px"></asp:TextBox>

  

           
     
 
                              </td>
                    </tr>
                    <tr>
                        <td class="auto-style7">

           <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="masterPolicyTextBox" ErrorMessage="Please enter the Master Policy number." ValidationGroup="SearchValidation"></asp:RequiredFieldValidator>
<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="EndorsementTextbox" ErrorMessage="Please enter the Endorsement number." ValidationGroup="SearchValidation"></asp:RequiredFieldValidator>
<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="PolicyNumbertextbox" ErrorMessage="Please enter the Policy number." ValidationGroup="SearchValidation"></asp:RequiredFieldValidator>
<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="EmployeeTextbox" ErrorMessage="Please enter the Employee ID." ValidationGroup="SearchValidation"></asp:RequiredFieldValidator>
 <br />--%>
            </td>
                        
          
</tr>

            


          </table> 



       <asp:Label ID="searchResultLabel" runat="server" Visible="False"></asp:Label>    
<asp:Button ID="searchButton" runat="server" class="button button3"  Width="200px" Height="39px"  Text="Search" OnClick="searchButton_Click" style ="" />
                        
<asp:Label ID="clearResultLabel" runat="server" Visible="false"></asp:Label>
                <asp:Button ID="ClearButton" runat="server" class="button button3" Width="200px" Height="39px" OnClick="clearButton_Click" Visible="False" style="" Text="Clear" />



       <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

          </div> 

        <br /> <br />
        <br /> <br />
        <br />
            <br />

        </div>
        


        <p>
            &nbsp;</p>
    </form>
     <footer class="bg-light text-center text-lg-start">
  <!-- Copyright -->
          <%--class="text-center p-3" --%>
  <div style="background-color: #cc2321; align-content:center; justify-content:center; color:white; width:auto; height:93px;">
    <b></b>
   <%-- <a class="text-dark" href=""></a>--%>
    <p>  © 2023 Copyright </p>
  </div>
  <!-- Copyright -->
</footer>  
</body>
   
    <script >
        function showAlert(message)
        {
            alert(message);
        } 
       
    </script>
   <%-- <script>
    function pageLoad() {
        var policyNumberTextbox = document.getElementById('<%= PolicyNumbertextbox.ClientID %>');
        var policyNumberValidator = document.getElementById('<%= PolicyNumbertextboxValidator.ClientID %>');

        if (policyNumberTextbox.value.trim() === '') {
            policyNumberValidator.style.display = 'inline';
        }
    }
    </script>--%>
</html>
