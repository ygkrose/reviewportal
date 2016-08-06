using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace reviewportal
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
        {
            string name = Login1.UserName;
            List<string> users = new List<string>();
            users.AddRange(ConfigurationManager.AppSettings["users"].ToString().Split(new char[] { ',' }) );
            
            if (users.Exists(n => n==name))
            {
                if (Login1.Password == "4rfv5tgb")
                {
                    e.Authenticated = true;
                    Session.Add("valid", "true");
                    Response.Redirect("acct.aspx");
                }
                    
            }

            e.Authenticated = false;


        }
    }
}