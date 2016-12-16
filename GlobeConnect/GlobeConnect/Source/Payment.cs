//
// Payment.cs
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
	public class Payment : Context
	{
		/* Payment API Url */
		const string PAYMENT_URL = "https://devapi.globelabs.com.ph/payment/v1/transactions/amount";

		/* Payment last reference url */
		const string LAST_REF_URL = "https://devapi.globelabs.com.ph/payment/v1/transactions/getLastRefCode";

		/* API app id */
		protected string appId     = null;

		/* API app secret */
		protected string appSecret = null;

		/* API Access token */
		protected string accessToken = null;

		/* Payment amount */
		protected double amount = 0.00;

		/* Payment description */
		protected string description = null;

		/* User id / Subscribers number */
		protected string endUserId = null;

		/* Custom reference code */
		protected string referenceCode = null;

		/* Resource state */
		protected string transactionOperationStatus = null;

		/**
		 * Create payment class without parameters.
		 */
		public Payment() : base()
		{
		}

		/**
		 * Create payment class with access token parameters.
		 * 
		 * @param string
		 */
		public Payment(string accessToken) : base()
		{
			// set access token
			this.accessToken = accessToken;
		}

		/**
		 * Create payment class with app id
		 * and app secret parameters.
		 * 
		 * @param  string
		 * @param  string
		 */
		public Payment(string appId, string appSecret) : base()
		{
			// set app id
			this.appId = appId;
			// set app secret
			this.appSecret = appSecret;
		}

		/**
		 * Create payment class with app id,
		 * app secret and access token parameters.
		 * 
		 * @param  string
		 * @param  string
		 * @param  string
		 */
		public Payment(string appId, string appSecret, string accessToken) : base()
		{
			// set app id
			this.appId = appId;
			// set app secret
			this.appSecret = appSecret;
			// set access token
			this.accessToken = accessToken;
		}

		/**
		 * Set API app id.
		 * 
		 * @param  string
		 * @return this
		 */
		public Payment SetAppId(string appId)
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
		public Payment SetAppSecret(string appSecret)
		{
			// set app secret
			this.appSecret = appSecret;

			return this;
		}

		/**
		 * Set access token.
		 * 
		 * @param  string
		 * @return this
		 */
		public Payment SetAccessToken(string accessToken)
		{
			// set access token
			this.accessToken = accessToken;

			return this;
		}

		/**
		 * Set payment amount.
		 * 
		 * @param  double
		 * @return this
		 */
		public Payment SetAmount(double amount)
		{
			// set amount
			this.amount = amount;

			return this;
		}

		/**
		 * Set payment description.
		 * 
		 * @param  string
		 * @return this
		 */
		public Payment SetDescription(string description)
		{
			// set description
			this.description = description;

			return this;
		}

		/**
		 * Set end user id.
		 * 
		 * @param  string
		 * @return this
		 */
		public Payment SetEndUserId(string endUserId)
		{
			// set end user id
			this.endUserId = endUserId;

			return this;
		}

		/**
		 * Set reference code.
		 * 
		 * @param  string
		 * @return this
		 */
		public Payment SetReferenceCode(string referenceCode)
		{
			// set reference code
			this.referenceCode = referenceCode;

			return this;
		}

		/**
		 * Set transaction operation status.
		 * 
		 * @param  string
		 * @return this
		 */
		public Payment SetTransactionOperationStatus(string transactionOperationStatus)
		{
			// set transaction operation status
			this.transactionOperationStatus = transactionOperationStatus;

			return this;
		}

		/**
		 * Send payment request.
		 * 
		 * @param  double
		 * @param  string
		 * @param  string
		 * @param  string
		 * @param  string
		 * @return HttpResponse
		 */
		public HttpResponse SendPaymentRequest(
			double amount,
			string description,
			string endUserId,
			string referenceCode,
			string transactionOperationStatus)
		{
			// set parameters
			NameValueCollection parameters = new NameValueCollection();

			// set access token
			parameters.Add("access_token", this.accessToken);

			// build url
			string url = PAYMENT_URL + Utility.ToQueryString(parameters);

			// set base data
			dynamic data = new
			{
				amount = String.Format("{0:0.00}", amount),
				description = description,
				endUserId = endUserId,
				referenceCode = referenceCode,
				transactionOperationStatus = transactionOperationStatus
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
		 * Send payment request.
		 * 
		 * @return HttpResponse
		 */
		public HttpResponse SendPaymentRequest()
		{
			// call send payment request
			return this.SendPaymentRequest(
				this.amount,
				this.description,
				this.endUserId,
				this.referenceCode,
				this.transactionOperationStatus);
		}

		/**
		 * Get last reference code request.
		 * 
		 * @return HttpResponse
		 */
		public HttpResponse GetLastReferenceCode()
		{
			// set parameters
			NameValueCollection parameters = new NameValueCollection();

			// set app id
			parameters.Add("app_id", this.appId);
			// set app secret
			parameters.Add("app_secret", this.appSecret);

			// build url
			string url = LAST_REF_URL + Utility.ToQueryString(parameters);
		
			// send request
			HttpResponse results = this.request
	        // set url 
	        .SetUrl(url)
	        // send get
	        .SendGet();

			return results;
		}
	}
}
