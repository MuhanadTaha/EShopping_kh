<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductsList.aspx.cs" Inherits="EKhadori.ProductsList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <br />
    <br />
    <h1 class="text-dark">Products List</h1>
    <div style="overflow: hidden">
        <a id="Button1" href="Products.aspx" class="btn btn-primary" style="float: right; justify-content: center; display: block; width: 100px">Create</a>
    </div>
    <br />


    <div class="container cont pt-2 pb-2 ">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" OnRowEditing="GridView1_RowEditing"
            OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDeleting="GridView1_RowDeleting" OnRowUpdating="GridView1_RowUpdating"
            BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" RowStyle-Height="35px"
            Width="100%">
            <Columns>
                <asp:TemplateField HeaderText="ID" SortExpression="ID">

                    <ItemTemplate>
                        <asp:Label ID="idlbl" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle CssClass="pt-1 pb-1 pl-1 pr-1" />
                </asp:TemplateField>


                <asp:TemplateField HeaderText="Name" SortExpression="Name">
                    <ItemTemplate>
                        <asp:Label ID="namelbl" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="nametext" runat="server" Text='<%# Bind("Name") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Price" SortExpression="Price">
                    <ItemTemplate>
                        <asp:Label ID="pricelbl" runat="server" Text='<%# Bind("Price") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="pricetext" runat="server" Text='<%# Bind("Price") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Description" SortExpression="Description">
                    <ItemTemplate>
                        <asp:Label ID="desclbl" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="desctext" runat="server" Text='<%# Bind("Description") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>


                <asp:TemplateField HeaderText="Image" SortExpression="Image">
                    <ItemTemplate>
                        <asp:Image ID="imgCategories" runat="server" Style="width: 130px; border-radius: 7px; margin: 10px; border: 1px solid #F5F4F1;" ImageUrl='<%# "~/Image/ImageProducts/" + Eval("Image") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:FileUpload ID="FileUpload1" runat="server" />
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Quantity" SortExpression="Quantity">
                    <ItemTemplate>
                        <asp:Label ID="Quantitylbl" runat="server" Text='<%# Bind("Quantity") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="Quantitytxt" runat="server" Text='<%# Bind("Quantity") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>


                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="btnEdit" runat="server" CommandName="Edit" Text="Edit" />
                    </ItemTemplate>

                    <EditItemTemplate>
                        <asp:Button ID="btnUpdate" runat="server" Text="Update" CommandName="Update" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CommandName="Cancel" />
                    </EditItemTemplate>
                    <ControlStyle CssClass="btn btn-primary" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete" Text="Delete"></asp:LinkButton>
                    </ItemTemplate>
                    <ControlStyle CssClass="btn btn-danger" />
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
            <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />

            <RowStyle Height="35px"></RowStyle>

            <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F7F7F7" />
            <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
            <SortedDescendingCellStyle BackColor="#E5E5E5" />
            <SortedDescendingHeaderStyle BackColor="#242121" />
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:EShoppDBConnectionString %>" SelectCommand="SELECT * FROM [Products]"></asp:SqlDataSource>
    </div>
</asp:Content>
