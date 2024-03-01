using System.Data.SqlClient;

public class DbConnectionInfo
{
    private SqlConnection _Connection;

    public SqlConnection Connetcion
    {
        get { return _Connection; }
    }

    public DbConnectionInfo()
    {
        this._Connection = new SqlConnection("Data Source=SQL5101.site4now.net;Initial Catalog=db_a161ae_docqchat;User Id=db_a161ae_docqchat_admin;Password=eS3#7y#gP**mh9LR30Ud;");
    }


    public string ConnectionString()
    {
        string b = "Data Source=SQL5101.site4now.net;Initial Catalog=db_a161ae_docqchat;User Id=db_a161ae_docqchat_admin;Password=eS3#7y#gP**mh9LR30Ud;";
        return b;
    }
}