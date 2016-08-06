<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="acct.aspx.cs" Inherits="reviewportal.acct" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Account Status</title>
    <script type="text/javascript" src="Scripts/1.10.3/jquery-1.8.2.js"></script>
    <script type="text/javascript" src="Scripts/1.10.3/jquery-ui.js"></script>
    <link href="CSS/jquery-ui.css" rel="stylesheet" type="text/css"/>
    <style type="text/css">.td {padding:3px}</style>
    <script type="text/javascript">
    $("#btnShowPopUp").live("click", function () {
        $("#popup").dialog({
            title: "Displaying GridView Data",
            width:600,
            buttons: {
            //if you want close button use below code
                Close: function () {
                    $(this).dialog('close');
                }
            }
        });
        return false;
    });
    function openReview(eid,jrows) {
        var _rows = JSON.parse(jrows);
        $("#popup").empty();
        $("#popup").dialog({
            title: eid + " Review Detail",
            width: 600
            //buttons: {
                //if you want close button use below code
                //Close: function () {
                //    $(this).dialog('close');
                //}
            //}
        });
        mytable = $('<table border="1px"></table>').attr({ id: "basicTable" });
        $('<th>Review Date</th><th>Product ASIN</th><th>Review Type</th><th>Status</th>').appendTo(mytable);
        for (var i = 0; i < _rows.length; i++) {
            //var row = $('<tr></tr>').attr({ class: ["class1", "class2", "class3"].join(' ') }).appendTo(mytable);
            var _row = $('<tr></tr>').appendTo(mytable);
            $('<td></td>').text(_rows[i].rdate).appendTo(_row);
            $('<td></td>').text(_rows[i].ritem).appendTo(_row);
            $('<td></td>').text(_rows[i].rtype).appendTo(_row);
            $('<td></td>').text(_rows[i].status).appendTo(_row);
            //$('<td></td>').text(rows[i].rdate).appendTo(_row);
        }
        //console.log("TTTTT:" + mytable.html());
        mytable.appendTo("#popup");
    }
</script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="align-content: center; padding: 10px">

            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound" AllowSorting="True" OnSorting="GridView1_Sorting" PageSize="20">
                <Columns>
                    <asp:BoundField DataField="email" HeaderText="Email" SortExpression="email" />
                    <asp:BoundField DataField="status" HeaderText="Status" SortExpression="status" />
                    <asp:BoundField HeaderText="PWD" />
                    <asp:BoundField HeaderText="Purchase Date" SortExpression="pdate" />
                    <asp:BoundField HeaderText="Purchase ASIN" SortExpression="asin" />
                    <asp:BoundField HeaderText="Purchase TEL" SortExpression="tel" />
                    <asp:BoundField HeaderText="Credit Card" SortExpression="pcard" />
                    <asp:HyperLinkField HeaderText="Reviews" ItemStyle-HorizontalAlign="Center" NavigateUrl="~/review.aspx" Target="_self" SortExpression="rvs">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:HyperLinkField>
                </Columns>
            </asp:GridView>

        </div>
        <div id="popup" style="display: none">
<%--            <asp:GridView runat="server" ID="grd" AutoGenerateColumns="false"
                Style="margin-bottom: 35px" Width="550px">
                <Columns>
                    <asp:TemplateField HeaderText="Email">
                        <ItemTemplate>
                            <asp:Label ID="lblEMail" Text='<%#Eval("id") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Review ASIN">
                        <ItemTemplate>
                            <asp:Label ID="lblASIN" Text='<%#Eval("FirstName") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Review Date">
                        <ItemTemplate>
                            <asp:Label ID="lblDate" Text='<%#Eval("LastName") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Review Type">
                        <ItemTemplate>
                            <asp:Label ID="lblType" Text='<%#Eval("Address") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Review Status">
                        <ItemTemplate>
                            <asp:Label ID="lblSts" Text='<%#Eval("Address") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>--%>
        </div>
    </form>
</body>
</html>
