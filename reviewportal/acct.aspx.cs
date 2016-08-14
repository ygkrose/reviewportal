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
using MongoDB.Bson.IO;

namespace reviewportal
{
    public partial class acct : System.Web.UI.Page
    {
        private MongoClient _client;
        MongoDatabase _database = null;
        MongoCollection<Account> _collection = null;
        MongoCollection<Cards> _collect_Card = null;
        private string cardJson = "";
        int rowsPerPage = 25;
        int currPage = 0;
        int totalPages = 0;
        protected void Page_Load(object sender, EventArgs e)
        {

            //#if DEBUG
            //#else
            //if (Session["valid"] == null)
            //{
            //    Response.Redirect("./default.aspx");
            //    Response.End();
            //}
            //#endif
            LoadMongoDB();

            if (!IsPostBack)
            {
                manageCtlStyle();
                ViewState["currentPage"] = currPage;
            }
            else
            {
                if (ViewState["cardstring"] != null)
                {
                    //_collection = Session["main_collection"] as MongoCollection<Account>;
                    cardJson = ViewState["cardstring"].ToString();
                    currPage = (int)ViewState["currentPage"];
                    totalPages = (int)ViewState["totalpage"];
                }
            }
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
            _collect_Card = _database.GetCollection<Cards>("card");

        }

        private void manageCtlStyle()
        {
            GridView1.AlternatingRowStyle.BackColor = System.Drawing.Color.WhiteSmoke;

        }

        private MongoCursor<Account> doFilter(IMongoQuery filter = null)
        {
            if (string.IsNullOrEmpty(cardJson))
            {
                var jsonWriterSettings = new JsonWriterSettings { OutputMode = JsonOutputMode.Strict };
                cardJson = _collect_Card.FindAllAs<Cards>()
                    .SetFields(Fields.Exclude(new string[2] { "scode", "adddate" })).ToJson(jsonWriterSettings);
                ViewState["cardstring"] = cardJson;
            }
            //Parallel.ForEach<Account>(_collection.FindAllAs<Account>(), (_doc)=>{
            //});

            GridView1.DataSource = null;
            MongoCursor<Account> gridsource;
            if (filter == null)
            {
                gridsource = _collection.FindAllAs<Account>().SetLimit(rowsPerPage);
                if (_collection.Count() > rowsPerPage)
                    displayNavBtn(true);
                else
                    displayNavBtn(false);
                totalPages = (int)Math.Ceiling((decimal)_collection.Count() / rowsPerPage);
            }
            else
            {
                Session["filterstring"] = filter;
                gridsource = _collection.Find(filter).SetLimit(rowsPerPage);
                if (gridsource.Count() > rowsPerPage)
                    displayNavBtn(true);
                else
                    displayNavBtn(false);
                totalPages = (int)Math.Ceiling((decimal)gridsource.Count() / rowsPerPage);
            }
            resetInfo();
            return gridsource;
        }

        private void resetInfo()
        {
            lbl_cp.Text = "1";
            ViewState["currentPage"] = 0;
            ViewState["totalpage"] = totalPages;
            Session["filterstring"] = null; 
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
                string _card = ((Account)e.Row.DataItem).purchase.pcardno;
                if (_card.Length <= 3)
                {
                    HyperLink lnk = new HyperLink();
                    lnk.Text = _card;
                    lnk.Attributes.Add("onclick", "openCardInfo('" + _card + "','" + cardJson + "')");
                    lnk.NavigateUrl = "#";
                    e.Row.Cells[6].Controls.Add(lnk);
                }
                else
                    e.Row.Cells[6].Text = _card;

                HyperLink hl = new HyperLink();
                hl.Text = ((Account)e.Row.DataItem).review.Count.ToString();
                if (((Account)e.Row.DataItem).review.Count > 0)
                {
                    //hl.Attributes.Add("rvdata", ((Account)e.Row.DataItem).review.ToJson());
                    hl.Attributes.Add("onclick", "openReview('" + ((Account)e.Row.DataItem).email + "','" + ((Account)e.Row.DataItem).review.ToJson() + "')");
                    hl.NavigateUrl = "#";
                }
                e.Row.Cells[7].Controls.Add(hl);
                e.Row.Cells[8].Text = ((Account)e.Row.DataItem).vpn;
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

            if (ViewState[sortfield] == null)
            {
                ViewState.Add(sortfield, "Asc");
            }
            else
            {
                if (ViewState[sortfield].ToString() == "Asc")
                    ViewState[sortfield] = "Desc";
                else
                    ViewState[sortfield] = "Asc";
            }
            if (ViewState[sortfield].ToString() == "Asc")
            {
                GridView1.DataSource = ((MongoCursor<Account>)Session["gridsource"]).Clone<Account>().SetSortOrder(SortBy.Ascending(sortfield)).SetLimit(rowsPerPage);
            }
            else
            {
                GridView1.DataSource = ((MongoCursor<Account>)Session["gridsource"]).Clone<Account>().SetSortOrder(SortBy.Descending(sortfield)).SetLimit(rowsPerPage);
            }
            GridView1.DataBind();
            ViewState["sortdata"] = sortfield;
            resetInfo();
        }

        protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {
            string val = (sender as TreeView).SelectedValue.Trim();
            if (val == "AS")
            {
                Session["gridsource"] = doFilter();
            }
            else if (val == "SVSPR")
            {
                Session["gridsource"] = doFilter(Query.And(Query.EQ("vpn", "Canada#18"), Query.Matches("createdate", "2016-08-07")));
            }
            else if (val == "RO")
            {
                Session["gridsource"] = doFilter(Query.SizeGreaterThan("review", 0));
            }
            else if (val == "SO5")
            {
                Session["gridsource"] = doFilter(Query.EQ("vpn", "Canada#26"));
            }
            GridView1.DataSource = Session["gridsource"];
            GridView1.DataBind();
        }

        private void displayNavBtn(bool show)
        {
            if (show)
            {
                btn_n.Visible = true;
                btn_nn.Visible = true;
                btn_p.Visible = true;
                btn_pp.Visible = true;
                lbl_cp.Visible = true;
            }
            else
            {
                btn_n.Visible = false;
                btn_nn.Visible = false;
                btn_p.Visible = false;
                btn_pp.Visible = false;
                lbl_cp.Visible = false;
            }
        }

        protected void nav_btn_click(object sender, EventArgs e)
        {
            switch ((sender as LinkButton).CommandName)
            {
                case "first":
                    currPage = 0;
                    break;
                case "next":
                    if(currPage != totalPages-1)
                       currPage++;
                    break;
                case "previous":
                    if (currPage != 0)
                        currPage--;
                    break;
                case "last":
                        currPage = totalPages - 1;
                    break;
            }
            GridView1.DataSource = null;

            var _cussor = Session["gridsource"] as MongoCursor<Account>;
            if (Session["filterstring"] != null)
                _cussor = _collection.FindAs<Account>(Session["filterstring"] as IMongoQuery);

            if (ViewState["sortdata"] == null)
            {
                //GridView1.DataSource = ((MongoCursor<Account>)Session["gridsource"]).Clone<Account>().Skip(currPage * rowsPerPage).Take(rowsPerPage);
                GridView1.DataSource = _cussor.Collection.FindAllAs<Account>().Skip(currPage * rowsPerPage).Take(rowsPerPage);
                //GridView1.DataSource = _cussor.Skip(currPage * rowsPerPage).Take(rowsPerPage);
            }
            else
            {
                if (ViewState[ViewState["sortdata"].ToString()].ToString() == "Asc")
                {
                    GridView1.DataSource = _cussor.Collection.FindAllAs<Account>().SetSortOrder(SortBy.Ascending( ViewState["sortdata"].ToString())).Skip(currPage * rowsPerPage).Take(rowsPerPage);
                }
                else
                {
                    GridView1.DataSource = _cussor.Collection.FindAllAs<Account>().SetSortOrder(SortBy.Descending(ViewState["sortdata"].ToString())).Skip(currPage * rowsPerPage).Take(rowsPerPage);
                }
            }

            ViewState["currentPage"] = currPage;
            lbl_cp.Text = (currPage + 1).ToString();
            GridView1.DataBind();
        }
    }
}