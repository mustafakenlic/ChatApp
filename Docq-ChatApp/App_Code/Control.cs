using System;
using System.Text.RegularExpressions;

public static class Control
{
    // sadece sitenin kendi iç linklerini kabul eder /kategor/urun/ gibi
    public static bool IsSiteUrl(string veriKontrol)
    {
        bool sonuc;
        if (!string.IsNullOrEmpty(veriKontrol))
        {
            string pattern = @"^((\/+(['a-z0-9''\-']{1,100})+\/)(((['a-z0-9''\-']{1,100})+\/)?)*){1,500}$";
            Match match = Regex.Match(veriKontrol, pattern, RegexOptions.IgnoreCase);
            sonuc = match.Success;
        }
        else
        {
            sonuc = false;
        }
        return sonuc;
    }

    // başında http - https olan urlleri kontrol eder
    public static bool IsUrl(string veriKontrol)
    {
        bool sonuc;
        if (!string.IsNullOrEmpty(veriKontrol))
        {
            string pattern = @"^((https?:\/\/)?(([\da-z\.-]+)\.)?([\da-z\.-]+)\.([a-z\.]{2,6})([\/\w \.-\?_&]*)*){1,1000}$";
            Match match = Regex.Match(veriKontrol, pattern, RegexOptions.IgnoreCase);
            sonuc = match.Success;
        }
        else
        {
            sonuc = false;
        }
        return sonuc;
    }

    // başlık lar Clean.CleanUrl den geçtikten sonra kalacak olan urli kabul eder
    public static bool IsUrlBaslik(string veriKontrol)
    {
        bool sonuc;
        if (!string.IsNullOrEmpty(veriKontrol))
        {
            string pattern = @"^[a-z0-9\-]{1,100}$";
            Match match = Regex.Match(veriKontrol, pattern, RegexOptions.IgnoreCase);
            sonuc = match.Success;
        }
        else
        {
            sonuc = false;
        }
        return sonuc;
    }

    public static bool IsTelefon(string veriKontrol)
    {
        bool sonuc;
        if (!string.IsNullOrEmpty(veriKontrol))
        {
            string pattern = @"^0[1-9]{1}[0-9]{2} [0-9]{3} [0-9]{2} [0-9]{2}$";
            Match match = Regex.Match(veriKontrol, pattern, RegexOptions.IgnoreCase);
            sonuc = match.Success;
        }
        else
        {
            sonuc = false;
        }
        return sonuc;
    }

    public static bool IsAdres(string veriKontrol)
    {
        bool sonuc;
        if (!string.IsNullOrEmpty(veriKontrol))
        {
            string pattern = @"^['a-z''A-Z''0-9''ıİğĞüÜşŞçÇöÖÄäẞß''\-.!%&+?,:()\'/ ']{1,300}$";
            Match match = Regex.Match(veriKontrol, pattern, RegexOptions.IgnoreCase);
            sonuc = match.Success;
        }
        else
        {
            sonuc = false;
        }
        return sonuc;
    }

    public static bool IsSehir(string veriKontrol)
    {
        bool sonuc;
        if (!string.IsNullOrEmpty(veriKontrol))
        {
            string pattern = @"^['a-z''A-Z''0-9''ıİğĞüÜşŞçÇöÖÄäẞß''\-.!%&+?,:()\'/ ']{1,100}$";
            Match match = Regex.Match(veriKontrol, pattern, RegexOptions.IgnoreCase);
            sonuc = match.Success;
        }
        else
        {
            sonuc = false;
        }
        return sonuc;
    }

    /// <summary>
    /// Posta Kodu regex güncellenmeli; belki bunun için özel bir class dahi olabilir.
    /// XMl den ülke kodu ile eşleştiren bir şeyler.
    /// </summary>
    /// <param name="veriKontrol"></param>
    /// <returns></returns>
    public static bool IsPostaKodu(string veriKontrol)
    {
        bool sonuc;
        if (!string.IsNullOrEmpty(veriKontrol))
        {
            string pattern = @"^[0-9]{5}$";
            Match match = Regex.Match(veriKontrol, pattern, RegexOptions.IgnoreCase);
            sonuc = match.Success;
        }
        else
        {
            sonuc = false;
        }
        return sonuc;
    }

