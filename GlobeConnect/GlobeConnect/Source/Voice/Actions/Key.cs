//
// Key.cs
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
using System.ComponentModel;
using Globe.Connect.Voice.Enums;
using Newtonsoft.Json.Linq;

namespace Globe.Connect.Voice.Actions
{
	public class Key
	{
		/* Key name */
		private string name = null;

		/* Key value */
		private object value = null;

		/**
		 * Create a key with the given name.
		 * 
		 * @param  string
		 */
		private Key(string name)
		{
			// set key name
			this.name = name;
		}

		/**
	     * Creates an AS key.
	     * 
	     * @param  As
	     * @return this
	     */
		public static Key AS(As az) 
		{
			return CreateKey("as", az);
		}

		/**
	     * Creates an ALLOW_SIGNAL key.
	     * 
	     * @param  bool
	     * @return this
	     */
		public static Key ALLOW_SIGNALS(bool allowSignals) 
		{
			return CreateKey("allowSignals", allowSignals);
		}

		/**
	     * Creates an ATTEMPTS key.
	     * 
	     * @param  int
	     * @return this
	     */
		public static Key ATTEMPTS(int attempts) 
		{
			return CreateKey("attempts", attempts);
		}

		/**
	     * Creates an ARRAY key.
	     * 
	     * @param  object
	     * @return this
	     */
		public static Key ARRAY(params object[] objects) 
		{
			JArray list = new JArray();

			for(int i = 0; i < objects.Length; i ++) {
				list.Add(JToken.FromObject(objects[i]));
			}

			return CreateKey("array", list);
		}

		/**
	     * Creates an ANSWER_ON_MEDIA key.
	     * 
	     * @param  bool
	     * @return this
	     */
		public static Key ANSWER_ON_MEDIA(bool answerOnMedia)
		{
			return CreateKey("answerOnMedia", answerOnMedia);
		} 

		/**
	     * Creates an ASYNC_UPLOAD key.
	     * 
	     * @param  bool
	     * @return this
	     */
		public static Key ASYNC_UPLOAD(bool asyncUpload) 
		{
			return CreateKey("asyncUpload", asyncUpload);
		}

		/**
	     * Creates a BARGEIN key.
	     * 
	     * @param  bool
	     * @return this
	     */
		public static Key BARGEIN(bool bargein) 
		{
			return CreateKey("bargein", bargein);
		}

		/**
	     * Creates a BEEP key.
	     * 
	     * @param  bool
	     * @return this
	     */
		public static Key BEEP(bool beep) 
		{
			return CreateKey("beep", beep);
		}

		/**
	     * Creates a CHANNEL key.
	     * 
	     * @param  Channel
	     * @return this
	     */
		public static Key CHANNEL(Channel channel) 
		{
			return CreateKey("channel", channel);
		}

		/**
	     * Creates a CALLER_ID key.
	     * 
	     * @param  string
	     * @return this
	     */
		public static Key CALLER_ID(string callerId) 
		{
			return CreateKey("callerID", callerId);
		}

		/**
	     * Creates an EMAIL_FORMAT key.
	     * 
	     * @param  string
	     * @return this
	     */
		public static Key EMAIL_FORMAT(string emailFormat) 
		{
			return CreateKey("emailFormat", emailFormat);
		}

		/**
	     * Creates an EVENT key.
	     * 
	     * @param  string
	     * @return this
	     */
		public static Key EVENT(string eventz) 
		{
			return CreateKey("event", eventz);
		}

		/**
	     * Creates a FROM key.
	     * 
	     * @param  string
	     * @return this
	     */
		public static Key FROM(string from) 
		{
			return CreateKey("from", from);
		}

		/**
	     * Creates a FORMAT key.
	     * 
	     * @param  Format
	     * @return this
	     */
		public static Key FORMAT(Format format) 
		{
			return CreateKey("format", format);
		}

		/**
	     * Creates an ID key.
	     * 
	     * @param  value
	     * @return this
	     */
		public static Key ID(string value) 
		{
			return CreateKey("id", value);
		}

		/**
	     * Creates an INSTANCE key.
	     * 
	     * @param  Base
	     * @return this
	     */
		public static Key INSTANCE(Base baze) 
		{
			// get instance name
			string instance = baze.GetType().Name;

			// camel case
			instance = instance.Substring(0, 1).ToLower() + instance.Substring(1);

			if(baze["fromArray"] != null) {
				return CreateKey(instance, baze.GetValue(baze.GetValue("fromArray").ToString()));
			}

			return CreateKey(instance, baze);
		}

