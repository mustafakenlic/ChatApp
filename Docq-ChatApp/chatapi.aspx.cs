using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using System.Web.Services;
using Newtonsoft.Json;

namespace Docq_ChatApp
{
    public partial class chatapi : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            // load all DB chaces
            DataSet_SiteCache.SetWebSiteChace();
        }


        [WebMethod]
        public static string getmessages(string userid)
        {
            List<message> messages = new List<message>();

            Int64 currentUserId = Convert.ToInt64(HttpContext.Current.Session["userId"].ToString());
            StringBuilder sb = new StringBuilder();

            DbConnectionInfo b = new DbConnectionInfo();
            SqlCommand com = new SqlCommand("select m.* from dbo.Messages m where ((m.SenderId = @SenderId and m.ReceiverId=@ReceiverId) or (m.SenderId = @ReceiverId and m.ReceiverId=@SenderId)) order by ID", b.Connetcion);
            com.Parameters.AddWithValue("@SenderId", userid);
            com.Parameters.AddWithValue("@ReceiverId", currentUserId);

            if (com.Connection.State == ConnectionState.Closed) com.Connection.Open();
            SqlDataReader dr = com.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Int64 SenderId = Convert.ToInt64(dr["SenderId"].ToString());
                    Int64 ReceiverId = Convert.ToInt64(dr["ReceiverId"].ToString());
                    string Content = dr["Content"].ToString();

                    var message = new message
                    {
                        senderid = SenderId,
                        //ReceiverId = ReceiverId,
                        content = Content
                    };

                    messages.Add(message);
                }
            }
            
            string jsonResult = JsonConvert.SerializeObject(messages);

            return jsonResult;
        }
        public class message
        {
            public Int64 senderid { get; set; }
            //public Int64 ReceiverId { get; set; }
            public string content { get; set; }
        }
    }
}