    public static bool IsBaslik(string veriKontrol)
    {
        bool sonuc;
        if (!string.IsNullOrEmpty(veriKontrol))
        {
            string pattern = @"^['a-z''A-Z''0-9''ıİğĞüÜşŞçÇöÖÄäẞß''\-.!%&+?,:()\""/✓³² ']{1,100}$";
            Match match = Regex.Match(veriKontrol, pattern, RegexOptions.IgnoreCase);
            sonuc = match.Success;
        }
        else
        {
            sonuc = false;
        }
        return sonuc;
    }

    public static bool IsTcKimlikNo(string veriKontrol)
    {
        bool sonuc;
        if (!string.IsNullOrEmpty(veriKontrol))
        {
            // 11 Haneli rakammı kontrol et
            string pattern = @"^['0-9'\s]{11}$";
            Match match = Regex.Match(veriKontrol, pattern, RegexOptions.IgnoreCase);
            sonuc = match.Success;

            // 11 hanli rakam ise geçerli TC nomu kontrol et
            if (sonuc)
            {
                var tcNo = Int64.Parse(veriKontrol);

                var atcno = tcNo / 100;
                var btcno = tcNo / 100;

                var c1 = atcno % 10; atcno = atcno / 10;
                var c2 = atcno % 10; atcno = atcno / 10;
                var c3 = atcno % 10; atcno = atcno / 10;
                var c4 = atcno % 10; atcno = atcno / 10;
                var c5 = atcno % 10; atcno = atcno / 10;
                var c6 = atcno % 10; atcno = atcno / 10;
                var c7 = atcno % 10; atcno = atcno / 10;
                var c8 = atcno % 10; atcno = atcno / 10;
                var c9 = atcno % 10; //atcno = atcno / 10;
                var q1 = ((10 - ((((c1 + c3 + c5 + c7 + c9) * 3) + (c2 + c4 + c6 + c8)) % 10)) % 10);
                var q2 = ((10 - (((((c2 + c4 + c6 + c8) + q1) * 3) + (c1 + c3 + c5 + c7 + c9)) % 10)) % 10);

                sonuc = ((btcno * 100) + (q1 * 10) + q2 == tcNo);
            }
        }
        else
        {
            sonuc = false;
        }
        return sonuc;
    }

    public static bool IsAciklama(string veriKontrol)
    {
        bool sonuc;
        if (!string.IsNullOrEmpty(veriKontrol))
        {
            string pattern = @"^['a-z''A-Z''0-9''ıİğĞüÜşŞçÇöÖÄäẞß''\-.!%&+?,:()\""/✓³² ']{1,500}$";
            Match match = Regex.Match(veriKontrol, pattern, RegexOptions.IgnoreCase);
            sonuc = match.Success;
        }
        else
        {
            sonuc = false;
        }
        return sonuc;
    }

    public static bool IsHexColor(string veriKontrol)
    {
        bool sonuc;
        if (!string.IsNullOrEmpty(veriKontrol))
        {
            string pattern = @"^#[0-9a-fA-F]{6}$";
            Match match = Regex.Match(veriKontrol, pattern, RegexOptions.IgnoreCase);
            sonuc = match.Success;
        }
        else
        {
            sonuc = false;
        }
        return sonuc;
    }

    public static bool IsMail(string veriKontrol)
    {
        bool sonuc;
        if (!string.IsNullOrEmpty(veriKontrol))
        {
            string pattern = @"^([a-zA-Z0-9\-_\.]{1,50})@([a-zA-Z0-9\-_]{1,50})\.([a-zA-Z0-9\-_]{1,10})(\.(['a-z''A-Z''0-9''\-_]{1,5}))?$";
            Match match = Regex.Match(veriKontrol, pattern, RegexOptions.IgnoreCase);
            sonuc = match.Success;
        }
        else
        {
            sonuc = false;
        }
        return sonuc;
    }

