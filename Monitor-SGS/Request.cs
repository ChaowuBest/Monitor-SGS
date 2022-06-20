using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;

namespace Monitor_SGS
{
    internal class Request
    {
        public bool httpRequest(string url, string info, Encoding encoding, Encoding streamEncoding)
        {
        A: HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            byte[] contentByte = streamEncoding.GetBytes(info);
            req.Timeout = 5000;
            req.Method = "POST";
            req.ContentLength = contentByte.Length;
            req.Accept = "*/*";
            req.Headers.Add("Origin", "https://marknad.sgs.se");
            req.Headers.Add("Sec-Fetch-Dest", "empty");
            req.Headers.Add("Sec-Fetch-Mode", "cors");
            req.Headers.Add("Sec-Fetch-Site", "same-origin");
            req.Headers.Add("X-Requested-With", "XMLHttpRequest");
            req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/101.0.4951.67 Safari/537.36 OPR/87.0.4390.45";
            req.ContentType = "application/x-www-form-urlencoded";
            Stream webstream = req.GetRequestStream();
            webstream.Write(contentByte, 0, contentByte.Length);
            webstream.Close();
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)req.GetResponse())
                {
                    StreamReader readhtmlStream = null;
                    string readtoend = null;
                    if (response.ContentEncoding == "gzip")
                    {
                        Stream tokenStream = response.GetResponseStream();
                        readhtmlStream = new StreamReader(new GZipStream(tokenStream, CompressionMode.Decompress), encoding);
                        readtoend = readhtmlStream.ReadToEnd();
                    }
                    else
                    {
                        Stream tokenStream = response.GetResponseStream();
                        readhtmlStream = new StreamReader(tokenStream, encoding);
                        readtoend = readhtmlStream.ReadToEnd();
                    }
                    JObject jo = JObject.Parse(readtoend);
                    try
                    {
                        if (int.Parse(jo["TotalCount"].ToString()) > 0)
                        {
                            Console.WriteLine("Release!!!!");
                            return true;
                        }
                        Console.WriteLine("SGS Still Not Release");
                        return false;
                    }
                    catch
                    {
                        Console.WriteLine("Json Error");
                        goto A;
                    }
                }

            }
            catch (WebException)
            {
                Console.WriteLine("Web Error");
                goto A;
            }
            catch (Exception)
            {
                Console.WriteLine("Error");
                goto A;
            }
        }
    }
}
