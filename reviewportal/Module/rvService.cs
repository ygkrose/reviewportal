using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Newtonsoft.Json;

namespace reviewportal.Module
{
    public class rvService
    {
        MongoCollection _collection;
        public rvService(MongoCollection collection)
        {
            _collection = collection;
        }
        public List<BsonDocument> Read(DataTableParamModel InputData)
        {
            //var rst = _collection.FindAs<Account>(Query.And(Query.GTE("review.rdate", InputData.sDate),Query.SizeGreaterThan("review",0)));
            var rst = _collection.FindAs<Account>(Query.SizeGreaterThan("review", 0));
            // .SetFields(Fields.Include(new string[] {"","","","","","","" }));
            List<BsonDocument> bdoc = new List<BsonDocument>();
            foreach (var row in rst)
            {
                foreach (var rv in row.review)
                {
                    bdoc.Add(geneBSON(row, rv));
                }
            }
            
            return bdoc;
        }

        private BsonDocument geneBSON(Account acc,Review rv)
        {
            return new BsonDocument()
            {
                { "rvitem" , rv.ritem.ToString().Trim() },
                { "seller" , rv.seller.ToString().Trim() },
                { "rvtime" , rv.rdate.ToString("yyyy/MM/dd HH:mm:ss")},
                { "rvemail" , acc.email.ToString().Trim() },
                { "rvtype" , rv.rtype.ToString().Trim()=="vp"?"Verified Purchase":"Customer Review" },
                { "rvstar" , rv.stars.ToString().Trim() },
                { "status" , rv.status.ToString().Trim() }
            };
        }
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

        public DateTime sDate { get; set; }

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