    public static bool IsSifre(string veriKontrol)
    {
        bool sonuc;
        if (!string.IsNullOrEmpty(veriKontrol))
        {
            //Minimum 8 characters at least 1 Alphabet, 1 Number and 1 Special Character:
            string pattern = @"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{8,}$";
            Match match = Regex.Match(veriKontrol, pattern, RegexOptions.IgnoreCase);
            sonuc = match.Success;
        }
        else
        {
            sonuc = false;
        }
        return sonuc;
    }


    /// <summary>
    /// Bu class gelen değerin harita enlem veya boylam koordinatı olup olmadığını kontrol ediyor. 
    /// </summary>
    /// <param name="veriKontrol"></param>
    /// <returns></returns>
    public static bool IsKoordinat(string veriKontrol)
    {
        bool sonuc;
        if (!string.IsNullOrEmpty(veriKontrol))
        {
            string pattern = @"^\d{1,5}.\d{1,20}$";
            Match match = Regex.Match(veriKontrol, pattern, RegexOptions.IgnoreCase);
            sonuc = match.Success;
        }
        else
        {
            sonuc = false;
        }
        return sonuc;
    }

    public static bool IsGoogleConsoleAdmin(string veriKontrol)
    {
        bool sonuc;
        if (!string.IsNullOrEmpty(veriKontrol))
        {
            string pattern = @"^['a-zA-Z''0-9''\-'\s]{1,60}$";
            Match match = Regex.Match(veriKontrol, pattern, RegexOptions.IgnoreCase);
            sonuc = match.Success;
        }
        else
        {
            sonuc = false;
        }
        return sonuc;
    }

    public static bool IsBingWebAdmin(string veriKontrol)
    {
        bool sonuc;
        if (!string.IsNullOrEmpty(veriKontrol))
        {
            string pattern = @"^['a-zA-Z''0-9'\s]{1,40}$";
            Match match = Regex.Match(veriKontrol, pattern, RegexOptions.IgnoreCase);
            sonuc = match.Success;
        }
        else
        {
            sonuc = false;
        }
        return sonuc;
    }

    public static bool IsYandexAdmin(string veriKontrol)
    {
        bool sonuc;
        if (!string.IsNullOrEmpty(veriKontrol))
        {
            string pattern = @"^['a-z''0-9'\s]{1,20}$";
            Match match = Regex.Match(veriKontrol, pattern, RegexOptions.IgnoreCase);
            sonuc = match.Success;
        }
        else
        {
            sonuc = false;
        }
        return sonuc;
    }

    public static bool IsTwitterUserName(string veriKontrol)
    {
        bool sonuc;
        if (!string.IsNullOrEmpty(veriKontrol))
        {
            string pattern = @"^[a-zA-Z0-9_]{1,15}$";
            Match match = Regex.Match(veriKontrol, pattern, RegexOptions.IgnoreCase);
            sonuc = match.Success;
        }
        else
        {
            sonuc = false;
        }
        return sonuc;
    }

    public static bool IsPinterestUserName(string veriKontrol)
    {
        bool sonuc;
        if (!string.IsNullOrEmpty(veriKontrol))
        {
            string pattern = @"^[a-zA-Z0-9_]{3,30}$";
            Match match = Regex.Match(veriKontrol, pattern, RegexOptions.IgnoreCase);
            sonuc = match.Success;
        }
        else
        {
            sonuc = false;
        }
        return sonuc;
    }

    public static bool IsInstagramUserName(string veriKontrol)
    {
        bool sonuc;
        if (!string.IsNullOrEmpty(veriKontrol))
        {
            string pattern = @"^[a-zA-Z0-9_.]{1,30}$";
            Match match = Regex.Match(veriKontrol, pattern, RegexOptions.IgnoreCase);
            sonuc = match.Success;
        }
        else
        {
            sonuc = false;
        }
        return sonuc;
    }

