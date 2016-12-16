//
// Ussd.cs
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
	public class Ussd : Context
	{
		/* USSD Send url */
		const string USSD_SEND_NI_URL = "https://devapi.globelabs.com.ph/ussd/v1/outbound/{0}/send/requests";

		/* USSD Reply url */
		const string USSD_REPLY_MT_URL = "https://devapi.globelabs.com.ph/ussd/v1/outbound/{0}/reply/requests";

		/* Sender address (Short Code) */
		protected string senderAddress = null;

		/* API Access token */
		protected string accessToken = null;

		/* USSD Message */
		protected string ussdMessage = null;

		/* Subscriber address */
		protected string address = null;

		/* Final message flag */
		protected bool flash = false;

		/* USSD Session id */
		protected string sessionId = null;

		/**
	     * Create Ussd class without parameters.
	     */
		public Ussd() : base() 
		{
		}

		/**
	     * Create Ussd class with accessToken parameter.
	     * 
	     * @param string
	     */
		public Ussd(string accessToken) : base()
		{
			// set access token
			this.accessToken = accessToken;
		}

		/**
	     * Create Ussd class with senderAddress and
	     * accessToken parameter.
	     * 
	     * @param string
	     * @param string
	     */
		public Ussd(string senderAddress, string accessToken) : base()
		{
			// set sender address
			this.senderAddress = senderAddress;
			// set access token
			this.accessToken = accessToken;
		}

		/**
	     * Sets sender address (Short Code)
	     * 
	     * @param  string
	     * @return this 
	     */
		public Ussd SetSenderAddress(string senderAddress) 
		{
			// set sender address
			this.senderAddress = senderAddress;

			return this;
		}

		/**
	     * Sets access token.
	     * 
	     * @param  string
	     * @return this
	     */
		public Ussd SetAccessToken(string accessToken) 
		{
			// set access token
			this.accessToken = accessToken;

			return this;
		}

		/**
	     * Set ussd message.
	     * 
	     * @param  string
	     * @return this
	     */
		public Ussd SetUssdMessage(string ussdMessage) 
		{
			// set ussd message
			this.ussdMessage = ussdMessage;

			return this;
		}

		/**
	     * Set subscriber address.
	     * 
	     * @param  string
	     * @return this
	     */
		public Ussd SetAddress(string address) 
		{
			// set subscriber address
			this.address = address;

			return this;
		}

		/**
	     * Set flash.
	     * 
	     * @param  bool
	     * @return this
	     */
		public Ussd SetFlash(bool flash) 
		{
			// set flash
			this.flash = flash;

			return this;
		}

		/**
	     * Set session id.
	     * 
	     * @param  string
	     * @return this
	     */
		public Ussd SetSessionId(string sessionId) 
		{
			// set session id
			this.sessionId = sessionId;

			return this;
		}

		/**
	     * Send USSD Request.
	     * 
	     * @param  string
	     * @param  string
	     * @param  string
	     * @param  bool
	     * @return HttpResponse
	     */
		public HttpResponse SendUssdRequest(
			string senderAddress,
			string ussdMessage,
			string address,
			bool flash)
		{
			// set parameters
			NameValueCollection parameters = new NameValueCollection();

			// add access token
			parameters.Add("access_token", this.accessToken);

			// build url
			string url = String.Format(USSD_SEND_NI_URL, senderAddress) + Utility.ToQueryString(parameters);

			// set outbound ussd message request
			dynamic oumr = new 
			{
				senderAddress = senderAddress,
				address = address,
				flash = flash,
				outboundUSSDMessage = new
				{
					message = ussdMessage
				}
			};

			// set base data
			dynamic data = new
			{
				outboundUSSDMessageRequest = oumr
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
	     * Send USSD Request.
	     * 
	     * @return HttpResponse
	     */
		public HttpResponse SendUssdRequest() 
		{

			// call send ussd request
			return this.SendUssdRequest(
				this.senderAddress, 
				this.ussdMessage, 
				this.address, 
				this.flash);
		}

		/**
	     * Reply USSD Request.
	     * 
	     * @param  string
	     * @param  string
	     * @param  string
	     * @param  bool
	     * @return HttpResponse
	     */
		public HttpResponse ReplyUssdRequest(
			string sessionId,
			string senderAddress,
			string address,
			bool flash)
		{
			// set parameters
			NameValueCollection parameters = new NameValueCollection();

			// add access token
			parameters.Add("access_token", this.accessToken);

			// build url
			string url = String.Format(USSD_REPLY_MT_URL, senderAddress) + Utility.ToQueryString(parameters);

			// set outbound ussd message request
			dynamic oumr = new 
			{
				senderAddress = senderAddress,
				address = address,
				sessionID = sessionId,
				flash = flash,
				outboundUSSDMessage = new
				{
					message = ussdMessage
				}
			};

			// set base data
			dynamic data = new
			{
				outboundUSSDMessageRequest = oumr
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
	     * Reply USSD Request.
	     * 
	     * @return HttpResponse
	     */
		public HttpResponse ReplyUssdRequest() {

			// call reply ussd request
			return this.ReplyUssdRequest(
				this.sessionId, 
				this.senderAddress, 
				this.address, 
				this.flash);
		}
	}
}