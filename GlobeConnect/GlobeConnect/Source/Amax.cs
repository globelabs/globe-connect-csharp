//
// Amax.cs
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
	public class Amax : Context
	{
		/* Amax api url */
		const string AMAX_URL = "https://devapi.globelabs.com.ph/rewards/v1/transactions/send";

		/* API app id */
		protected string appId     = null;

		/* API app secret */
		protected string appSecret = null;

		/* Rewards token */
		protected string rewardsToken = null;

		/* Subscriber address */
		protected string address = null;

		/* Defined promo */
		protected string promo = null;

		/**
	     * Create Amax class without paramters.
	     */
		public Amax() : base()
		{
		}

		/**
	     * Create Amax class with appId and
	     * appSecret parameters.
	     * 
	     * @param string
	     * @param string
	     */
		public Amax(string appId, string appSecret) : base()
		{
			// set app id
			this.appId = appId;
			// set app secret
			this.appSecret = appSecret;
		}

		/**
	     * Set API app id.
	     * 
	     * @param  string
	     * @return this
	     */
		public Amax SetAppId(string appId) 
		{
			// set app id
			this.appId = appId;

			return this;
		}

		/**
	     * Set API app secret.
	     * 
	     * @param  string
	     * @return this 
	     */
		public Amax SetAppSecret(string appSecret) 
		{
			// set app secret
			this.appSecret = appSecret;

			return this;
		}

		/**
	     * Set rewards token.
	     * 
	     * @param  string
	     * @return this
	     */
		public Amax SetRewardsToken(string rewardsToken) 
		{
			// set rewards token
			this.rewardsToken = rewardsToken;

			return this;
		}

		/**
	     * Set subscriber address.
	     * 
	     * @param  string
	     * @return this
	     */
		public Amax SetAddress(string address) {
			// set subscriber address
			this.address = address;

			return this;
		}

		/**
	     * Set defined promo.
	     * 
	     * @param  string
	     * @return this
	     */
		public Amax SetPromo(string promo) {
			// set defined promo
			this.promo = promo;

			return this;
		}

		/**
	     * Send rewards request.
	     * 
	     * @param  string
	     * @param  string
	     * @param  string
	     * @return HttpResponse
	     */
		public HttpResponse SendRewardRequest(
			String rewardsToken,
			String address,
			String promo) 
		{
			// set url
			string url = AMAX_URL;

			// set base data
			dynamic data = new
			{
				outboundRewardRequest = new
				{
					app_id = this.appId,
					app_secret = this.appSecret,
	                rewards_token = rewardsToken,
					address = address,
					promo = promo
				}
			};

			// send the request
			HttpResponse results = this.request
	        // set url
            .SetUrl(url)
	        // set data
	        .SetData(data)
	        // send post request
	        .SendPost();

			return results;
		}

		/**
	     * Send rewards request.
	     * 
	     * @return HttpResponse
	     */
		public HttpResponse SendRewardRequest() 
		{
			// call send reward request
			return this.SendRewardRequest(this.rewardsToken, this.address, this.promo);
		}
	}
}