    public static bool IsFacebookUrl(string veriKontrol)
    {
        bool sonuc;
        if (!string.IsNullOrEmpty(veriKontrol))
        {
            string pattern = @"^((https\:\/\/)(?:www\.)?facebook\.com\/(?:[a-zA-Z0-9ıİğĞüÜşŞçÇöÖÄäẞß\-\._]{1,50})(\/)?)$";
            Match match = Regex.Match(veriKontrol, pattern, RegexOptions.IgnoreCase);
            sonuc = match.Success;
        }
        else
        {
            sonuc = false;
        }
        return sonuc;
    }

    public static bool IsLinkedinUrl(string veriKontrol) //maks 60 karakter
    {
        bool sonuc;
        if (!string.IsNullOrEmpty(veriKontrol))
        {
            string pattern = @"^((https:\/\/)((www\.)|[a-zA-Z0-9]{1,4}\.)?(linkedin\.com\/)([a-zA-Z0-9ıİğĞüÜşŞçÇöÖÄäẞß\-_.\/]{1,40}))$";
            Match match = Regex.Match(veriKontrol, pattern, RegexOptions.IgnoreCase);
            sonuc = match.Success;
        }
        else
        {
            sonuc = false;
        }
        return sonuc;
    }

    public static bool IsYoutubeEmbedUrl(string veriKontrol) //maks 60 karakter
    {
        bool sonuc;
        if (!string.IsNullOrEmpty(veriKontrol))
        {
            string pattern = @"^((https:\/\/)*(www\.?)*(?:youtube\.com\/\S*(?:(?:\/e(?:mbed))?\/|watch\/?\?(?:\S*?&?v\=))|youtu\.be\/)(['a-zA-Z''0-9''ıİğĞüÜşŞçÇöÖÄäẞß''\-_.']{1,50}))$";
            Match match = Regex.Match(veriKontrol, pattern, RegexOptions.IgnoreCase);
            sonuc = match.Success;
        }
        else
        {
            sonuc = false;
        }
        return sonuc;
    }

    public static bool IsYoutubeVideoUrl(string veriKontrol) //maks 60 karakter
    {
        bool sonuc;
        if (!string.IsNullOrEmpty(veriKontrol))
        {
            string pattern = @"^https:\/\/(?:www\.)?youtube.com\/watch\?v=(['a-zA-Z''0-9''\-_.']{1,50})$";
            Match match = Regex.Match(veriKontrol, pattern, RegexOptions.IgnoreCase);
            sonuc = match.Success;
        }
        else
        {
            sonuc = false;
        }
        return sonuc;
    }

    public static bool IsYoutubeKanalUrl(string veriKontrol)
    {
        bool sonuc;
        if (!string.IsNullOrEmpty(veriKontrol))
        {
            string pattern = @"^https\:\/\/(www\.|)youtube\.com\/(c\/|channel\/|user\/|)[a-zA-Z0-9\-_\.]{1,50}$";
            Match match = Regex.Match(veriKontrol, pattern, RegexOptions.IgnoreCase);
            sonuc = match.Success;
        }
        else
        {
            sonuc = false;
        }
        return sonuc;
    }


    public static bool IsNumber(string veriKontrol)
    {
        bool sonuc;
        if (!string.IsNullOrEmpty(veriKontrol))
        {
            string pattern = @"^[0-9]{1,19}$";
            Match match = Regex.Match(veriKontrol, pattern, RegexOptions.IgnoreCase);
            sonuc = match.Success;
        }
        else
        {
            sonuc = false;
        }
        return sonuc;
    }

    public static bool IsYuzdeOrani(string veriKontrol)
    {
        bool sonuc;
        if (!string.IsNullOrEmpty(veriKontrol))
        {
            string pattern = @"^(100|\d{1,2})$";
            Match match = Regex.Match(veriKontrol, pattern, RegexOptions.IgnoreCase);
            sonuc = match.Success;
        }
        else
        {
            sonuc = false;
        }
        return sonuc;
    }

