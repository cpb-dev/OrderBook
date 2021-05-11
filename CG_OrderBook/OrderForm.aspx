<%@ Page Title="Order Form" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OrderForm.aspx.cs" Inherits="CG_OrderBook.OrderForm" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <h1 class="text-center mx-auto">Order Form</h1>

    <section id="main-content">
        <section id="wrapper">
        
            <div class="container">
                <div class="row">
                    <div class="col-md-4 col-sm-6">
                        <h2 class="text-center mx-auto">Customer Information</h2>
                        
                            <div class="col-md-6">
                                <label class="form-label">First Name:</label>
                                <asp:TextBox placeholder="First Name" id="txtCustFName" runat="server" />
                            </div>
                            <div class="col-md-6">
                                <label class="form-label">Surname:</label>
                                <asp:TextBox placeholder="Surname" id="txtCustSName" runat="server" />
                            </div>
                            <div class="col-md-12">
                                <label class="form-label">Phone Number:</label>
                                <asp:TextBox placeholder="Phone Number" id="txtPhone" runat="server" />
                            </div>
                            <div class="col-md-12">
                                <label class="form-label">Email Address:</label>
                                <asp:TextBox placeholder="Email Address" id="txtEmail" runat="server" />
                            </div>
                    </div>

                    <div class="col-md-4 col-sm-6">
                        <h2 class="text-center mx-auto">Product Information</h2>
                            <div class="col-md-12">
                                <label class="form-label">Item Name:</label>
                                <asp:TextBox placeholder="Item Name" id="txtItem" runat="server" />
                            </div>
                            <div class="col-md-6">
                                <label class="form-label">Brand:</label>
                                <asp:DropDownList ID="dlBrand" runat="server" >
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-6">
                                <label class="form-label">Style Code:</label>
                                <asp:TextBox placeholder="Style Code" id="txtSCode" runat="server" />
                            </div>
                            <div class="col-md-12">
                                <label class="form-label">Specifications:</label>
                                <asp:TextBox placeholder="Item Spec" id="txtSpec" runat="server" />
                            </div>

                    </div>

                    <div class="col-md-4 col-sm-6">
                        <h2 class="text-center mx-auto">Order Information</h2>
                            <div class="col-md custom-control-inline">
                                <label class="custom-control-label" for="cbPaid">Paid?</label>
                                <asp:CheckBox id="cbPaid" runat="server" />
                            </div>
                            <div class="col-md-12">
                                <label class="form-label">Order Status</label>
                                <asp:DropDownList ID="dlOrderStatus" runat="server" >
                                    <asp:ListItem>Not Ordered Yet</asp:ListItem>
                                    <asp:ListItem>Ordered</asp:ListItem>
                                    <asp:ListItem>Received</asp:ListItem>
                                    <asp:ListItem>Received - Customer Notified</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-12">
                                <label class="form-label">Notes</label>
                                <asp:TextBox placeholder="Order Notes" id="txtOrderNotes" runat="server" />
                            </div>

                        <asp:Label ID="ErrorMsg" runat="server" Text="" Visible="false" />
                        <br />
                        <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="Save" OnClick="btnSave_Click" />
                        <asp:Button ID="btnDelete" class="btn btn-primary" runat="server" Text="Delete" OnClick="btnDelete_Click" />
                        
                    </div>
                </div>
            </div>

        </section>
    </section>

</asp:Content>