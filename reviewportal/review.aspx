<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="review.aspx.cs" Inherits="reviewportal.review" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Reivew Status</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="align-content:center;padding:10px">
      
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound">
            <Columns>
                <asp:BoundField DataField="email" HeaderText="Email" />
                <asp:BoundField DataField="status" HeaderText="Status" SortExpression="ASC" />
                <asp:BoundField HeaderText="PWD" />
                <asp:BoundField HeaderText="Purchase Date" />
                <asp:BoundField HeaderText="Purchase ASIN" />
                <asp:BoundField HeaderText="Credit Card" />
                <asp:HyperLinkField HeaderText="Reviews" ItemStyle-HorizontalAlign="Center" NavigateUrl="~/review.aspx" Target="_self">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:HyperLinkField>
            </Columns>
        </asp:GridView>
      
    </div>
    </form>
</body>
</html>
