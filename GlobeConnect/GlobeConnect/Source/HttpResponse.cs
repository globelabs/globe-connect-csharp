//
// HttpResponse.cs
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
using System.Net;
using Newtonsoft.Json;

namespace Globe.Connect
{
	public class HttpResponse
	{
		/* Raw response */
		protected HttpWebResponse rawResponse = null;

		/* Response string */
		protected string response = null;

		/**
		 * Create http response without parameter.
		 */
		public HttpResponse()
		{
		}

		/**
		 * Create http response with response parameter.
		 *
		 * @param string
		 */
		public HttpResponse(string response)
		{
			// set response string
			this.response = response;
		}

		/**
		 * Create http response with http web response parameter.
		 *
		 * @param HttpWebResponse
		 */
		public HttpResponse(HttpWebResponse rawResponse)
		{
			// set raw response
			this.rawResponse = rawResponse;
		}

		/**
		 * Create http response with http web response
		 * and response string parameter.
		 *
		 * @param HttpWebResponse
		 * @param string
		 */
		public HttpResponse(HttpWebResponse rawResponse, string response)
		{
			// set raw response
			this.rawResponse = rawResponse;
			// set response string
			this.response = response;
		}

		/**
		 * Set response string.
		 *
		 * @param  string
		 * @return this
		 */
		public HttpResponse SetResponse(string response)
		{
			// set response string
			this.response = response;

			return this;
		}

		/**
		 * Returns raw response.
		 *
		 * @return HttpWebResponse
		 */
		public HttpWebResponse GetRawResponse()
		{
			return this.rawResponse;
		}

		/**
		 * Returns response string.
		 *
		 * @return string.
		 */
		public string GetResponse()
		{
			// read response string
			string responseString = new StreamReader(this.rawResponse.GetResponseStream()).ReadToEnd();

			return responseString;
		}

		/**
		 * Get response as dynamic object.
		 *
		 * @return dynamic
		 */
		public dynamic GetDynamicResponse()
		{
			// deserialize object
			dynamic deserialized = JsonConvert.DeserializeObject<dynamic>(this.GetResponse());

			return deserialized;
		}
	}
}
