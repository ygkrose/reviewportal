using reviewportal.Module;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using System.Configuration;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.IO;

namespace reviewportal.Controllers
{
    public class reviewController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(string id)
        {
            rvService svc = new rvService(getMongoDB());
            DataTableParamModel para = new DataTableParamModel();
            para.sDate =Convert.ToDateTime(id);
            var jsonWriterSettings = new JsonWriterSettings { OutputMode = JsonOutputMode.Strict };

            string rtn = svc.Read(para).ToJson(jsonWriterSettings);
            //rtn = "{\"data\":" + rtn + "}";
            return rtn;

        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }

        
        private MongoCollection getMongoDB()
        {
            MongoClient _client;
            MongoDatabase _database;
            MongoCollection _collection;
            if (ConfigurationManager.AppSettings["MONGOLAB_URI"] == null)
            {
                //mongodb://appharbor_f5h26gwv:b0i898m2k4kcp09l6btpj3g9fb@ds139735.mlab.com:39735/appharbor_f5h26gwv
                //mongodb://ygkroses:4rfv5tgb@ds033015.mlab.com:33015/acct
                _client = new MongoClient("mongodb://ygkroses:4rfv5tgb@ds033015.mlab.com:33015/acct");
                _database = _client.GetServer().GetDatabase("acct");
                _collection = _database.GetCollection<BsonDocument>("account");
            }
            else
            {
                _client = new MongoClient(ConfigurationManager.AppSettings["MONGOLAB_URI"].ToString());
                //_client = new MongoClient("mongodb://appharbor_f5h26gwv:b0i898m2k4kcp09l6btpj3g9fb@ds139735.mlab.com:39735/appharbor_f5h26gwv");
                _database = _client.GetServer().GetDatabase("appharbor_f5h26gwv");
                _collection = _database.GetCollection<BsonDocument>("account");
            }
            return _collection;
        }
    }


}
