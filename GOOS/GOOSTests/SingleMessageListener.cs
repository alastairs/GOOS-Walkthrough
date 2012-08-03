using System;
using System.Collections.Concurrent;
using System.Threading;
using NUnit.Framework;
using agsXMPP.protocol.client;

namespace GOOSTests
{
    class SingleMessageListener
    {
        private readonly ConcurrentQueue<Message> messages = new ConcurrentQueue<Message>();
        private readonly ManualResetEvent messageEvent = new ManualResetEvent(false);

        public void OnMessage(object sender, Message msg)
        {
            if (msg.Type == MessageType.chat)
            {
                messages.Enqueue(msg);
                messageEvent.Set();
                //currentChat = msg.CreateNewThread(); 
            }
        }

        public void ReceivesAMessage()
        {
            messageEvent.WaitOne(TimeSpan.FromSeconds(5));
            messageEvent.Reset();
            Assert.AreNotEqual(0, messages.Count);
        }
    }
}