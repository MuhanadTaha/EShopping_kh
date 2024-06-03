<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Orders.aspx.cs" Inherits="EKhadori.Orders" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .corners {
            border: 0px solid blue;
            -moz-border-radius: 8px;
            border-radius: 7px;
            overflow: hidden;
            -webkit-border-radius: 8px;
        }
    </style>
    <br />
    <br />






    <div class="container cont pt-2 pb-2 ">
        <asp:GridView ID="GridView1" OnRowCommand="GV_OnRowCommand" Style="width: 100%" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#0000" BorderWidth="4px" CellPadding="4" DataKeyNames="IdOrder" DataSourceID="SqlDataSource1" ForeColor="Black" GridLines="Horizontal">
            <Columns>
                <asp:BoundField DataField="IdOrder" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="IdOrder" />
                <asp:BoundField DataField="NameProducts" HeaderText="Product" SortExpression="NameProducts" />
                <asp:BoundField DataField="NameCategories" HeaderText="Category" SortExpression="NameCategories" />
                <asp:BoundField DataField="BuyerUser" HeaderText="Buyer" SortExpression="BuyerUser" />
                <asp:BoundField DataField="Price" HeaderText="Price" SortExpression="Price" />
                <asp:BoundField DataField="Count" HeaderText="Count" SortExpression="Count" />
                <asp:BoundField DataField="Arrive" HeaderText="Arrive" SortExpression="Arrive" />

                <asp:ButtonField ButtonType="Button" Text="Done" CommandName="OkCommand">
                    <ControlStyle CssClass="btn btn-primary btn-sm" />
                </asp:ButtonField>
            </Columns>
            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
            <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
            <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F7F7F7" />
            <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
            <SortedDescendingCellStyle BackColor="#E5E5E5" />
            <SortedDescendingHeaderStyle BackColor="#242121" />
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:EShoppDBConnectionString %>" SelectCommand="SELECT [IdOrder], [NameProducts], [NameCategories], [BuyerUser], [Price],[Arrive],[Count] FROM [Orders] WHERE ([BuyerUser] = @BuyerUser)">
            <SelectParameters>
                <asp:SessionParameter Name="BuyerUser" SessionField="Email" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>


        <br />



    </div>


    <div class="row">
        <div class="col-md-12 my-2">


            <div class="text-center pt-5">
                <asp:Button ID="btnConfime" OnClick="btnConfime_Click" runat="server" CssClass="btn btn-success" Text="Confime" />
                <strong class="table-bordered img-rounded alert-danger" style="padding: 30px; font-size: 30px;">
                    <asp:Label ID="Totallbl" runat="server" Text="Total" CssClass="text-primary"></asp:Label>:
                <asp:Label ID="resultTotalLbl" runat="server" Text="" CssClass="text-primary"></asp:Label>
                    Shekel
                </strong>
            </div>
        </div>

        <hr />
        <br />
        <br />
        <br />
        <br />

        <div class="col-md-12">


            <div>
                <div class="corners ">
                    <asp:GridView ID="GridView2" Width="100%" runat="server" AutoGenerateColumns="False" CellPadding="4" DataSourceID="SqlDataSource2" ForeColor="#333333" GridLines="None">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:TemplateField HeaderText="Id" SortExpression="Id">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Id") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Id") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="TotalOrder" HeaderText="Total Order" SortExpression="TotalOrder" />
                            <asp:BoundField DataField="Username" HeaderText="Username" SortExpression="Username" />
                            <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" />

                        </Columns>
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#0A81CA" Font-Bold="True" ForeColor="White" Height="60px" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" Height="60px" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:EShoppDBConnectionString %>" SelectCommand="SELECT [Id] ,[TotalOrder], [Username], [Date] FROM [Invoice] WHERE ([Username] = @Username) ORDER BY DATE DESC">
                        <SelectParameters>
                            <asp:SessionParameter Name="Username" SessionField="Email" Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </div>
            </div>

        </div>

    </div>

    <br />
    <br />






</asp:Content>
