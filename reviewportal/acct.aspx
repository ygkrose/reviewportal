<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="acct.aspx.cs" Inherits="reviewportal.acct" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Account Status</title>
    <script type="text/javascript" src="Scripts/1.10.3/jquery-1.8.2.js"></script>
    <script type="text/javascript" src="Scripts/1.10.3/jquery-ui.js"></script>
    <link href="CSS/jquery-ui.css" rel="stylesheet" type="text/css"/>
    <style type="text/css">.td {padding:3px}
        #vtree {
            width: 260px;
        }
    </style>
    <script type="text/javascript">
    $("#btnShowPopUp").live("click", function () {
        $("#popup").dialog({
            title: "Displaying GridView Data",
            width:350,
            buttons: {
            //if you want close button use below code
                Close: function () {
                    $(this).dialog('close');
                }
            }
        });
        return false;
    });
    function openCardInfo(cid, allid) {
        var cardsjson = JSON.parse(allid);
        $("#popup").empty();
        $("#popup").dialog({
            title: " Card Info ",
            width: 500
        });
        mytable = $('<table border="1px"></table>').attr({ id: "cardTable" });
        $('<th>ID</th><th>Num</th><th>Expired</th><th>Usable</th><th>Balance</th>').appendTo(mytable);
        for (var i = 0; i < cardsjson.length; i++) {
            var _row = $('<tr></tr>').appendTo(mytable);
            $('<td></td>').text(cardsjson[i].cardname).appendTo(_row);
            $('<td></td>').text(cardsjson[i].CardId).appendTo(_row);
            $('<td></td>').text(cardsjson[i].bmonth + "/" +cardsjson[i].byear).appendTo(_row);
            $('<td></td>').text(cardsjson[i].status).appendTo(_row);
            $('<td align=right></td>').text(cardsjson[i].balance).appendTo(_row);
        }
        mytable.appendTo("#popup");
    }
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
        $('<div>cr:Customer Review, pr:Purchase Review</div>').appendTo("#popup");
    }
</script>
</head>
<body>
    <Div style="font-family: &quot;Times New Roman&quot;, Times, serif; font-size: 25px">Welcome Review Portal</Div>
    <form id="form1" runat="server">
        <div id="vtree" style="float:left; height: 116px;padding-right:3px" height: 93px;">

            <asp:TreeView ID="TreeView1" runat="server" Width="239px" Font-Size="Medium" Height="110px" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged">
                <Nodes>
                    <asp:TreeNode Text="Accounts Status" Value="AS"></asp:TreeNode>
                    <asp:TreeNode Text="Test Result" Value="TR">
                        <asp:TreeNode Text="1.Same VPN,Same Prod. Review" Value="SVSPR"></asp:TreeNode>
                        <asp:TreeNode Text="2.Shop over $5" Value="SO5"></asp:TreeNode>
                    </asp:TreeNode>
                    <asp:TreeNode Text="Review Outcome" Value="RO"></asp:TreeNode>
                </Nodes>
                <SelectedNodeStyle BackColor="#CCFF33" />
            </asp:TreeView>

        </div>
        <div style="float:left; width: 726px;" padding:"10px">

            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound" AllowSorting="True" OnSorting="GridView1_Sorting" PageSize="20" BorderStyle="Solid">
                <Columns>
                    <asp:BoundField DataField="email" HeaderText="Email" SortExpression="email" />
                    <asp:BoundField DataField="status" HeaderText="Status" SortExpression="status" />
                    <asp:BoundField HeaderText="PWD" />
                    <asp:BoundField HeaderText="Purchase Date" SortExpression="pdate" HtmlEncode="False" />
                    <asp:BoundField HeaderText="Purchase ASIN" SortExpression="asin" />
                    <asp:BoundField HeaderText="Purchase TEL" SortExpression="tel" />
                    <asp:BoundField HeaderText="Credit Card" SortExpression="pcard"><ItemStyle HorizontalAlign="Center"></ItemStyle></asp:BoundField>
                    <asp:HyperLinkField HeaderText="Reviews" ItemStyle-HorizontalAlign="Center" NavigateUrl="~/review.aspx" Target="_self" SortExpression="rvs">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:HyperLinkField>
                    <asp:BoundField DataField="vpn" HeaderText="VPN Name" SortExpression="vpn" />
                </Columns>
                <EditRowStyle Wrap="False" />
                <HeaderStyle BackColor="#CCCCFF" Wrap="False" />
                <PagerSettings Mode="NextPreviousFirstLast" />
                <RowStyle Wrap="False" />
            </asp:GridView>

            <asp:LinkButton ID="btn_pp" runat="server" CommandName="first" OnClick="nav_btn_click" Visible="False">&lt;&lt;</asp:LinkButton>
&nbsp;
            <asp:LinkButton ID="btn_p" runat="server" CommandName="previous" OnClick="nav_btn_click" Visible="False">&lt;</asp:LinkButton>
&nbsp;<asp:Label ID="lbl_cp" runat="server" Text="#" Visible="False"></asp:Label>
&nbsp;<asp:LinkButton ID="btn_n" runat="server" CommandName="next" OnClick="nav_btn_click" Visible="False">&gt;</asp:LinkButton>
&nbsp;
            <asp:LinkButton ID="btn_nn" runat="server" CommandName="last" OnClick="nav_btn_click" Visible="False">&gt;&gt;</asp:LinkButton>
            <div id="msgdesc" style="text-align:left" runat="server"></div>
        </div>
        <div id="popup" style="display: none">
         
        </div>
    </form>
        </body>
</html>
