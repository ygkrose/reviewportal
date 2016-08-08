using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;

namespace reviewportal
{
    public class Account
    {
        public BsonObjectId id { get; }
        public string email { get; set; } = "";
        public string pwd { get; set; } = "";
        public string status { get; set; } = "";

        public Purchase purchase = new Purchase();

        public List<Review> review = new List<Review>();

        public string vpn { get; set; } = "";

        public string createdate { get; set; } = "";
    }

    public class Purchase
    {
        public string pdate { get; set; } = "";
        public string pname { get; set; } = "";
        public string ptel { get; set; } = "";
        public string pitem { get; set; } = "";
        public string pcardno { get; set; } = "";
    }

    public class Review
    {
        public string ritem { get; set; } = "";
        public string rdate { get; set; } = "";
        public string rtype { get; set; } = "";
        public string status { get; set; } = "";
        public string reviewer { get; set; } = "";
    }
}