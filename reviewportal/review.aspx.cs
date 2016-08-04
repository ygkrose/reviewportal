using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MongoDB.Driver;
using MongoDB.Bson;


namespace reviewportal
{
    public partial class review : System.Web.UI.Page
    {
        private MongoClient _client;

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadMongoDB();
        }

        private void LoadMongoDB()
        {
            
            _client = new MongoClient("mongodb://ygkroses:4rfv5tgb@ds033015.mlab.com:33015/acct");
            MongoDatabase _database = _client.GetServer().GetDatabase("acct");
            MongoCollection<Account> _collection =  _database.GetCollection<Account>("account");
            _collection.FindAll();



        }
    }
}