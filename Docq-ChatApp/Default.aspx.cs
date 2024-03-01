using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace Docq_ChatApp
{
    public partial class Default : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            // load all DB chaces
            DataSet_SiteCache.SetWebSiteChace();

            #region add response header
            // say google do not index this page
            Response.AddHeader("X-Robots-Tag", "noindex, noarchive, nosnippet, notranslate");
            //for mixed content problem
            Response.Headers.Add("Content-Security-Policy", "upgrade-insecure-requests");
            // don't use cache
            Response.CacheControl = "no-cache";
            #endregion

            #region Clean session datas
            HttpContext.Current.Session["control"] = "";
            HttpContext.Current.Session["secCode"] = "";
            HttpContext.Current.Session["userId"] = "";
            HttpContext.Current.Session["userName"] = "";
            #endregion


            #region get username and password
            string user = "";
            string pass = "";
            try
            {
                user = hash.UserNameHash(Request.Form["username"].ToLower().Trim()).ToLower();
                pass = Request.Form["password"].Trim();
            }
            catch
            {
                //do nothing
            }
            #endregion

            // if username and password is not empty check user login info
            if (!string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(pass))
            {
                // use chace
                DataTable table = DataSet_SiteCache.DataSetForSite.Tables["Users"];
                string where = string.Format("mailHass='{0}'", user);

                DataRow[] filteredRows = table.Select(where);
                if (filteredRows.Length > 0)
                {
                    foreach (DataRow dtRows in filteredRows)
                    {
                        var salt = dtRows["salt"].ToString().Trim();
                        pass = hash.UserPassHash(pass, salt).Trim().ToLower();

                        if (pass == dtRows["pass"].ToString().Trim().ToLower())
                        {
                            HttpContext.Current.Session["control"] = "Loggedin";
                            HttpContext.Current.Session["secCode"] = hash.GetUniqueKey(64);
                            HttpContext.Current.Session["userId"] = dtRows["ID"].ToString().Trim();
                            HttpContext.Current.Session["userName"] = dtRows["name"].ToString().Trim();


                            Response.Redirect("chat.aspx");
                        }
                        else EmptyForm("Invalid Username or Password");
                    }
                }
                else
                {
                    EmptyForm("Invalid Username or Password");
                }


            }
            else
            {

                EmptyForm("");
            }
        }

        private void EmptyForm(string msj)
        {
            LtrForm.Text = "<form method=\"post\" class=\"form\">";
            LtrForm.Text += "<div>";
            LtrForm.Text += "<h2 class=\"formBaslik\">Doc-Q<br>";
            LtrForm.Text += "<strong>Private Chat</strong></h2>";
            LtrForm.Text += "</div>";
            LtrForm.Text += "<div class=\"formWrap\">";
            LtrForm.Text += "<label for=\"username\" class=\"eob\">User Name</label>";
            LtrForm.Text += "<input autocomplete=\"off\" name=\"username\" type=\"email\" class=\"mainForm-input\" id=\"username\" placeholder=\"Your Email\" required>";
            LtrForm.Text += "</div>";
            LtrForm.Text += "<div class=\"formWrap\">";
            LtrForm.Text += "<label for=\"password\" class=\"eob\">Password</label>";
            LtrForm.Text += "<input autocomplete=\"off\" name=\"password\" type=\"password\" class=\"mainForm-input\" id=\"password\" placeholder=\"Your Password\" required>";
            LtrForm.Text += "</div>";
            LtrForm.Text += "<div class=\"formWrap\">";
            LtrForm.Text += "<button class=\"buton formSubmit\" id=\"mainSliderformSubmit\" type=\"submit\">Login</button>";
            LtrForm.Text += "</div>";

            if (!string.IsNullOrEmpty(msj))
            {
                LtrForm.Text += "<div id=\"errorMessages\">";
                LtrForm.Text += "<p id=\"errorMessage\" class=\"errorMessage\">" + msj + "</p>";
                LtrForm.Text += "</div>";
            }

            //LtrForm.Text += "<div class=\"links\">";
            //LtrForm.Text += "<a href=\"singup.aspx\">Not A Member</a>";
            //LtrForm.Text += "<a href=\"forgotpassword.aspx\">Forgot Password</a>";
            //LtrForm.Text += "</div>";

            LtrForm.Text += "</form>";
        }


    }
}