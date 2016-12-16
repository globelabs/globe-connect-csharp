//
// Authentication.cs
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
using System.Collections.Specialized;

namespace Globe.Connect
{
	public class Authentication : Context
	{
		/* Default api host */
		const string API_HOST = "https://developer.globelabs.com.ph";

		/* Default dialog url */
		const string DIALOG_URL = "/dialog/oauth";

		/* Default access url */
		const string ACCESS_URL = "/oauth/access_token";

		/* App id */
		protected string appId = null;

		/* App secret */
		protected string appSecret = null;

		/**
		 * Creates authentication class without parameters.
		 */
		public Authentication() : base()
		{
		}

		/**
		 * Creates authentication class with
		 * app id and app secret parameters.
		 * 
		 * @param  string
		 * @param  string
		 */
		public Authentication(string appId, string appSecret) : base()
		{
			// set app id
			this.appId = appId;
			// set app secret
			this.appSecret = appSecret;
		}

		/**
		 * Returns the app id.
		 * 
		 * @return string
		 */
		public string GetAppId()
		{
			return this.appId;
		}

		/**
		 * Returns the app secret.
		 * 
		 * @return string
		 */
		public string GetAppSecret()
		{
			return this.appSecret;
		}

		/**
		 * Returns the access url.
		 * 
		 * @return string
		 */
		public string GetAccessUrl()
		{
			return API_HOST + ACCESS_URL;
		}

		/**
		 * Returns the dialog url.
		 * 
		 * @return string
		 */
		public string GetDialogUrl()
		{
			// combine url
			string url = API_HOST + DIALOG_URL;

			// create a url parameter
			NameValueCollection parameters = new NameValueCollection();

			// add the app id
			parameters.Add("app_id", this.appId);

			// set the query parameters
			url += Utility.ToQueryString(parameters);

			return url;
		}

		/**
		 * Sends access token request.
		 * 
		 * @param  string
		 * @return HttpResponse
		 */
		public HttpResponse GetAccessToken(string code)
		{
			// create the post data
			dynamic data = new
			{
				app_id = this.appId,
				app_secret = this.appSecret,
				code = code
			};

			// send the request
			HttpResponse results = this.request
			// set url
			.SetUrl(this.GetAccessUrl())
			// set data
            .SetData(data)
            // send post request
            .SendPost();
            
			return results;
		}

		/**
		 * Set app id.
		 *
		 * @param  string
		 * @return this
		 */
		public Authentication SetAppId(string appId)
		{
			// set app id
			this.appId = appId;

			return this;
		}

		/**
		 * Set app secret.
		 * 
		 * @param  string
		 * @return this
		 */
		public Authentication SetAppSecret(string appSecret)
		{
			// set app secret
			this.appSecret = appSecret;

			return this;
		}
	}
}
