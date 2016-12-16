//
// Subscriber.cs
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
	public class Subscriber : Context
	{
		/* Subscriber url */
		const string SUBSCRIBER_URL = "https://devapi.globelabs.com.ph/location/v1/queries/balance";

		/* Subscriber reload amount url */
		const string SUBSCRIBER_RA_URL = "https://devapi.globelabs.com.ph/location/v1/queries/reload_amount";

		/* API Access token */
		protected string accessToken = null;

		/* Subscribers address */
		protected string address = null;

		/**
		 * Create subscriber class without parameter.
		 */
		public Subscriber() : base()
		{
		}

		/**
		 * Create subscriber class with access token parameter.
		 * 
		 * @param string
		 */
		public Subscriber(string accessToken) : base()
		{
			// set access token
			this.accessToken = accessToken;
		}

		/**
		 * Set access token.
		 * 
		 * @param  string
		 * @return this
		 */
		public Subscriber SetAccessToken(string accessToken)
		{
			// set access token
			this.accessToken = accessToken;

			return this;
		}

		/**
		 * Set address.
		 * 
		 * @param  string
		 * @return this
		 */
		public Subscriber SetAddress(string address)
		{
			// set address
			this.address = address;

			return this;
		}

		/**
		 * Get subscriber balance request.
		 * 
		 * @param  string
		 * @return HttpResponse
		 */
		public HttpResponse GetSubscriberBalance(string address)
		{
			// set parameters
			NameValueCollection parameters = new NameValueCollection();

			// set access token
			parameters.Add("access_token", this.accessToken);
			// set app id
			parameters.Add("address", this.address);

			// set url
			string url = SUBSCRIBER_URL + Utility.ToQueryString(parameters);

			// send request
			HttpResponse results = this.request
	        // set url 
	        .SetUrl(url)
	        // send get
	        .SendGet();

			return results;
		}

		/**
		 * Get subscriber balance request.
		 * 
		 * @return HttpResponse
		 */
		public HttpResponse GetSubscriberBalance()
		{
			// call get subscriber balance
			return this.GetSubscriberBalance(this.address);
		}

		/**
		 * Get subscriber reload amount.
		 * 
		 * @param  string
		 * @return HttpResponse
		 */
		public HttpResponse GetSubscriberReloadAmount(string address)
		{
			// set parameters
			NameValueCollection parameters = new NameValueCollection();

			// set access token
			parameters.Add("access_token", this.accessToken);
			// set app id
			parameters.Add("address", this.address);

			// set url
			string url = SUBSCRIBER_RA_URL + Utility.ToQueryString(parameters);

			// send request
			HttpResponse results = this.request
	        // set url 
	        .SetUrl(url)
	        // send get
	        .SendGet();

			return results;
		}

		/**
		 * Get subscriber reload amount request.
		 * 
		 * @return HttpResponse
		 */
		public HttpResponse GetSubscriberReloadAmount()
		{
			// call get subscriber reload amount
			return this.GetSubscriberReloadAmount(this.address);
		}
	}
}
