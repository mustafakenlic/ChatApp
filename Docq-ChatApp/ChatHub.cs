using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;

namespace Docq_ChatApp
{
    public class ChatHub : Hub
    {


        public async Task send(string senderId, string receiverId, string content)
        {
            // call async Task and don't wait to finish
#pragma warning disable CS4014
            WriteToDb(senderId, receiverId, content);
#pragma warning restore CS4014

            Clients.All.boardcastMessage(senderId, receiverId, content);
        }


        /// <summary>
        /// This task will run async
        /// We will not wait it to finish so it wil not efect our apps performance
        /// But as long as there is no error it will whire to DB
        /// And we can read it when we need it.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="message"></param>
        /// <returns></returns>
#pragma warning disable CS1998
        protected async Task WriteToDb(string senderId, string receiverId, string content)
#pragma warning restore CS1998
        {
            // for futher use, so we can track is inster happend or not
            Int64 yeniID = 0;

            DbConnectionInfo b = new DbConnectionInfo();
            SqlCommand com = new SqlCommand("insert into Messages (SenderId, ReceiverId, Content, Timestamp)" +
                                            " values " +
                                            "(@SenderId, @ReceiverId, @Content, @Timestamp)" +
                                            " SELECT @@Identity", b.Connetcion); // id yi dön


            com.Parameters.AddWithValue("@SenderId", senderId);
            com.Parameters.AddWithValue("@ReceiverId", receiverId);
            com.Parameters.AddWithValue("@Content", content);
            com.Parameters.AddWithValue("@Timestamp", DateTime.Now);


            if (com.Connection.State == ConnectionState.Closed) com.Connection.Open();

            try
            {
                yeniID = Convert.ToInt64(com.ExecuteScalar().ToString());
            }
            catch
            {
                yeniID = 0;
            }

            if (com.Connection.State != ConnectionState.Closed) com.Connection.Close();
        }
    }
}