		/**
	     * Creates a INTER_DIGIT_TIMEOUT key.
	     * 
	     * @param  int
	     * @return this
	     */
		public static Key INTER_DIGIT_TIMEOUT(int timeout) 
		{
			return CreateKey("interdigitTimeout", timeout);
		}

		/**
	     * Creates a INTRODUCTION key.
	     * 
	     * @param  introduction
	     * @return this
	     */
		public static Key INTRODUCTION(string introduction) 
		{
			return CreateKey("introduction", introduction);
		}

		/**
	     * Creates a custom key.
	     * 
	     * @param  string
	     * @param  object
	     * @return this
	     */
		public static Key KEY(string key, object value) 
		{
			return CreateKey(key, value);
		}

		/**
	     * Creates a MAX_SILENCE key.
	     * 
	     * @param  float
	     * @return this
	     */
		public static Key MAX_SILENCE(float maxSilence) 
		{
			return CreateKey("maxSilence", maxSilence);
		}

		/**
	     * Creates a MAX_TIME key.
	     * 
	     * @param  float
	     * @return this
	     */
		public static Key MAX_TIME(float maxTime) 
		{
			return CreateKey("maxTime", maxTime);
		}

		/**
	     * Creates a METHOD key.
	     * 
	     * @param  string
	     * @return this
	     */
		public static Key METHOD(string method) 
		{
			return CreateKey("method", method);
		}

		/**
	     * Creates a MILLISECONDS key.
	     * 
	     * @param  float
	     * @return this
	     */
		public static Key MILLISECONDS(float milliseconds) 
		{
			return CreateKey("milliseconds", milliseconds);
		}

		/**
	     * Creates a MIN_CONFIDENCE key.
	     * 
	     * @param   int
	     * @return  this
	     */
		public static Key MIN_CONFIDENCE(int confidence) 
		{
			return CreateKey("minConfidence", confidence);
		}

		/**
	     * Creates a MODE key.
	     * 
	     * @param  Mode
	     * @return this
	     */
		public static Key MODE(Mode mode) 
		{
			return CreateKey("mode", mode);
		}

		/**
	     * Creates a MUTE key.
	     * 
	     * @param  bool
	     * @return this
	     */
		public static Key MUTE(bool mute) 
		{
			return CreateKey("mute", mute);
		}

		/**
	     * Creates a NEXT key.
	     * 
	     * @param  string
	     * @return this
	     */
		public static Key NEXT(string next) 
		{
			return CreateKey("next", next);
		}

		/**
	     * Creates a NETWORK key.
	     * 
	     * @param  Network
	     * @return this
	     */
		public static Key NETWORK(Network network) 
		{
			return CreateKey("network", network);
		}

		/**
	     * Creates a NAME key.
	     * 
	     * @param  string
	     * @return this
	     */
		public static Key NAME(string name) 
		{
			return CreateKey("name", name);
		}

		/**
	     * Creates a PASSWORD key.
	     * 
	     * @param  password
	     * @return this
	     */
		public static Key PASSWORD(string password) 
		{
			return CreateKey("password", password);
		}

		/**
	     * Creates a PLAY_TONES key.
	     * 
	     * @param  playTones
	     * @return this
	     */
		public static Key PLAY_TONES(bool playTones) 
		{
			return CreateKey("playTones", playTones);
		}

		/**
	     * Creates a RECOGNIZER key.
	     * 
	     * @param  string
	     * @return this
     	 */
		public static Key RECOGNIZER(string recognizer)
		{
			return CreateKey("recognizer", recognizer);
		}

		/**
	     * Creates a RECOGNIZER key.
	     * 
	     * @param  recognizer
	     * @return this
	     */
		public static Key RECOGNIZER(Recognizer recognizer) 
		{
			return CreateKey("recognizer", recognizer);
		}

		/**
	     * Creates a REQUIRED key.
	     * 
	     * @param  required
	     * @return this
	     */
		public static Key REQUIRED(bool required) 
		{
			return CreateKey("required", required);
		}

		/**
	     * Creates a RING_REPEAT key.
	     * 
	     * @param  int
	     * @return this
	     */
		public static Key RING_REPEAT(int ringRepeat) 
		{
			return CreateKey("ringRepeat", ringRepeat);
		}

