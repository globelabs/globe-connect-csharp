//
// Voice.cs
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
using Globe.Connect.Voice.Actions;
using Newtonsoft.Json.Linq;

namespace Globe.Connect.Voice
{
	public class Voice
	{
		/* Root json object */
		protected JObject rootObject = new JObject();

		/* Root json array */
		protected JArray rootArray = new JArray();

		/**
         * Add an action base on the given
         * action object.
         * 
         * @param  Base
         * @return this
         */
		public Voice AddAction(Base baze) {
			// add action
			this.AddAction(baze, null);

			return this;
		}

		/**
         * Add an action base on the given
         * action object.
         * 
         * @param  Base
         * @param  string
         * @return this
         */
		public Voice AddAction(Base baze, String root) {
			// push to our commands
			if(root != null) {
				this.rootArray.Add(baze.GetAction(root));

				return this;
			}

			this.rootArray.Add(baze.GetAction());

			return this;
		}

		/**
         * Initialize an "ask" command.
         * 
         * @param  Key[][]
         * @return this
         */
		public Voice Ask(params Key[] keys) {
			// initialize ask
			Ask ask = new Ask(keys);

			// push to our commands
			this.rootArray.Add(ask.GetAction());

			return this;
		}

		/**
         * Initialize a "call" command.
         * 
         * @param  Key[]
         * @return this
         */
		public Voice Call(params Key[] keys) {
			// initialize call
			Call call = new Call(keys);

			// push to our commands
			this.rootArray.Add(call.GetAction());

			return this;
		}

		/**
         * Initialize a "conference" command.
         * 
         * @param  Key[]
         * @return this
         */
		public Voice Conference(params Key[] keys) {
			// initialize conference
			Conference conference = new Conference(keys);

			// push to our commands
			this.rootArray.Add(conference.GetAction());

			return this;
		}

		/**
         * Initialize a "hangup" command.
         * 
         * @return this
         */
		public Voice Hangup() {
			// initialize hangup
			Hangup hangup = new Hangup();

			// push to our commands
			this.rootArray.Add(hangup.GetAction());

			return this;
		}

		/**
         * Initialize a "joinPrompt" command.
         * 
         * @param  Key[]
         * @return this
         */
		public Voice JoinPrompt(params Key[] keys) {
			// initialize join prompt
			JoinPrompt joinPrompt = new JoinPrompt(keys);

			// push to our commands
			this.rootArray.Add(joinPrompt.GetAction());

			return this;
		}

		/**
         * Initialize a "leavePrompt" command.
         * 
         * @param  Key[]
         * @return this
         */
		public Voice LeavePrompt(params Key[] keys) {
			// initialize leave prompt
			LeavePrompt leavePrompt = new LeavePrompt(keys);

			// push to our commands
			this.rootArray.Add(leavePrompt.GetAction());

			return this;
		}

		/**
         * Initialize a "machineDetection" command.
         * 
         * @param  Key[]
         * @return this
         */
		public Voice MachineDetection(params Key[] keys) {
			// initialize machine detection
			MachineDetection machineDetection = new MachineDetection(keys);

			// push to our commands
			this.rootArray.Add(machineDetection.GetAction());

			return this;
		}

		/**
         * Initialize a "message" command.
         * 
         * @param  Key[]
         * @return this
         */
		public Voice Message(params Key[] keys) {
			// initialize message
			Message message = new Message(keys);

			// push to our commands
			this.rootArray.Add(message.GetAction());

			return this;
		}

		/**
         * Initialize an "on" command.
         * 
         * @param  Key[]
         * @return this
         */
		public Voice On(params Key[] keys) {
			// initialize on
			On on = new On(keys);

			// push to our commands
			this.rootArray.Add(on.GetAction());

			return this;
		}

		/**
         * Initialize a "record" command.
         * 
         * @param  Key[]
         * @return this
         */
		public Voice Record(params Key[] keys) {
			// initialize record
			Record record = new Record(keys);

			// push to our commands
			this.rootArray.Add(record.GetAction());

			return this;
		}

		/**
         * Initialize a "redirect" command.
         * 
         * @param  Key[]
         * @return this
         */
		public Voice Redirect(params Key[] keys) {
			// initialize redirect
			Redirect redirect = new Redirect (keys);

			// push to our commands
			this.rootArray.Add(redirect.GetAction());

			return this;
		}

		/**
         * Initialize a "reject" command.
         * 
         * @return this
         */
		public Voice Reject() {
			// initialize reject
			Reject reject = new Reject();

			// push to our commands
			this.rootArray.Add(reject.GetAction());

			return this;
		}

		/**
         * Initialize a "say" command.
         * 
         * @param  Key[]
         * @return this
         */
		public Voice Say(params Key[] keys) {
			// initialize say
			Say say = new Say(keys);

			// push to our commands
			this.rootArray.Add(say.GetAction());

			return this;
		}

		/**
         * Initialize a "say" command.
         * 
         * @param  value
         * @return this
         */
		public Voice Say(String value) {
			// initialize say
			Say say = new Say(value);

			// push to our commands
			this.rootArray.Add(say.GetAction());

			return this;
		}

		/**
         * Initialize a "startRecording" command.
         * 
         * @param  Key[]
         * @return this
         */
		public Voice StartRecording(params Key[] keys) {
			// initialize start recording
			StartRecording startRecording = new StartRecording(keys);

			// push to our commands
			this.rootArray.Add(startRecording.GetAction());

			return this;
		}

		/**
         * Initialize a "stopRecording" command.
         * 
         * @return this
         */
		public Voice StopRecording() {
			// initialize stop recording
			StopRecording stopRecording = new StopRecording();

			// push to our commands
			this.rootArray.Add(stopRecording.GetAction());

			return this;
		}

		/**
         * Initialize a "transfer" command.
         * 
         * @param  Key[]
         * @return this
         */
		public Voice Transfer(params Key[] keys) {
			// initialize transfer
			Transfer transfer = new Transfer(keys);

			// push to our commands
			this.rootArray.Add(transfer.GetAction());

			return this;
		}

		/**
         * Initialize a "wait" command.
         * 
         * @param  Key[]
         * @return this
         */
		public Voice Wait(params Key[] keys) {
			// initialize wait
			Wait wait = new Wait(keys);

			// push to our commands
			this.rootArray.Add(wait.GetAction());

			return this;
		}

		/**
         * Renders the json object including
         * all the commands in the root object.
         * 
         * @return  JObject
         */
		public JObject Render() {
			// set the root tropo
			this.rootObject.Add("tropo", this.rootArray);

			return this.rootObject;
		}
	}
}