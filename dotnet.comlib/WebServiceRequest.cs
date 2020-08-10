/*
 * Copyright (c) 2020 ZP
 * Revision: 2.1
 * CLR: 4.0.30319.42000
 * Date 2/15/2020 10:29:54 PM
 * Name WebServiceRequest
 * Create on device ZPX.ZPX
 * Author Create By ZHAOPAN
 * Describe something
 *
 */

using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Comlib
{
    /// <summary>
    /// http请求类
    /// </summary>
    public static class WebServiceRequest
    {
        /// <summary>
        /// http get
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="httpContext"></param>
        /// <param name="timeOut">超时时间（秒）</param>
        /// <returns></returns>
        public static string Get(string url, Dictionary<string, object> queryParam, int timeOut = 60)
        {
            var queryString = "?";
            foreach (var key in queryParam.Keys)
            {
                queryString += key + "=" + queryParam[key] + "&";
            }

            queryString = queryString.Substring(0, queryString.Length - 1);

            HttpWebRequest httpWebRequest = null;
            HttpWebResponse httpWebResponse = null;
            StreamReader streamReader = null;

            try
            {
                httpWebRequest = (HttpWebRequest)WebRequest.Create(url + queryString);

                httpWebRequest.ContentType = "application/x-www-form-urlencoded";
                httpWebRequest.UserAgent = "Mozilla/5.0 (Windows NT 5.1) AppleWebKit/537.1 (KHTML, like Gecko) Chrome/21.0.1180.89 Safari/537.1";
                httpWebRequest.Method = "GET";
                httpWebRequest.Timeout = timeOut * 1000;
                httpWebRequest.Proxy = null;

                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                streamReader = new StreamReader(httpWebResponse.GetResponseStream());
                return streamReader.ReadToEnd();
            }
            catch
            {
                throw;
            }
            finally
            {
                if (httpWebResponse != null)
                {
                    httpWebResponse.Close();
                }
                if (streamReader != null)
                {
                    streamReader.Close();
                }
                if (httpWebRequest != null)
                {
                    httpWebRequest.Abort();
                }
            }
        }

        /// <summary>
        /// get
        /// </summary>
        /// <param name="url"></param>
        /// <param name="timeOut">请求超时（秒）</param>
        /// <returns></returns>
        public static string GetHtml(string url, int timeOut = 60)
        {
            return Get(url, "application/x-www-form-urlencoded", timeOut);
        }

        /// <summary>
        /// get
        /// </summary>
        /// <param name="url"></param>
        /// <param name="timeOut">请求超时（秒）</param>
        /// <returns></returns>
        public static string GetJson(string url, int timeOut = 60)
        {
            return Get(url, "application/json; charset=utf-8", timeOut);
        }

        /// <summary>
        /// post
        /// </summary>
        /// <param name="url"></param>
        /// <param name="body">body参数格式id=1&name=张三</param>
        /// <param name="timeOut">超时时间（秒）</param>
        /// <returns></returns>
        public static string PostHtml(string url, string body, int timeOut = 60)
        {
            return Post(url, body, "application/x-www-form-urlencoded", timeOut);
        }

        /// <summary>
        /// post
        /// </summary>
        /// <param name="url"></param>
        /// <param name="body">body参数格式{name='张三',id=1}</param>
        /// <param name="timeOut">超时时间（秒）</param>
        /// <returns></returns>
        public static string PostJson(string url, string body, int timeOut = 60)
        {
            return Post(url, body, "application/json; charset=utf-8", timeOut);
        }

        private static string Get(string url, string contentType, int timeOut)
        {
            HttpWebRequest httpWebRequest = null;
            HttpWebResponse httpWebResponse = null;
            StreamReader streamReader = null;
            try
            {
                httpWebRequest = (HttpWebRequest)WebRequest.Create(url);

                httpWebRequest.ContentType = contentType;
                httpWebRequest.UserAgent = "Mozilla/5.0 (Windows NT 5.1) AppleWebKit/537.1 (KHTML, like Gecko) Chrome/21.0.1180.89 Safari/537.1";
                httpWebRequest.Method = "GET";
                httpWebRequest.Timeout = timeOut * 1000;
                httpWebRequest.Proxy = null;

                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                streamReader = new StreamReader(httpWebResponse.GetResponseStream());
                return streamReader.ReadToEnd();
            }
            catch
            {
                throw;
            }
            finally
            {
                if (httpWebResponse != null)
                {
                    httpWebResponse.Close();
                }
                if (streamReader != null)
                {
                    streamReader.Close();
                }
                if (httpWebRequest != null)
                {
                    httpWebRequest.Abort();
                }
            }
        }

        private static string Post(string url, string body, string contentType, int timeOut)
        {
            HttpWebRequest httpWebRequest = null;
            HttpWebResponse httpWebResponse = null;
            StreamReader streamReader = null;

            try
            {
                httpWebRequest = (HttpWebRequest)WebRequest.Create(url);

                httpWebRequest.ContentType = contentType;
                httpWebRequest.UserAgent = "Mozilla/5.0 (Windows NT 5.1) AppleWebKit/537.1 (KHTML, like Gecko) Chrome/21.0.1180.89 Safari/537.1";
                httpWebRequest.Method = "POST";
                httpWebRequest.Timeout = timeOut * 1000;
                httpWebRequest.Proxy = null;

                var btBodys = Encoding.UTF8.GetBytes(body);
                httpWebRequest.ContentLength = btBodys.Length;
                httpWebRequest.GetRequestStream().Write(btBodys, 0, btBodys.Length);

                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                streamReader = new StreamReader(httpWebResponse.GetResponseStream());
                return streamReader.ReadToEnd();
            }
            catch
            {
                throw;
            }
            finally
            {
                if (httpWebResponse != null)
                {
                    httpWebResponse.Close();
                }
                if (streamReader != null)
                {
                    streamReader.Close();
                }
                if (httpWebRequest != null)
                {
                    httpWebRequest.Abort();
                }
            }
        }
    }
}