		/**
	     * Creates a SENSITIVITY key.
	     * 
	     * @param  float
	     * @return this
	     */
		public static Key SENSITIVITY(float sensitivity) 
		{
			return CreateKey("sensitivity", sensitivity);
		}

		/**
	     * Creates a SPEECH_COMPLETE_TIMEOUT key.
	     * 
	     * @param  int
	     * @return this
	     */
		public static Key SPEECH_COMPLETE_TIMEOUT(int timeout) 
		{
			return CreateKey("speechCompleteTimeout", timeout);
		}

		/**
	     * Creates a SPEECH_INCOMPLETE_TIMEOUT key.
	     * 
	     * @param  int
	     * @return this
	     */
		public static Key SPEECH_INCOMPLETE_TIMEOUT(int timeout) 
		{
			return CreateKey("speechIncompleteTimeout", timeout);
		}

		/**
	     * Creates a TIMEOUT key.
	     *
	     * @param  float
	     * @return this
	     */
		public static Key TIMEOUT(float timeout) 
		{
			return CreateKey("timeout", timeout);
		}

		/**
	     * Creates a TERMINATOR key.
	     * 
	     * @param  string
	     * @return this
	     */
		public static Key TERMINATOR(string terminator) 
		{
			return CreateKey("terminator", terminator);
		}

		/**
	     * Creates a TO key.
	     * 
	     * @param  string
	     * @return this
	     */
		public static Key TO(string to) 
		{
			return CreateKey("to", to);
		}

		/**
	     * Creates a TO key.
	     * 
	     * @param  string
	     * @return this
	     */
		public static Key TO(params string[] to) 
		{
			return null;
		}

		/**
	     * Creates TRANSCRIPTION_ID key.
	     * 
	     * @param  string
	     * @return this
	     */
		public static Key TRANSCRIPTION_ID(string transcriptionId) 
		{
			return CreateKey("transcriptionID", transcriptionId);
		}

		/**
	     * Creates TRANSCRIPTION_EMAIL_FORMAT key.
	     * 
	     * @param  string
	     * @return this
	     */
		public static Key TRANSCRIPTION_EMAIL_FORMAT(string transcriptionEmailFormat) 
		{
			return CreateKey("transacriptionEmailFormat", transcriptionEmailFormat);
		}

		/**
	     * Creates TRANSCRIPTION_OUT_URI key.
	     * 
	     * @param  string
	     * @return this
	     */
		public static Key TRANSCRIPTION_OUT_URI(string transcriptionOutUri) 
		{
			return CreateKey("transacriptionOutURI", transcriptionOutUri);
		}

		/**
	     * Creates a URL key.
	     * 
	     * @param  string
	     * @return this
	     */
		public static Key URL(string url) 
		{
			return CreateKey("url", url);
		}

		/**
	     * Creates a USERNAME key.
	     * 
	     * @param  string
	     * @return this
	     */
		public static Key USERNAME(string username) 
		{
			return CreateKey("username", username);
		}

		/**
	     * Creates a VALUE key.
	     * 
	     * @param  string
	     * @return this
	     */
		public static Key VALUE(string value) 
		{
			return CreateKey("value", value);
		}

		/**
	     * Creates a VOICE key.
	     * 
	     * @param  Voice
	     * @return this
	     */
		public static Key VOICE(Globe.Connect.Voice.Enums.Voice voice) 
		{
			return CreateKey("voice", voice);
		}

		/**
	     * Creates a VOICE key.
	     * 
	     * @param  string
	     * @return this
	     */
		public static Key VOICE(string voice) 
		{
			return CreateKey("voice", voice);
		}

		/**
	     * Create a key using the 
	     * given name and value.
	     * 
	     * @param  string
	     * @param  object
	     * @return this
	     */
		private static Key CreateKey(string name, object value) 
		{
			// is it enum?
			if(value.GetType().IsEnum) {
				DescriptionAttribute[] attributes = (
					DescriptionAttribute[]
				) value
					.GetType()
					.GetField(value.ToString())
					.GetCustomAttributes(typeof(DescriptionAttribute), false);
				
				value = attributes.Length > 0 ? attributes[0].Description : value.ToString();
			}

			// create new key
			Key key = new Key(name);

			// set value
			key.value = value;

			return key;
		}

		/**
	     * Returns the key name.
	     * 
	     * @return string 
	     */
		public string GetName() 
		{
			return this.name;
		}

		/**
	     * Returns the key value.
	     * 
	     * @return object 
	     */
		public object GetValue() 
		{
			return this.value;
		}
	}
}
