//
// Sms.cs
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
	public class Sms : Context
	{
		/* SMS-MT url */
		const string SMS_MT_URL = "https://devapi.globelabs.com.ph/smsmessaging/v1/outbound/{0}/requests";

		/* Sender address */
		protected string senderAddress = null;
		
		/* API Access token */
	    protected string accessToken = null;
	    
	    /* Client correlator */
	    protected string clientCorrelator = null;
	    
	    /* Receiver address */
	    protected string receiverAddress = null;
	    
	    /* Outbound SMS Text message */
	    protected string message = null;
		
		
		/**
		 * Create sms class without parameters.
		 */
		public Sms() : base()
		{
		}

		/**
	     * Create sms class with sender address parameter.
	     * 
	     * @param string
	     */
		public Sms(String senderAddress) : base() 
		{
			// set sender address
			this.senderAddress = senderAddress;
		}

		/**
		 * Create sms class with sender address
		 * and access token parameter.
		 * 
		 * @param  string
		 * @param  string
		 */
		public Sms(String senderAddress, String accessToken) : base()
		{
			// set sender address
			this.senderAddress = senderAddress;
			// set access token
			this.accessToken = accessToken;
		}

		/**
		 * Set sender address.
		 * 
		 * @param  string
		 * @return this
		 */
		public Sms SetSenderAddress(string senderAddress)
		{
			// set sender address
			this.senderAddress = senderAddress;

			return this;
		}

		/**
		 * Set access token.
		 * 
		 * @param  string
		 * @return this
		 */
		public Sms SetAccessToken(string accessToken)
		{
			// set access token
			this.accessToken = accessToken;

			return this;
		}

		/**
		 * Set client correlator.
		 * 
		 * @param  string
		 * @return this
		 */
		public Sms SetClientCorrelator(string clientCorrelator)
		{
			// set client correlator
			this.clientCorrelator = clientCorrelator;

			return this;
		}

		/**
		 * Set receiver address.
		 * 
		 * @param  string
		 * @return this
		 */
		public Sms SetReceiverAddress(string receiverAddress)
		{
			// set receiver address
			this.receiverAddress = receiverAddress;

			return this;
		}

		/**
		 * Set outbound sms message.
		 * 
		 * @param  string
		 * @return this
		 */
		public Sms SetMessage(string message)
		{
			// set outbound message
			this.message = message;

			return this;
		}

		/**
		 * Returns sender address.
		 * 
		 * @return string
		 */
		public string getSenderAddress()
		{
			return this.senderAddress;
		}

		/**
		 * Returns the current access token.
		 * 
		 * @return string
		 */
		public string getAccessToken()
		{
			return this.accessToken;
		}

		/**
		 * Send an outbound sms message.
		 * 
		 * @param  string
		 * @param  string
		 * @param  string
		 * @return HttpResponse
		 */
		public HttpResponse SendMessage(
			string clientCorrelator,
			string message,
			string receiverAddress)
		{
			// create parameters
			NameValueCollection parameters = new NameValueCollection();

			// add access token
			parameters.Add("access_token", this.accessToken);

			// build url
			string url = String.Format(SMS_MT_URL, this.senderAddress) + Utility.ToQueryString(parameters);

			// create the outbound sms message request
			dynamic osmr = new 
			{
				senderAddress = "tel:" + this.senderAddress,
				outboundSMSTextMessage = new 
				{
					message = message
				},
				address = new string[] {"tel:" + receiverAddress}
			};

			// if client correlator is set
			if(clientCorrelator != null) 
			{
				// set client correlator
				osmr.clientCorrelator = clientCorrelator;
			}

			// create the base data
			dynamic data = new 
			{
				outboundSMSMessageRequest = osmr
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
		 * Sends an outbound sms message.
		 * 
		 * @param  string
		 * @param  string
		 * @return HttpResponse
		 */
		public HttpResponse SendMessage(string message, string receiverAddress)
		{
			// call send messsage
			return this.SendMessage(this.clientCorrelator, message, receiverAddress);
		}

		/**
		 * Sends an outbound sms message.
		 * 
		 * @return HttpResponse
		 */
		public HttpResponse SendMessage()
		{
			// call send message
			return this.SendMessage(this.clientCorrelator, this.message, this.receiverAddress);
		}
	}
}