    public static bool IsYorumMetin(string veriKontrol)
    {
        bool sonuc;
        if (!string.IsNullOrEmpty(veriKontrol))
        {
            string pattern = @"^['a-zA-Z''0-9''ıİğĞüÜşŞçÇöÖÄäẞß''\-.!%&+?,\:()\'\/'\s]{1,1000}";
            Match match = Regex.Match(veriKontrol, pattern, RegexOptions.IgnoreCase);
            sonuc = match.Success;
        }
        else
        {
            sonuc = false;
        }
        return sonuc;
    }

    public static bool IsParamiktari(string veriKontrol)
    {
        string pattern = @"^((\d{1,12})[\,](\d{2}))$";
        Match match = Regex.Match(veriKontrol, pattern, RegexOptions.IgnoreCase);

        return match.Success;
    }

    public static bool IsTarih(string veriKontrol)
    {
        bool sonuc;
        if (!string.IsNullOrEmpty(veriKontrol))
        {
            string pattern = @"^((\d{2})-(\d{2})-(\d{4}))$";
            Match match = Regex.Match(veriKontrol, pattern, RegexOptions.IgnoreCase);
            sonuc = match.Success;
        }
        else
        {
            sonuc = false;
        }
        return sonuc;
    }

    public static bool IsTarihYil(string veriKontrol)
    {
        bool sonuc;
        if (!string.IsNullOrEmpty(veriKontrol))
        {
            string pattern = @"([1-9]{1}\d{3})$";
            Match match = Regex.Match(veriKontrol, pattern, RegexOptions.IgnoreCase);
            sonuc = match.Success;
        }
        else
        {
            sonuc = false;
        }
        return sonuc;
    }

    public static bool IsTarihAy(string veriKontrol)
    {
        bool sonuc;
        if (!string.IsNullOrEmpty(veriKontrol))
        {
            string pattern = @"[1-12]$";
            Match match = Regex.Match(veriKontrol, pattern, RegexOptions.IgnoreCase);
            sonuc = match.Success;
        }
        else
        {
            sonuc = false;
        }
        return sonuc;
    }

    public static bool IsTarihGun(string veriKontrol)
    {
        bool sonuc;
        if (!string.IsNullOrEmpty(veriKontrol))
        {
            string pattern = @"[1-31]$";
            Match match = Regex.Match(veriKontrol, pattern, RegexOptions.IgnoreCase);
            sonuc = match.Success;
        }
        else
        {
            sonuc = false;
        }
        return sonuc;
    }

    public static bool IsSaat(string veriKontrol)
    {
        bool sonuc;
        if (!string.IsNullOrEmpty(veriKontrol))
        {
            string pattern = @"^[0-23]:[0-59]$$";
            Match match = Regex.Match(veriKontrol, pattern, RegexOptions.IgnoreCase);
            sonuc = match.Success;
        }
        else
        {
            sonuc = false;
        }
        return sonuc;
    }


    /// <summary>
    /// başında ve sonunda virgül olan bir birinden vigül ile ayrılmış rakamlar (Idler) için
    /// bir ürünün renkler, resimleri, bedenleri vs.. eti de ,1,3,5, şeklinde tutuluyor db de.
    /// </summary>
    /// <param name="veriKontrol"></param>
    /// <returns></returns>
    public static bool IsIdList(string veriKontrol)
    {
        bool sonuc;
        if (!string.IsNullOrEmpty(veriKontrol))
        {
            // maks 1000 karakter
            if (veriKontrol.Length < 1001)
            {
                string pattern = @"^[,][0-9]+(,[0-9]+)*[,]$";
                Match match = Regex.Match(veriKontrol, pattern, RegexOptions.IgnoreCase);
                sonuc = match.Success;
            }
            else sonuc = false;
        }
        else
        {
            sonuc = false;
        }
        return sonuc;
    }

