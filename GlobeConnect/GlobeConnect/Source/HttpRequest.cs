//
// HttpRequest.cs
//
// Author:
//       Charles Zamora <czamora@openovate.com>
//
// Copyright (c) 2016
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;
using System.IO;
using System.Text;
using System.Net;
using Newtonsoft.Json;

namespace Globe.Connect
{
	public class HttpRequest
	{
		/* Default user agent */
		const string USER_AGENT = "Mozilla/5.0";

		/* Default content type */
		const string CONTENT_TYPE = "application/json";

		/* Http GET Method */
		const string HTTP_GET = "GET";

		/* Http POST Method */
		const string HTTP_POST = "POST";

		/* Target url */
		protected string url = null;

		/* Request data */
		protected dynamic data = null;

		/**
		 * Create http request without parameter.
		 */
		public HttpRequest()
		{
		}

		/**
		 * Create http request with url parameter.
		 * 
		 * @param  string
		 */
		public HttpRequest(string url)
		{
			// set target url
			this.url = url;
		}

		/**
		 * Set target url.
		 * 
		 * @param  string
		 * @return this
		 */
		public HttpRequest SetUrl(string url)
		{
			// set target url
			this.url = url;

			return this;
		}

		/**
		 * Set request data.
		 * 
		 * @param  dynamic
		 * @return this
		 */
		public HttpRequest SetData(dynamic data)
		{
			// set request data
			this.data = data;

			return this;
		}

		/**
		 * Sends get request.
		 * 
		 * @return HttpResponse
		 */
		public HttpResponse SendGet()
		{
			// initialize http web request
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(this.url);

			// set request method
			request.Method = HTTP_GET;

			// set user agent
			request.UserAgent = USER_AGENT;

			// set content type
			request.ContentType = CONTENT_TYPE;

			// create http web response
			HttpWebResponse response = (HttpWebResponse)request.GetResponse();

			return new HttpResponse(response);
		}

		/**
		 * Sends post request.
		 * 
		 * @return HttpResponse
		 */
		public HttpResponse SendPost()
		{
			// initialize http web request
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(this.url);

			// parse data into json string
			string data = JsonConvert.SerializeObject(this.data);

			Console.WriteLine(this.url);
			Console.WriteLine(data);

			// convert data to bytes
			byte[] raw = Encoding.UTF8.GetBytes(data);

			// set request method
			request.Method = HTTP_POST;

			// set user agent
			request.UserAgent = USER_AGENT;

			// set content type
			request.ContentType = CONTENT_TYPE;

			// set content length
			request.ContentLength = raw.Length;

			// write request data via stream
			using (Stream stream = request.GetRequestStream())
			{
				// write data
				stream.Write(raw, 0, raw.Length);
			}

			// create http web response
			HttpWebResponse response = (HttpWebResponse)request.GetResponse();

			return new HttpResponse(response);
		}
	}
}
