<%@ Page Title="" Language="C#" MasterPageFile="~/content/multiAdressbook.master" AutoEventWireup="true" CodeFile="Contact.aspx.cs" Inherits="AdminPanel_Contact_Contact" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>AddressBook | Contact View
    </title>
    <style>
        .ContactShow .row {
            --bs-gutter-x: none;
        }

        .ContactShow .col-md-12 > div {
            overflow: auto !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div ID="ContactShow" runat="server">
        <div class="container-fluid mt-3 ContactShow">
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
            <div runat="server" ID="MainContent" Visible="true" class="row justify-content-center">
                <div class="mb-2 mt-3" style="width: 90%;">
                    <h4>Contact Page Data</h4>
                </div>
                <div class="mb-3" style="width: 90%;">
                    <asp:HyperLink runat="server" ID="btnEditContact" NavigateUrl="~/AdminPanel/Contact/ContactAddEdit.aspx" Text="Go to Add/Edit Page" CssClass="btn btn-success btn-sm" />
                </div>
                <div class="col-md-12 mt-3">
                    <asp:GridView ID="gvContactShow" runat="server" CellPadding="4" EmptyDataText="Null" HorizontalAlign="Center" OnRowCommand="gvContactShow_RowCommand" Width="90%" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellSpacing="2" ForeColor="Black">
                        <FooterStyle BackColor="#CCCCCC" />
                        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
                        <RowStyle BackColor="White" />
                        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                        <SortedAscendingHeaderStyle BackColor="#808080" />
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#383838" />

                        <Columns>
                            
                            <asp:TemplateField HeaderText="Delete">
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnDeleteContact" Text="Delete" CssClass="btn btn-danger btn-sm" CommandName="DeleteRecord" CommandArgument='<%# Eval("ContactID").ToString() %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Edit">
                                <ItemTemplate>
                                    <asp:HyperLink runat="server" ID="btnEditContact" Text="Edit" CssClass="btn btn-warning btn-sm" CommandName="EditRecord" NavigateUrl='<%# "~/AdminPanel/Contact/ContactAddEdit.aspx?ContactID=" + Eval("ContactID").ToString().Trim() %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>

                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

