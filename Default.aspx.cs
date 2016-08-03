using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using MongoDB.Driver;
using MongoDB.Bson;

public partial class _Default : System.Web.UI.Page
{
    private MongoClient _client;
    private IMongoDatabase _database;
    private IMongoCollection<BsonDocument> _collection;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            _client = new MongoClient("mongodb://ygkroses:4rfv5tgb@ds033015.mlab.com:33015/acct");
            _database = _client.GetDatabase("acct");
            _collection = _database.GetCollection<BsonDocument>("account");
            //Response.Write(ConfigurationManager.AppSettings["MONGOLAB_URI"].ToString());
        }
        catch (Exception err)
        {
            Response.Write(err.Message);
        }
    }
}