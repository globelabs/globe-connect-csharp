//
// BinarySms.cs
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
	public class BinarySms : Sms
	{
		/* Sms-Binary API url */
		const string SMS_BIN_URL = "https://devapi.globelabs.com.ph/binarymessaging/v1/outbound/{0}/requests";

		/* Message UDH */
		protected string userDataHeader = null;

		/* Data coding of the message */
		protected int dataCodingScheme = 0;

		/* Binary message */
		protected string binaryMessage = null;

		/**
		 * Create binary sms class without parameters.
		 */
		public BinarySms() : base()
		{
		}

		/**
	     * Create binary sms class with sender address parameter.
	     * 
	     * @param string
	     */
		public BinarySms(String senderAddress) : base(senderAddress) 
		{
		}

		/**
	     * Create binary sms class with sender 
	     * address and access token parameter.
	     * 
	     * @param string
	     * @param string
	     */
		public BinarySms(String senderAddress, String accessToken) : base(senderAddress, accessToken) 
		{
		}

		/**
		 * Set sender address.
		 * 
		 * @param  string
		 * @return this
		 */
		new public BinarySms SetSenderAddress(string senderAddress)
		{
			// call base
			base.SetSenderAddress(senderAddress);

			return this;
		}

		/**
		 * Set access token.
		 * 
		 * @param  string
		 * @return this
		 */
		new public BinarySms SetAccessToken(string accessToken)
		{
			// call base
			base.SetAccessToken(accessToken);

			return this;
		}

		/**
		 * Set receiver address.
		 * 
		 * @param  string
		 * @return this
		 */
		new public BinarySms SetReceiverAddress(string receiverAddress)
		{
			// call base
			base.SetReceiverAddress(receiverAddress);

			return this;
		}

		/**
		 * Set user data header.
		 * 
		 * @param  string
		 * @return this
		 */
		public BinarySms SetUserDataHeader(string userDataHeader)
		{
			// set user data header
			this.userDataHeader = userDataHeader;

			return this;
		}

		/**
		 * Set data coding scheme.
		 * 
		 * @param  int
		 * @return this
		 */
		public BinarySms SetDataCodingScheme(int dataCodingScheme)
		{
			// set data coding scheme
			this.dataCodingScheme = dataCodingScheme;

			return this;
		}

		/**
		 * Set binary message.
		 * 
		 * @param  string
		 * @return this
		 */
		public BinarySms SetBinaryMessage(string binaryMessage)
		{
			// set binary message
			this.binaryMessage = binaryMessage;

			return this;
		}

		/**
		 * Send binary message request.
		 * 
		 * @param  string
		 * @param  int
		 * @param  string
		 * @param  string
		 * @return HttpResponse
		 */
		public HttpResponse SendBinaryMessage(
			string userDataHeader,
			int dataCodingScheme,
			string receiverAddress,
			string binaryMessage) {

			// create parameters
			NameValueCollection parameters = new NameValueCollection();

			// add access token
			parameters.Add("access_token", this.accessToken);

			// build the url
			string url = String.Format(SMS_BIN_URL, this.senderAddress) + Utility.ToQueryString(parameters);

			// set outbound binary message request
			dynamic obmr = new 
			{
				senderAddress = this.senderAddress,
				outboundBinaryMessage = new 
				{
					message = binaryMessage
				},
				userDataHeader = userDataHeader,
				dataCodingScheme = dataCodingScheme.ToString(),
				address = receiverAddress,
			};

			// set base data
			dynamic data = new
			{
				outboundBinaryMessageRequest = obmr
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
		 * Send binary message request.
		 * 
		 * @return HttpResponse
		 */
		public HttpResponse SendBinaryMessage()
		{
			// call send binary message
			return this.SendBinaryMessage(
				this.userDataHeader, 
				this.dataCodingScheme, 
				this.receiverAddress, 
				this.binaryMessage);
		}
	}
}
