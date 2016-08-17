using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace reviewportal.Module
{
    public class rvService
    {
        public DataTableResultModel Read(DataTableParamModel InputData)
        {
            DataTableResultModel OutputData = new DataTableResultModel();
            //Expression > System.Web.Mvc.Filter = x => x.Display == false;

            ///查詢的邏輯

            OutputData.iTotalDisplayRecords = //計算共顯示幾筆資料
            OutputData.iTotalRecords = OutputData.iTotalRecords =
                (int)Math.Ceiling((Double)OutputData.iTotalDisplayRecords / (Double)InputData.iDisplayLength);//總共有幾頁
            OutputData.sEcho = HttpContext.Current.Request.QueryString["sEcho"];
            return OutputData;
        }
    }

    public class DataTableResultModel
    {
        //相關參數
        public String sEcho { get; set; }
        public int iTotalRecords { get; set; }
        public int iTotalDisplayRecords { get; set; }
        //回傳資料
        public IEnumerable aaData { get; set; }
    }

    public class DataTableParamModel
    {
        /// 

        /// 請求的次數序號
        /// 

        public string sEcho { get; set; }

        /// 

        /// 查詢用的資料
        /// 

        public string sSearch { get; set; }

        /// 

        /// 一頁要顯示幾筆資料
        /// 

        public int iDisplayLength { get; set; }

        /// 

        /// 第一筆資料的位置
        /// 

        public int iDisplayStart { get; set; }

        /// 

        /// 在表中的列數
        /// 

        public int iColumns { get; set; }

        /// 

        /// 用哪一個欄位進行排序
        /// 

        public int iSortingCols { get; set; }

        /// 

        /// 欄位的名稱
        /// 

        public string sColumns { get; set; }
    }
}