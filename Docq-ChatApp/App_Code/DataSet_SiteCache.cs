using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web;

public class DataSet_SiteCache
{
    public static void CleanCache()
    {
        // chace i null yap
        //HttpRuntime.Cache["websitechace"] = HttpRuntime.Cache.;
        // cahce i sil
        HttpRuntime.Cache.Remove("websitechace");
        // data setteki tabloları sil
        DataSetForSite.Tables.Clear();
        // data seti temizle
        DataSetForSite.Clear();
        // data seti yeniden boş tanımla
        DataSetForSite = new DataSet();

    }

    public static void CleanOutputCache()
    {
        CleanCache();

    }

    public static DataSet DataSetForSite = new DataSet();
    protected static DbConnectionInfo DB = new DbConnectionInfo();
    

    public static void SetWebSiteChace()
    {
        //HttpRuntime.Cache.Remove()
        //BaglantiBilgileri b = new BaglantiBilgileri();

        if (HttpRuntime.Cache["websitechace"] == null) // eğer kategoriler cahce boş ise
        {
            var UsersDataTableTask = UsersDataTable();

            Task.WaitAll(UsersDataTableTask);
            // 24 cache le isim olarak kategoriler, veri DS1
            HttpRuntime.Cache.Insert("websitechace", DataSetForSite, null, DateTime.Now.AddHours(24), System.Web.Caching.Cache.NoSlidingExpiration);
            
        }
        else  // eğer kategoriler cahce boş DEĞİL ise
        {
            DataSetForSite = HttpRuntime.Cache["websitechace"] as DataSet;
        }

    }

    protected static async Task UsersDataTable()
    {
        DataTable Users_DT = new DataTable("Users");

        using (SqlConnection connection = new SqlConnection(DB.ConnectionString()))
        {
            connection.Open();

            SqlCommand command = new SqlCommand(DataSet_SqlSorgu.UsersTblSql, connection);
            SqlDataReader reader = command.ExecuteReader();

            Users_DT.Load(reader);

            reader.Close();
            connection.Close();
        }

        // Önlem olarak eğer var ise var olanı sil.
        if (!DataSetForSite.Tables.Contains("Users"))
            DataSetForSite.Tables.Add(Users_DT);

    }

}
