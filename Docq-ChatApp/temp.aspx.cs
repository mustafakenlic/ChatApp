using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Docq_ChatApp
{
    public partial class temp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string saalt = "^+%sacfgda6a7sd8dasdf646sdfşlsf*-+5ass5dö-*saas6df4safdfgf9d4üğ5faf6+45lasdf";
            string pass = "Mustafa1436**";

            string hasedPass = hash.UserPassHash(pass, saalt).Trim().ToLower();

            string userName = "user2@mustafakenlic.dev";
            string userNameHass = hash.UserNameHash(userName.ToLower().Trim()).ToLower();

            Ltr.Text = "userNameHass : " + userNameHass + "<br />";
            Ltr.Text += "hasedPass : " + hasedPass;
        }
    }
}