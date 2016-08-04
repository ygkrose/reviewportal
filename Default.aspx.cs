using System;
using System.Collections.Generic;
using System.Configuration;
using MongoDB.Driver;
using MongoDB.Bson;

public partial class _Default : System.Web.UI.Page
{
    private MongoClient _client;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //mongodb://appharbor_f5h26gwv:b0i898m2k4kcp09l6btpj3g9fb@ds139735.mlab.com:39735/appharbor_f5h26gwv
             if ( ConfigurationManager.AppSettings["MONGOLAB_URI"] == null)
                _client = new MongoClient("mongodb://appharbor_f5h26gwv:b0i898m2k4kcp09l6btpj3g9fb@ds139735.mlab.com:39735/appharbor_f5h26gwv");
             else
                _client = new MongoClient(ConfigurationManager.AppSettings["MONGOLAB_URI"].ToString());
            //Response.Write(ConfigurationManager.AppSettings["MONGOLAB_URI"].ToString());
            bindData();
        }
        catch (Exception err)
        {
            Response.Write(err.Message);
        }
    }

    private void bindData()
    {
        //MongoServer ms = _client.GetServer();
        //ms.Connect();
        //MongoDatabase md = ms.GetDatabase("appharbor_f5h26gwv");
        //MongoCollection<account> mongoCollection = md.GetCollection<account>("_account");
        //ms.Disconnect();
       // GridView1.DataSource = mongoCollection.FindAll();
       // GridView1.DataBind();
        //DetailsView1.DataSource = mongoCollection.FindAll();
        //DetailsView1.DataBind();
    }

    public class account 
    {
        public BsonObjectId id { get; set; }
        public string email { get; set; } 
        public string pwd { get; set; }

        public string status { get; set; }
        public purchase purchase { get; set; }

        public List<review> review { get; set; }

    }

    public class purchase
    {
        public string pdate { get; set; }
        public string pname { get; set; }
        public string ptel { get; set; }
        public string pitem { get; set; }
        public string pcardno { get; set; }

    }

    public class card
    {
        public string cNO { get; set; }

        public string cVDate { get; set; }
    }

    public class review
    {
        public string ritem { get; set; }
        public string rdate { get; set; }
        public string rtype { get; set; }
        public string status { get; set; }
        public string reviewer { get; set; }
    }
}