    /// <summary>
    /// başında ve sonunda virgül olan bir birinden vigül ile ayrılmış rakamlar (Idler) için
    /// Bir üsttekinden temel farkı en az iki 2 ID olması şartı koşmasıdır.
    /// </summary>
    /// <param name="veriKontrol"></param>
    /// <returns></returns>
    public static bool IsIdListMin2ID(string veriKontrol)
    {
        bool sonuc;
        if (!string.IsNullOrEmpty(veriKontrol))
        {
            // maks 1000 karakter
            if (veriKontrol.Length < 1001)
            {
                string pattern = @"^[,][0-9]+[,][0-9]+(,[0-9]+)*[,]$";
                Match match = Regex.Match(veriKontrol, pattern, RegexOptions.IgnoreCase);
                sonuc = match.Success;
            }
            else sonuc = false;
        }
        else
        {
            sonuc = false;
        }
        return sonuc;
    }

    /// <summary>
    /// bir birinden virgül ile ayırlımış kelimeler lsitesi
    /// etic de filtrelemelerin özellik (renk, beden vb..) url de parametre olarak birbirinden virgül ile ayrılmış olarak geliyor
    /// </summary>
    /// <param name="veriKontrol"></param>
    /// <returns></returns>
    public static bool IsOzellikListesi(string veriKontrol)
    {
        bool sonuc;
        if (!string.IsNullOrEmpty(veriKontrol))
        {
            // maks 1000 karakter
            if (veriKontrol.Length < 1001)
            {
                string pattern = @"^[a-zA-Z0-9\-]+(,[a-zA-Z0-9\-]+)*$";
                Match match = Regex.Match(veriKontrol, pattern, RegexOptions.IgnoreCase);
                sonuc = match.Success;
            }
            else sonuc = false;
        }
        else
        {
            sonuc = false;
        }
        return sonuc;
    }

    public static bool IsIcerikUrllist(string veriKontrol)
    {
        bool sonuc;
        if (!string.IsNullOrEmpty(veriKontrol))
        {
            // maks 1000 karakter
            if (veriKontrol.Length < 1001)
            {
                string pattern = @"^[,][a-zA-Z0-9\-]+(,[a-zA-Z0-9\-]+)*[,]$";
                Match match = Regex.Match(veriKontrol, pattern, RegexOptions.IgnoreCase);
                sonuc = match.Success;
            }
            else sonuc = false;
        }
        else
        {
            sonuc = false;
        }
        return sonuc;
    }


    /// <summary>
    /// 100tl-400tl gibi para aralıkları için kullanıyorum
    /// e-ticaret vs.. de filtrelemelerde fiyat aralığı arametresi kontrolü
    /// tl de büyük küçük harf fark etmiyor.
    /// </summary>
    /// <param name="veriKontrol"></param>
    /// <returns></returns>
    public static bool IsParaAraligi(string veriKontrol)
    {
        bool sonuc;
        if (!string.IsNullOrEmpty(veriKontrol))
        {
            // maks 100 karakter
            if (veriKontrol.Length < 101)
            {
                // javascriptte i (case insetive) dışarıda ve soru işareti yok burada böyle
                string pattern = @"^\d+(tl)\-\d+(tl)(?i)$";
                Match match = Regex.Match(veriKontrol, pattern, RegexOptions.IgnoreCase);
                sonuc = match.Success;
            }
            else sonuc = false;
        }
        else
        {
            sonuc = false;
        }
        return sonuc;
    }

    /// <summary>
    /// Bu kontrol gelen verinin bir sıcaklık (daha doğrusu değer rakamsal değer) arallığı olup olmadığını kontrol ediyor
    /// gelen değer negatif ve/veya pozitif sayılar olabilir.
    /// -20 +40
    /// 20 40
    /// 20 +40 vs..
    /// </summary>
    /// <param name="veriKontrol"></param>
    /// <returns></returns>
    public static bool IsDegerAraligi(string veriKontrol)
    {
        bool sonuc;
        if (!string.IsNullOrEmpty(veriKontrol))
        {
            // maks 100 karakter
            if (veriKontrol.Length < 101)
            {
                string pattern = @"^[\-\+]?[0-9]* [\-\+]?[0-9]*$";
                Match match = Regex.Match(veriKontrol, pattern, RegexOptions.IgnoreCase);
                sonuc = match.Success;
            }
            else sonuc = false;
        }
        else
        {
            sonuc = false;
        }
        return sonuc;
    }

