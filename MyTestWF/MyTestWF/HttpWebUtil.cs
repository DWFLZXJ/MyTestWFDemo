using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Net.Cache;
using System.Drawing;




namespace MyTestWF
{
   public class HttpWebUtil
   {
       #region 请求地址获取内容
       public bool Post( string PostURL, string EncodeName, out string strMsg)
       {
          
           try
           {
              

              // byte[] bytes = Encoding.GetEncoding(EncodeName).GetBytes(strSrc);
               Uri url = new Uri(PostURL);
               HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
               #region request属性设置
               //request.Credentials = CredentialCache.DefaultCredentials;
               request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);
               request.KeepAlive = true;
               request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022; InfoPath.3)";
               request.Method = "POST";
               request.Timeout = 60000;
               request.Accept = "image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/x-ms-application, application/x-ms-xbap, application/vnd.ms-xpsdocument, application/xaml+xml, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, */*";
               request.ProtocolVersion = HttpVersion.Version11;
               request.AllowAutoRedirect = true;
               //request.ContentType = "text/plain";
               request.ContentType = "application/x-www-form-urlencoded";   //很关键，一定要用这个，不然数据提交不上去
               //request.ContentLength = bytes.Length;
               request.Proxy = null;
               #endregion
              

               HttpWebResponse response = (HttpWebResponse)request.GetResponse();

             Stream  dataStream = response.GetResponseStream();



               StreamReader reader = new StreamReader(dataStream, Encoding.GetEncoding(EncodeName), true);
               strMsg = reader.ReadToEnd();
               reader.Close();
               dataStream.Close();
               response.Close();
               return true;
           }
           catch (Exception ex)
           {
               strMsg = ex.Message;
               return false;
           }

       }
       #endregion
       #region 获取图片
       public  bool downloadFile(string url, string savePath)
       {
           Exception ex;
           int fileSize;
           HttpWebRequest req = null;
           HttpWebResponse res = null;
           System.IO.Stream stream = null;
           bool result = false;
           System.GC.Collect();
           try
           {
               req = HttpWebRequest.Create(url) as System.Net.HttpWebRequest;
               req.CookieContainer = new CookieContainer();
               req.AllowAutoRedirect = true;
               req.KeepAlive = false;
               req.Timeout = 60 * 1000;
               req.ServicePoint.ConnectionLeaseTimeout = 2 * 60 * 1000;
               // req.ServicePoint.ConnectionLimit = int.MaxValue;

               req.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
               req.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:9.0.1) Gecko/20100101 Firefox/9.0.1";
               req.Headers.Add("Accept-Language", "en-us,en;q=0.5");
               req.Headers.Add("Accept-Charset", "ISO-8859-1,utf-8;q=0.7,*;q=0.7");
               req.Headers.Add("Accept-Encoding", "gzip, deflate");


               res = req.GetResponse() as System.Net.HttpWebResponse;
               stream = res.GetResponseStream();
               byte[] buffer = new byte[10 * 1024];
               int bytesProcessed = 0;
               System.IO.FileStream fs = new FileStream(savePath, FileMode.Create, FileAccess.Write);
               int bytesRead;
               do
               {
                   bytesRead = stream.Read(buffer, 0, buffer.Length);
                   fs.Write(buffer, 0, bytesRead);
                   bytesProcessed += bytesRead;
               }
               while (bytesRead > 0);
               fs.Flush();
               fs.Close();
               result = true;
               fileSize = bytesProcessed;
           }
           catch (Exception exception)
           {
               ex = exception;
           }
           finally
           {
               if (stream != null)
               {
                   stream.Close(); stream.Dispose();
               }
               if (req != null)
               {
                   req.Abort();
                   req = null;
               }
               if (res != null)
               {
                   res.Close();
                   res = null;
               }
           }
           return result;
       }
     
          #endregion
       #region 获取图片bitmap
       public bool GetImg(string PostURL, string EncodeName, out string strMsg)
       {
           Bitmap img = null;
           try
           {


               // byte[] bytes = Encoding.GetEncoding(EncodeName).GetBytes(strSrc);
               Uri url = new Uri(PostURL);
               HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
               #region request属性设置
               //request.Credentials = CredentialCache.DefaultCredentials;
               request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);
               request.KeepAlive = true;
               request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022; InfoPath.3)";
               request.Method = "POST";
               request.Timeout = 60000;
               request.Accept = "image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/x-ms-application, application/x-ms-xbap, application/vnd.ms-xpsdocument, application/xaml+xml, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, */*";
               request.ProtocolVersion = HttpVersion.Version11;
               request.AllowAutoRedirect = true;
               //request.ContentType = "text/plain";
               request.ContentType = "application/x-www-form-urlencoded";   //很关键，一定要用这个，不然数据提交不上去
               //request.ContentLength = bytes.Length;
               request.Proxy = null;
               #endregion

               HttpWebResponse response = (HttpWebResponse)request.GetResponse();

               Stream dataStream = response.GetResponseStream();
               img = new Bitmap(dataStream);
               img.Save(@"F:/MyDemoCode/TestFolder/test.png");

               dataStream.Close();
               response.Close();
               strMsg = "";
               return true;
           }
           catch (Exception ex)
           {
               strMsg = ex.Message;
               return false;
           }

       }
      #endregion


   }
}
