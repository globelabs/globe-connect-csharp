//
// Base.cs
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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Globe.Connect.Voice.Actions
{
	public class Base : JObject
	{
		/**
		 * Sets key-value pair based on keys.
		 * 
		 * @param Key
		 */
		public Base(params Key[] keys) 
		{
			// get key length
			int length = keys.Length;

			// iterate on each key
			for(int i = 0; i < length; i ++) {
				// get key name
				string name  = keys[i].GetName();
				// get key value
				object value = keys[i].GetValue();

				// is boolean?
				if(value.GetType() == true.GetType()) {
					// set primitive value
					this.Add(name, value.ToString() == "True" ? true : false);

					continue;
				}

				// add the value as string
				this.Add(name, JToken.FromObject(value));
			}
		}

		/**
		 * Returns this as json object.
		 * 
		 * @return  object
		 */
		public object GetAction() 
		{
			return this.GetAction(null);
		}

		/**
		 * Returns this as json object.
		 * 
		 * @return object
		 */
		public object GetAction(string root) 
		{
			// if root key is not set
			if(root == null) {
				// get instance name
				string instance = this.GetType().Name;

				// camel case
				instance = instance.Substring(0, 1).ToLower() + instance.Substring(1);

				// set root name
				root = instance;
			}

			// get from array?
			if(this["fromArray"] != null) {
				return this.GetValue(this.GetValue("fromArray").ToString());
			}

			// set json object
			JObject json = new JObject();

			// add the root key plus this instance
			json.Add(root, this);

			return json;
		}
	}
}
