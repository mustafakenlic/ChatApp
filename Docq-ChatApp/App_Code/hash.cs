using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public static class hash
{
    private const string _urlSec = "¼Q*ÛêB0Ã7*C¨}|7½ÂGú§¯_$âÍ&+.êv'ÛTø°â7ÈÔï\"öºÎìãæv3(TòqòY~·Ã@âdf¡æ$Ð$å(Ð¯ÙkV/|[;/[-U[af*/asdf£#$£#¥Î«ìæbG½#$½23423o3½a¹Qu#$Wùsdf8s\\üâ&o¨ÖZêv'ÛTø°â7ÈÔ#$£#$æßßieµÍrf¤¤eGWÇÀ=W¶9ÂsNÎ[¾ÒÇV/|åV±Íqì6as4<¼ã[;¤[¿°7TV/|]oÎ¤S¤K,Æ.QòmÃ,¿èÝ{rÜi¬ð®âÌí¡$^WñJôÖBºDÛKÀXn|[;bV/i¤¤K½4&56sdf233£#$af*/asdf£#$£#½£#$£qòY~·Ã@â+.'J®m¨É²ôî)ÑNqP/[-Urft<±íZ9";

    //private static readonly clean clean = new clean();

    static readonly SHA512Managed HashIslem = new SHA512Managed();

    public static string SesionGuvenlik(string hashId, string sesCode)
    {
        byte[] clacHash = HashIslem.ComputeHash(Encoding.UTF8.GetBytes(
            BitConverter.ToString(HashIslem.ComputeHash(Encoding.UTF8.GetBytes(_urlSec))).Replace("-", "") +
            BitConverter.ToString(HashIslem.ComputeHash(Encoding.UTF8.GetBytes(hashId))).Replace("-", "") +
            BitConverter.ToString(HashIslem.ComputeHash(Encoding.UTF8.GetBytes(sesCode))).Replace("-", "")
        ));
        string hashSonuc = BitConverter.ToString(clacHash).Replace("-", "").ToLower();
        return hashSonuc;
    }

    public static string UserNameHash(string userName)
    {
        byte[] clacHash = HashIslem.ComputeHash(Encoding.UTF8.GetBytes(
            BitConverter.ToString(HashIslem.ComputeHash(Encoding.UTF8.GetBytes(userName.ToLower().Trim()))).Replace("-", "")
        ));
        string hashSonuc = BitConverter.ToString(clacHash).Replace("-", "");
        return hashSonuc;
    }

    public static string UserPassHash(string userPass, string salt)
    {
        byte[] clacHash = HashIslem.ComputeHash(Encoding.UTF8.GetBytes(
            BitConverter.ToString(HashIslem.ComputeHash(Encoding.UTF8.GetBytes(_urlSec))).Replace("-", "") +
            BitConverter.ToString(HashIslem.ComputeHash(Encoding.UTF8.GetBytes(userPass.Trim()))).Replace("-", "") +
            BitConverter.ToString(HashIslem.ComputeHash(Encoding.UTF8.GetBytes(_urlSec))).Replace("-", "") +
            BitConverter.ToString(HashIslem.ComputeHash(Encoding.UTF8.GetBytes(salt))).Replace("-", "") +
            BitConverter.ToString(HashIslem.ComputeHash(Encoding.UTF8.GetBytes("n$qi|pxA5UEn|[;bV/i¤¤KN[;/[-UQA5r.gVQn|[;bV/i¤¤*{)$DtJUU.¤eGWÇÀ=¤K,CiVbV/j:u.M(CiKA5UENbV/|[;/<ENrf)eM'^+%(/&%+;rwnQAj:un<rn<+Ka"))).Replace("-", "")
        ));
        string hashSonuc = BitConverter.ToString(clacHash).Replace("-", "");
        return hashSonuc;
    }
    
    public static string GetUniqueKey(int maxSize)
    {
        var chars = "0123456789@ABCDEFGHIJKLMNOPSTUVWXYZabcdefghijklmnopqrstuvwxyz".ToCharArray();
        byte[] data = new byte[1];
        using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
        {
            crypto.GetNonZeroBytes(data);
            data = new byte[maxSize];
            crypto.GetNonZeroBytes(data);
        }
        StringBuilder result = new StringBuilder(maxSize);
        foreach (byte b in data)
        {
            result.Append(chars[b % (chars.Length)]);
        }
        return result.ToString();
    }
}
