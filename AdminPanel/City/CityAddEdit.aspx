<%@ Page Title="" Language="C#" MasterPageFile="~/content/multiAdressbook.master" AutoEventWireup="true" CodeFile="CityAddEdit.aspx.cs" Inherits="WebPages_City_CityAddEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .form-select:focus {
            border: 1px solid red;
            box-shadow: none;
        }

        .form-control:focus {
            box-shadow: none;
            border: 1px solid red;
        }
    </style>
    <title>AddressBook | City Add/Edit
    </title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div runat="server" ID="CityEdit">
        <div class="container mt-3 divCityEdit">
            <svg xmlns="http://www.w3.org/2000/svg" style="display: none;">
                <symbol id="exclamation-triangle-fill" fill="currentColor" viewBox="0 0 16 16">
                    <path d="M8.982 1.566a1.13 1.13 0 0 0-1.96 0L.165 13.233c-.457.778.091 1.767.98 1.767h13.713c.889 0 1.438-.99.98-1.767L8.982 1.566zM8 5c.535 0 .954.462.9.995l-.35 3.507a.552.552 0 0 1-1.1 0L7.1 5.995A.905.905 0 0 1 8 5zm.002 6a1 1 0 1 1 0 2 1 1 0 0 1 0-2z" />
                </symbol>
            </svg>

            <div runat="server" ID="Exception" Visible="false" CssClass="alert alert-danger d-flex align-items-center" role="alert">
                <div class="container">
                    <svg class="bi flex-shrink-0 me-2" width="24" height="24" role="img" aria-label="Danger:">
                        <use xlink:href="#exclamation-triangle-fill" />
                    </svg>
                    <asp:Label ID="lblCatchMessage" runat="server" Text="" CssClass="" />
                </div>
            </div>

            <div runat="server" ID="FormSection" Visible="true" class="row">
                <div class="mb-2 mt-3" style="width: 90%;">
                    <h4>
                        <asp:Label ID="lblPageTitle" runat="server" />
                    </h4>
                </div>

                <div class="mb-3" style="width: 90%;">
                    <asp:HyperLink runat="server" ID="btnEditCity" NavigateUrl="~/AdminPanel/City/City.aspx" Text="Go to View Page" CssClass="btn btn-success btn-sm" />
                </div>
                <div class="container">
                    <div class="row">
                        <hr />
                       
                            <div class="mb-3">
                                <asp:TextBox runat="server" ID="txtCityName" placeholder="City Name" CssClass="form-control"  />
                            </div>
                            <div class="mb-3">
                                <asp:TextBox runat="server" ID="txtStdCode" placeholder="Std Code" CssClass="form-control" />
                            </div>
                       
                       
                            <div class="mb-3">
                                <asp:DropDownList ID="ddlStateList" CssClass="form-select" runat="server"></asp:DropDownList>
                            </div>
                            <div class="mb-3">
                                <asp:TextBox runat="server" ID="txtPinCode" placeholder="Pin Code" CssClass="form-control" />
                            </div>
                
                        <div class="mb-3">
                            <asp:Label runat="server" ID="lblErrorMessage" CssClass="text-dark" Font-Bold="true" />
                        </div>
                        <div class="mb-3">
                            <asp:Button runat="server" ID="btnSubmitCity" CssClass="btn btn-danger" OnClick="btnSubmitCity_Click" />
                        </div>
                        <div class="mb-3">
                            <asp:Label runat="server" ID="lblSuccessMessage" CssClass="text-success" Font-Bold="true" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

