//
// Say.cs
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
using Newtonsoft.Json.Linq;

namespace Globe.Connect.Voice.Actions
{
	public class Say : Base
	{
		public Say(string value) : base(Key.VALUE(value))
		{
		}

		public Say(params Key[] keys) : base(keys)
		{
			for(int i = 0; i < keys.Length; i ++) {
				if(keys[i].GetName() == "array") {
					this.Add("fromArray", "array");

					string original = null;

					for(int k = 0; k < keys.Length; k ++) {
						if(keys[k].GetName() == "value") {
							original = keys[k].GetValue().ToString();

							JObject current = new JObject();
							JArray array = JArray.Parse(this.GetValue("array").ToString());

							current.Add("value", original);
							array.Add(current);

							this.Remove("array");
							this.Add("array", array);
						}
					}
				}
			}
		}
	}
}
