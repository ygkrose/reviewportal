using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Threading.Tasks;
using System.Data;
using System.Configuration;
using MongoDB.Driver.Builders;
using System.Collections;

namespace reviewportal
{
    public partial class acct : System.Web.UI.Page
    {
        private MongoClient _client;
        MongoDatabase _database = null;
        MongoCollection<Account> _collection = null;

        int rowsPerPage = 200;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["valid"] == null)
            {
                Response.Redirect("./default.aspx");
                Response.End();
            }

            LoadMongoDB();
        }

        private void LoadMongoDB()
        {
           
            if (ConfigurationManager.AppSettings["MONGOLAB_URI"] == null)
            {
                //mongodb://appharbor_f5h26gwv:b0i898m2k4kcp09l6btpj3g9fb@ds139735.mlab.com:39735/appharbor_f5h26gwv
                //mongodb://ygkroses:4rfv5tgb@ds033015.mlab.com:33015/acct
                _client = new MongoClient("mongodb://ygkroses:4rfv5tgb@ds033015.mlab.com:33015/acct");
                _database = _client.GetServer().GetDatabase("acct");
                _collection = _database.GetCollection<Account>("account");
            }
            else
            {
                _client = new MongoClient(ConfigurationManager.AppSettings["MONGOLAB_URI"].ToString());
                //_client = new MongoClient("mongodb://appharbor_f5h26gwv:b0i898m2k4kcp09l6btpj3g9fb@ds139735.mlab.com:39735/appharbor_f5h26gwv");
                _database = _client.GetServer().GetDatabase("appharbor_f5h26gwv");
                _collection = _database.GetCollection<Account>("_account");
            }


            //Parallel.ForEach<Account>(_collection.FindAllAs<Account>(), (_doc)=>{

            //});
            GridView1.DataSource = _collection.FindAll().SetLimit(rowsPerPage);
            GridView1.DataBind();

        }


        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex > -1)
            {
                if (((Account)e.Row.DataItem).status == "Blocked")
                {
                    e.Row.Cells[1].ForeColor = System.Drawing.Color.Red;
                }
                e.Row.Cells[2].Text = ((Account)e.Row.DataItem).pwd.Substring(0, 2) + "...";
                e.Row.Cells[3].Text = ((Account)e.Row.DataItem).purchase.pdate;
                e.Row.Cells[4].Text = ((Account)e.Row.DataItem).purchase.pitem;
                e.Row.Cells[5].Text = ((Account)e.Row.DataItem).purchase.ptel;
                e.Row.Cells[6].Text = ((Account)e.Row.DataItem).purchase.pcardno;

                HyperLink hl = new HyperLink();
                hl.Text = ((Account)e.Row.DataItem).review.Count.ToString();
                if (((Account)e.Row.DataItem).review.Count > 0)
                {
                    //hl.Attributes.Add("rvdata", ((Account)e.Row.DataItem).review.ToJson());
                    hl.Attributes.Add("onclick", "openReview('" + ((Account)e.Row.DataItem).email + "','" + ((Account)e.Row.DataItem).review.ToJson() + "')");
                    hl.NavigateUrl = "#";
                }
                e.Row.Cells[7].Controls.Add(hl);
            }

        }
        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortfield = e.SortExpression;
            if (sortfield == "pdate")
                sortfield = "purchase.pdate";
            else if (sortfield == "pcard")
                sortfield = "purchase.pcardno";
            else if (sortfield == "asin")
                sortfield = "purchase.pitem";
            else if (sortfield == "tel")
                sortfield = "purchase.ptel";
            else if (sortfield == "rvs")
                sortfield = "review";

            if (ViewState[e.SortExpression]==null)
            {
                ViewState.Add(e.SortExpression, "Asc");
            }
            else
            {
                if (ViewState[e.SortExpression].ToString() == "Asc")
                    ViewState[e.SortExpression] = "Desc";
                else
                    ViewState[e.SortExpression] = "Asc";
            }
            if (ViewState[e.SortExpression].ToString()=="Asc")
            {
                GridView1.DataSource = _collection.FindAll().SetSortOrder(SortBy.Ascending(sortfield)).SetLimit(rowsPerPage);
            }
            else
            {
                GridView1.DataSource = _collection.FindAll().SetSortOrder(SortBy.Descending(sortfield)).SetLimit(rowsPerPage);
            }
                
            GridView1.DataBind();
        }
    }
}