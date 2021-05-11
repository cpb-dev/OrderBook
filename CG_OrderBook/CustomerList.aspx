<%@ Page Title="Customer List" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CustomerList.aspx.cs" Inherits="CG_OrderBook.CustomerList" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Customer List</h2>

    <br />
    <asp:TextBox id="tbSearchFN" runat="server" PlaceHolder="First Name" />
    <asp:TextBox id="tbSearchSN" runat="server" PlaceHolder="Surname" />
    <asp:Button ID="btnSearch" runat="server" Text="Search" Class="btn btn-primary" OnClick="btnSearch_Click" />
    <br />
    <br />

    <asp:GridView ID="gvCustomers" runat="server" 
        Width="80%" AutoGenerateColumns="False" DataKeyNames="Customer_ID" 
        DataSourceID="SqlDataSourceOrderBook" CellPadding="4" ForeColor="#333333" 
        GridLines="None" AllowSorting="True"
        ShowHeaderWhenEmpty="True" EmptyDataText="No Customers Found Yet!" AllowPaging="True">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="Customer_ID" HeaderText="Customer_ID" SortExpression="Customer_ID" InsertVisible="False" Visible="False" ReadOnly="True" />
            <asp:BoundField DataField="F_Name" HeaderText="First Name" SortExpression="F_Name" ReadOnly="True" />
            <asp:BoundField DataField="S_Name" HeaderText="Surname" SortExpression="S_Name" ReadOnly="True" />
            <asp:BoundField DataField="Email" HeaderText="Email" />
            <asp:BoundField DataField="Phone" HeaderText="Phone" />
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
    <asp:SqlDataSource ID="SqlDataSourceOrderBook" runat="server" 
        ConnectionString="<%$ ConnectionStrings:OrderDBConnectionString %>"
        SelectCommand="SELECT * FROM [customer_info]"
        UpdateCommand="UPDATE customer_info SET [Email] = @Email, [Phone] = @Phone WHERE [Customer_ID] = @Customer_ID" >
        <UpdateParameters>
            <asp:Parameter Name="Email" />
            <asp:Parameter Name="Phone" />
            <asp:Parameter Name="Customer_ID" />
        </UpdateParameters>
    </asp:SqlDataSource>

    <%-- Another GridView for when the user makes a search suggestion to the data --%>
    <asp:GridView ID="gvSearch" runat="server" 
        ShowHeaderWhenEmpty="True" EmptyDataText="No Customers Found!" 
        Visible="False"
        Width="80%" AutoGenerateColumns="False"
        CellPadding="4" ForeColor="#333333" 
        GridLines="None" AllowSorting="True"
        DataKeyNames="Customer_ID"
        OnRowEditing="gvSearch_RowEditing" OnRowCancelingEdit="gvSearch_RowCancelingEdit" OnRowUpdating="gvSearch_RowUpdating">
        <%-- As this GridView doesn't have a DataSource, it has to have the OnRow commands to allow similar functionality to the first GridView --%>
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="Customer_ID" HeaderText="Customer_ID" SortExpression="Customer_ID" Visible="false" ReadOnly="True" />
            <asp:BoundField DataField="F_Name" HeaderText="First Name" SortExpression="F_Name" ReadOnly="True" />
            <asp:BoundField DataField="S_Name" HeaderText="Surname" SortExpression="S_Name" ReadOnly="True" />
            <asp:BoundField DataField="Email" HeaderText="Email" />
            <asp:BoundField DataField="Phone" HeaderText="Phone" />
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
