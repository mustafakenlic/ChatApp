using System;
using System.Security.Cryptography;
using System.Text;
using System.Web;


public class SessionControl
{
    public static string SecurituCode(string host)
    {
        string guvenlik = "";
        //SessionStateMode.InProc.Equals(true);
        // local host ise sesiondan değer okuma sabit değer ver
        if (host.ToLower() == "localhost")
        {
            guvenlik = "tempdata";
        }
        else // serverda ise sessiondaki değeri oku 
        {
            //MySession session = (MySession)HttpContext.Current.Session["guvenlik"];
            if (HttpContext.Current.Session["secCode"] != null)
            {
                guvenlik = HttpContext.Current.Session["secCode"].ToString();
            }
        }

        return guvenlik;
    }


    public static bool IsLoggedin(string host)
    {
        bool loggedin = false;
        //bool hostKontrol = host.ToLower().Contains(db.site);
        //local host ise şifre kontrolü yapma
        if (host.ToLower() == "localhost")
        {
            loggedin = true;

            //spesific to this site
            string usreId = "";
            try
            {
                usreId = HttpContext.Current.Session["userId"].ToString();
            }
            catch
            {
                // do no thing
            }
            
            if (string.IsNullOrEmpty(usreId))
            {
                HttpContext.Current.Session["userId"] = "1";
                HttpContext.Current.Session["userName"] = "Mustafa Gmail";
            }
        }
        else // serverda ise şifre kontrolü yap
        {
            if (HttpContext.Current.Session["control"] != null)
            {
                var temp = HttpContext.Current.Session["control"].ToString();
                if (temp == "Loggedin") loggedin = true; // olumluGirisVerisi ise dbgiriş yapıldı
            }

        }

        return loggedin;
    }

}
