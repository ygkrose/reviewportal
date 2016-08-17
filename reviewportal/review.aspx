<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="review.aspx.cs" Inherits="reviewportal.review" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Reivew Status</title>
    <link href="CSS/jquery.dataTables.min.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-3.1.0.min.js"></script>
    <script src="Scripts/jquery.dataTables.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="viewgrid" style="align-content: center; padding: 10px">
            <table class="table table-striped table-bordered table-hover" id="DataTables">
                <thead>
                    <tr role="row">
                        <th data-name="ItemId" style="width: 5%;">#</th>
                        <th data-name="Name" style="width: 25%;">商品名稱</th>
                        <th data-name="Price" style="width: 10%;">商品價格</th>
                        <th data-name="Display" style="width: 10%;">商品狀態</th>
                        <th data-name="Image" style="width: 20%;">商品圖片</th>
                        <th data-name="Message" style="width: 30%;">商品描述</th>
                    </tr>
                </thead>
                <tbody></tbody>
            </table>
        </div>
    </form>
    <script>
        $(function () {
            var rvURL = "http://localhost:57047/api/review/" + new Date().toDateString();
            var oTable = $('#DataTables').dataTable({
                "aaSorting": [[0, 'asc']],//初始排序欄位
                "bFilter": true,//是否有搜尋資料的input
                "bAutoWidth": false,//是否要自動欄寬
                "bScrollCollapse": false,
                "bPaginate": true,
                "bLengthChange": true,
                "bDeferRender": true,
                "bServerSide": true,//資料是否來自伺服器
                "bProcessing": true,//顯示處理中的圖樣
                "sDom": "<'row'<'col-sm-offset-8 col-sm-3'f><'col-sm-1' l>t>" + "r" + "<'row'<'col-sm-6'i><'col-sm-6'p>>",//設定搜尋與資料長度
                "sEcho": 1,//起始Echo
                "sAjaxSource": rvURL ,//若資料來自伺服器，在此填寫URL
                "iDisplayLength": 10,//出史資料大小
                "aoColumns": [//(重要)欄位對應，可以讓伺服器回傳的資料可以在頁面上對應
                    { "mData": "Id" },
                    { "mData": "Name" },
                    { "mData": "Price" },
                    { "mData": "Display" },
                    { "mData": "Image" },
                    { "mData": "Message" }
                ],//設定語言
                "oLanguage": {
                    "sLengthMenu": " _MENU_ 筆/頁",
                    "sZeroRecords": "找不到符合的資料。",
                    "sInfo": "共 _MAX_ 頁",
                    "sSearch": "搜尋",
                    "sInfoFiltered": " - 找到 _TOTAL_ 筆 資料",
                    "sInfoEmpty": "共 0 頁",
                    "oPaginate": {
                        "sPrevious": "«",
                        "sNext": "»"
                    }
                }
            });
        });
    </script>
</body>
</html>
