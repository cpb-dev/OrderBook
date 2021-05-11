<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CG_OrderBook._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Order Book</h2>

    <asp:Label id="lblSearch" runat="server" Text="Search By:" />
    <%-- Drop down list for the user to choose search method (default being time) --%>
    <asp:DropDownList ID="dlSearchBy" runat="server" OnSelectedIndexChanged="dlSearchBy_SelectedIndexChanged" AutoPostBack="true">
        <asp:ListItem Value="0">Date</asp:ListItem>
        <asp:ListItem Value="1">Name</asp:ListItem>
        <asp:ListItem Value="2">Order Status</asp:ListItem>
        <asp:ListItem Value="3">Brand</asp:ListItem>
        <asp:ListItem Value="4">Paid</asp:ListItem>
        <asp:ListItem Value="5">Style Code</asp:ListItem>
    </asp:DropDownList>

    <br />
    <%-- All the different search criterias for the user to choose from using the drop down --%>
    <asp:TextBox id="tbSearch" runat="server" PlaceHolder="Search" Visible="false" />
    <asp:TextBox id="tbFName" runat="server" PlaceHolder="First Name" Visible="false" />
    <asp:TextBox id="tbSName" runat="server" PlaceHolder="Surname" Visible="false" />
    <asp:TextBox ID="tbDate" runat="server"  TextMode="Date"  placeholder="Date dd/MM/yyyy"></asp:TextBox>
    <asp:DropDownList ID="dlBrands" runat="server" Visible="false"></asp:DropDownList>
    <asp:DropDownList ID="dlPaid" runat="server" Visible="false">
        <asp:ListItem Value="Yes">Yes</asp:ListItem>
        <asp:ListItem Value="No">No</asp:ListItem>
    </asp:DropDownList>
    <asp:DropDownList ID="dlStatus" runat="server" Visible="false">
        <asp:ListItem Value="0">Not Ordered Yet</asp:ListItem>
        <asp:ListItem Value="1">Ordered</asp:ListItem>
        <asp:ListItem Value="2">Received</asp:ListItem>
        <asp:ListItem Value="3">Received - Customer Notified</asp:ListItem>
    </asp:DropDownList>
    <asp:Button ID="btnSearch" runat="server" Text="Search" Class="btn btn-primary" OnClick="btnSearch_Click" />
    <asp:Button ID="btnRefresh" runat="server" Text="Refresh" Class="btn btn-primary" OnClick="btnRefresh_Click" />
    <br />
    <br />

    <asp:GridView ID="gvOrders" runat="server" 
        Width="80%" AutoGenerateColumns="False" 
        DataKeyItems="Order_ID" DataSourceID="SqlDataSourceOrderBook" 
        CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="Order_ID" AllowSorting="True"
        ShowHeaderWhenEmpty="true" EmptyDataText="No Orders Found Yet!">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="Order_ID" HeaderText="Order_ID" SortExpression="Order_ID" ReadOnly="true" Visible="False" />
            <asp:BoundField DataField="OrderDate" HeaderText="Date" SortExpression="OrderDate" ReadOnly="true" />
            <asp:BoundField DataField="F_Name" HeaderText="First Name" ReadOnly="true" />
            <asp:BoundField DataField="S_Name" HeaderText="Surname" SortExpression="S_Name" ReadOnly="true" />
            <asp:BoundField DataField="Product_Name" HeaderText="Item" SortExpression="Product_Name" ReadOnly="true" />
            <%-- Brand field is hidden on smaller screens to free up space --%>
            <asp:BoundField DataField="Brand" HeaderText="Brand" ItemStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs"  SortExpression="Brand" ReadOnly="true" />
            <asp:BoundField DataField="Style_Code" HeaderText="Style Code" ReadOnly="true" />
            <asp:BoundField DataField="OrderStatus" HeaderText="Status" SortExpression="OrderStatus" />
            <asp:BoundField DataField="Paid" HeaderText="Paid?" SortExpression="Paid" />
            <asp:CommandField ShowEditButton="True" />
        </Columns>
        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
        <SortedAscendingCellStyle BackColor="#FDF5AC" />
        <SortedAscendingHeaderStyle BackColor="#4D0000" />
        <SortedDescendingCellStyle BackColor="#FCF6C0" />
        <SortedDescendingHeaderStyle BackColor="#820000" />
    </asp:GridView>
    <asp:SqlDataSource 
        ID="SqlDataSourceOrderBook" runat="server" 
        ConnectionString="<%$ ConnectionStrings:OrderDBConnectionString %>" 
        SelectCommand="GetOrderInfo" 
        UpdateCommand="UPDATE order_info SET [Paid] = @Paid, [OrderStatus] = @OrderStatus WHERE [Order_ID] = @Order_ID"
        SelectCommandType="StoredProcedure">
        <UpdateParameters>
            <asp:Parameter Name="Paid" />
            <asp:Parameter Name="OrderStatus" />
            <asp:Parameter Name="Order_ID" />
        </UpdateParameters>
    </asp:SqlDataSource>

    <asp:GridView ID="gvSearchOrder" runat="server" 
        ShowHeaderWhenEmpty="True" EmptyDataText="No Orders Found!" 
        Visible="False"
        Width="80%" AutoGenerateColumns="False"
        CellPadding="4" ForeColor="#333333" 
        GridLines="None" AllowSorting="True"
        DataKeyNames="Order_ID" 
        OnRowEditing="gvSearchOrder_RowEditing" OnRowCancelingEdit="gvSearchOrder_RowCancelingEdit" OnRowUpdating="gvSearchOrder_RowUpdating">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="Order_ID" HeaderText="Order_ID" SortExpression="Order_ID" ReadOnly="true" Visible="False" />
            <asp:BoundField DataField="OrderDate" HeaderText="Date" SortExpression="OrderDate" ReadOnly="true" />
            <asp:BoundField DataField="F_Name" HeaderText="First Name" ReadOnly="true" />
            <asp:BoundField DataField="S_Name" HeaderText="Surname" SortExpression="S_Name" ReadOnly="true" />
            <asp:BoundField DataField="Product_Name" HeaderText="Item" SortExpression="Product_Name" ReadOnly="true" />
            <asp:BoundField DataField="Brand" HeaderText="Brand" SortExpression="Brand" ReadOnly="true" />
            <asp:BoundField DataField="Style_Code" HeaderText="Style Code" ReadOnly="true" />
            <asp:BoundField DataField="OrderStatus" HeaderText="Status" SortExpression="OrderStatus" />
            <asp:BoundField DataField="Paid" HeaderText="Paid?" SortExpression="Paid" />
            <asp:CommandField ShowEditButton="True" />
        </Columns>
        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White"  HorizontalAlign="Center" />
        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
        <SortedAscendingCellStyle BackColor="#FDF5AC" />
        <SortedAscendingHeaderStyle BackColor="#4D0000" />
        <SortedDescendingCellStyle BackColor="#FCF6C0" />
        <SortedDescendingHeaderStyle BackColor="#820000" />
    </asp:GridView>
</asp:Content>