    /// <summary>
    /// decimal veya integer sayı olup olmadığını kontrol ediyor.
    /// eticaretlerde dinamik özellik içinde sayısal alanlarada minimum ve maksimum değerleri kontrol için kullanıyorum
    /// 100     150     200.20      1.25    gibi değerler olabilir
    /// </summary>
    /// <param name="veriKontrol"></param>
    /// <returns></returns>
    public static bool IsSayiMinMaskDeger(string veriKontrol)
    {
        bool sonuc;

        // maks 30 karakter
        if (veriKontrol.Length < 31)
        {
            string pattern = @"^\d{0,19}(\.\d{2})?$";
            Match match = Regex.Match(veriKontrol, pattern, RegexOptions.IgnoreCase);
            sonuc = match.Success;
        }
        else sonuc = false;

        return sonuc;
    }

    /// <summary>
    /// gelen verinin CSS de kullanılabilecek bir pozisyon değeri olup olmadığı kontrol ediliyro.
    /// 0 0 ve 15px 15px gibi değerler kabul ediliyor.
    /// 0 da px yok
    /// 0 dışındakilerde px var
    /// </summary>
    /// <param name="veriKontrol"></param>
    /// <returns></returns>
    public static bool IsCssPozisyonDeger(string veriKontrol)
    {
        bool sonuc;

        // maks 30 karakter
        if (veriKontrol.Length < 31)
        {
            string pattern = @"^(\-?(0|[1-9]\d*px)) (\-?(0|[1-9]\d*px))$";
            Match match = Regex.Match(veriKontrol, pattern, RegexOptions.IgnoreCase);
            sonuc = match.Success;
        }
        else sonuc = false;

        return sonuc;
    }

    /// <summary>
    /// Css kullanılan fotların icon kod değerleri olup olmadığını kontrol ediyorum.
    /// </summary>
    /// <param name="veriKontrol"></param>
    /// <returns></returns>
    public static bool IsCssFontIconDeger(string veriKontrol)
    {
        bool sonuc;

        // maks 50 karakter
        if (veriKontrol.Length < 51)
        {
            string pattern = @"^icon-[a-z0-9\-][a-z0-9\-]*$";
            Match match = Regex.Match(veriKontrol, pattern, RegexOptions.IgnoreCase);
            sonuc = match.Success;
        }
        else sonuc = false;

        return sonuc;
    }

    public static bool IsApplePlayStroreAppUrl(string veriKontrol)
    {
        bool sonuc;

        // maks 500 karakter
        if (veriKontrol.Length < 500)
        {
            string pattern = @"^(https:\/\/apps\.apple\.com\/)['a-z''A-Z']{2,6}(\/app\/)['a-z''A-Z''0-9''\-\.\%']{1,60}(\/id)['0-9']*((\?l\=)['a-z''A-Z']{2,6})?$";
            Match match = Regex.Match(veriKontrol, pattern, RegexOptions.IgnoreCase);
            sonuc = match.Success;
        }
        else sonuc = false;

        return sonuc;
    }

    public static bool IsGooglePlayStroreAppUrl(string veriKontrol)
    {
        bool sonuc;

        // maks 500 karakter
        if (veriKontrol.Length < 500)
        {
            // bu expresion yazılırken google maks 30 karakter isme izin veriyordu ama ben işimi sağlama aldım ve 40 yaptım isim kısmını
            // tüm URL maks uzunluğu 90 karakter oluyor.
            string pattern = @"^(https\:\/\/play\.google\.com\/store\/apps\/details\?id=com\.)+['a-z''A-Z''0-9''\-\.']{1,40}$";
            Match match = Regex.Match(veriKontrol, pattern, RegexOptions.IgnoreCase);
            sonuc = match.Success;
        }
        else sonuc = false;

        return sonuc;
    }
}
