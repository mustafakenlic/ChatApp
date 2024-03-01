using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using System.Web.SessionState;

namespace Docq_ChatApp
{
    public partial class Chat : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // get the host
            string host = Request.Url.Host.ToLower().Trim();

            // if user loggedin show the page
            if (SessionControl.IsLoggedin(host))
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
                
                LoadUsers();
            }
            else
            {
                // if not loggedin don't show the page
                Server.ClearError();
                Response.Status = "404 Not Found";
                Response.StatusCode = 404;
                Response.End();
            }

        }

        protected void LoadUsers()
        {
            StringBuilder sb = new StringBuilder();
            Int64 currentUserID = Convert.ToInt64(HttpContext.Current.Session["userId"].ToString());

            DataTable table = DataSet_SiteCache.DataSetForSite.Tables["Users"];
            string where = string.Format("ID <> '{0}'", currentUserID);

            int i = 0;
            foreach (DataRow dtRows in table.Select(where))
            {
                DateTime? lastSeen = null;
                if (!string.IsNullOrEmpty(dtRows["lastSeen"].ToString()))
                    lastSeen = Convert.ToDateTime(dtRows["lastSeen"]);

                sb.Append("<li class=\"user\" data-userid=\"" + dtRows["ID"] + "\" id=\"user" + dtRows["ID"] + "\">");
                sb.Append("<img src=\"/uploads/users/" + dtRows["image"] + "?w=30&format=webp\" alt=\"user icon\"/>");
                sb.Append("<div id=\"userinfo\">");
                sb.Append("<strong>" + dtRows["name"] + "</strong>");
                sb.Append("<p>" + dtRows["department"] + "</p>");
                sb.Append("</div>");
                sb.Append("</li>");

                #region create chate for firs user
                if (i == 0)
                {
                    i++;

                    // chat header icon etc..
                    LtrActiveChat.Text += "<img src=\"/uploads/users/" + dtRows["image"] + "?w=30&format=webp\" alt=\"user icon\"/>";
                    LtrActiveChat.Text += "<div id=\"userinfo\">";
                    LtrActiveChat.Text += "<strong>" + dtRows["name"] + "</strong>";
                    LtrActiveChat.Text += "<p>" + dtRows["department"] + "</p>";
                    LtrActiveChat.Text += "</div>";

                    LtrActiveUserId.Text = "<input type=\"hidden\" id=\"activeuserid\" value=\"" + dtRows["ID"] + "\"/><input type=\"hidden\" id=\"currentuserid\" value=\"" + HttpContext.Current.Session["userId"] + "\"/>";

                    //load chat
                    LoadChat(dtRows["ID"].ToString());
                }
                #endregion

            }

            LtrUserList.Text = sb.ToString();
            sb.Clear();
        }

        protected void LoadChat(string userID)
        {
            Int64 currentUserId = Convert.ToInt64(HttpContext.Current.Session["userId"].ToString());
            StringBuilder sb = new StringBuilder();

            DbConnectionInfo b = new DbConnectionInfo();
            SqlCommand com = new SqlCommand("select m.* from dbo.Messages m where ((m.SenderId = @SenderId and m.ReceiverId=@ReceiverId) or (m.SenderId = @ReceiverId and m.ReceiverId=@SenderId)) order by ID", b.Connetcion);
            com.Parameters.AddWithValue("@SenderId", userID);
            com.Parameters.AddWithValue("@ReceiverId", currentUserId);
            
            if (com.Connection.State == ConnectionState.Closed) com.Connection.Open();
            SqlDataReader dr = com.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Int64 SenderId = Convert.ToInt64(dr["SenderId"].ToString());
                    Int64 ReceiverId = Convert.ToInt64(dr["ReceiverId"].ToString());
                    string Content = HttpUtility.HtmlEncode(dr["Content"].ToString());

                    // if this is a sended msg..
                    if (currentUserId == SenderId)
                    {
                        sb.Append("<div class=\"message send\"><div class=\"mesaage\">" + Content + "</div></div>");
                    }
                    else
                    {
                        sb.Append("<div class=\"message recived\"><div class=\"mesaage\">" + Content + "</div></div>");
                    }
                }
            }

            if (!dr.IsClosed) dr.Close();
            if (com.Connection.State != ConnectionState.Closed) com.Connection.Close();

            LtrMsgList.Text = sb.ToString();
            sb.Clear();
        }
    }
}