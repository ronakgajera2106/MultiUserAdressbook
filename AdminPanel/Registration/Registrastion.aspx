    <%@ Page Language="C#" AutoEventWireup="true" CodeFile="Registrastion.aspx.cs" Inherits="AdminPanel_Registration_Registrastion" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <link href="../../content/css/bootstrap-theme.min.css" rel="stylesheet" />
    <link href="../../conte>nt/css/bootstrap.min.css" rel="stylesheet" />
    <script src="../../content/js/bootstrap.bundle.min.js"></script>


    <title>Registration Account</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid">
        <div class="row">
            <div class ="text-center" >
                <h2 class="text-center">SignUp For New User</h2>
                <br />
                    <div class="row">
                         <div class="col-md-12">
                             <asp:Label ID="lblMessage" runat="server" ForeColor="Red" />
                        </div>
                    </div>
                    <br />

                 <div class="row">
                     <div class="col-md-3">

                    </div>
                     <div class="col-md-1">
                         UserName :
                     </div>
                     <div class="col-md-4">
                        <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control" />
                     </div>
                </div>
                    <br />

                 <div class="row">
                     <div class="col-md-3">

                    </div>
                     <div class="col-md-1">
                         Password :
                     </div>
                     <div class="col-md-4">
                        <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" />
                     </div>
                </div>
                    <br />

                <div class="row">
                     <div class="col-md-3">

                    </div>
                     <div class="col-md-1">
                         Contact No :
                     </div>
                     <div class="col-md-4">
                        <asp:TextBox ID="txtContactno" runat="server" CssClass="form-control"></asp:TextBox>
                     </div>
                </div>
                    <br />

                <div class="row">
                     <div class="col-md-3">

                    </div>
                     <div class="col-md-1">
                         Email :
                     </div>
                     <div class="col-md-4">
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                     </div>
                </div>
                    <br />

                <div class="row">
                     <div class="col-md-3">

                    </div>
                     <div class="col-md-1">
                         Display Name :
                     </div>
                     <div class="col-md-4">
                       <asp:TextBox ID="txtDisplayName" runat="server" CssClass="form-control"></asp:TextBox>
                     </div>
                </div>
                    <br />

                <div class="row">                   
                    <div class="text-center">
                        <asp:Button runat="server" ID="btnSignup" Text="Sign Up" CssClass="btn btn-success"  Font-Bold="True" OnClick="btnSignup_Click"/>
                     </div>                  
                </div>

                <br />
                <div class="row">                    
                    <div class="text-center">
                    Go Back Login Page |&nbsp; <asp:HyperLink runat="server" ID="hyplinkGBLogin" Text="Login Page" NavigateUrl="~/AdminPanel/Login/Login.aspx" Font-Bold="True" Font-Size="Medium" CssClass="text-center"/>
                 </div>
                    </div>

            </div>
        </div>
    </div>
    </form>
</body>
</html>
