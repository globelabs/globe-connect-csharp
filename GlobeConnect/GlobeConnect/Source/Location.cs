//
// Location.cs
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
	public class Location : Context
	{
		/* Location API Url */
		const string LOCATION_URL = "https://devapi.globelabs.com.ph/location/v1/queries/location";

		/* API Access token */
		protected string accessToken = null;

		/* Subscribers address */
		protected string address = null;

		/* Prefered accuracy of the result */
		protected int requestedAccuracy = 0;

		/**
		 * Create location class without parameters.
		 */
		public Location() : base()
		{
		}

		/**
		 * Create location class with access token parameter.
		 * 
		 * @param string
		 */
		public Location(string accessToken) : base()
		{
			// set access token
			this.accessToken = accessToken;
		}

		/**
		 * Set subscriber address.
		 * 
		 * @param  string
		 * @return this
		 */
		public Location SetAddress(string address)
		{
			// set subscriber address
			this.address = address;

			return this;
		}

		/**
		 * Set requested accuracy.
		 * 
		 * @param  int
		 * @return this
		 */
		public Location SetRequestedAccuracy(int requestedAccuracy)
		{
			// set requested accuracy
			this.requestedAccuracy = requestedAccuracy;

			return this;
		}

		/**
		 * Get location request.
		 * 
		 * @param  string
		 * @param  int
		 * @return HttpResponse
		 */
		public HttpResponse GetLocation(string address, int requestedAccuracy)
		{
			// set parameter
			NameValueCollection parameters = new NameValueCollection();

			// set access token
			parameters.Add("access_token", this.accessToken);
			// set address
			parameters.Add("address", address);
			// set requested accuracy
			parameters.Add("requestedAccuracy", requestedAccuracy.ToString());

			// build the url
			string url = LOCATION_URL + Utility.ToQueryString(parameters);

			// send request
			HttpResponse results = this.request
            // set url 
	        .SetUrl(url)
			// send get
            .SendGet();

			return results;
		}

		/**
		 * Get location request.
		 * 
		 * @return HttpResponse
		 */
		public HttpResponse GetLocation()
		{
			// call get location
			return this.GetLocation(this.address, this.requestedAccuracy);
		}
	}
}
