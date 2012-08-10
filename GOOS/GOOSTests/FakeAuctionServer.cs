using System;
using System.Threading;
using agsXMPP;
using agsXMPP.protocol.client;

namespace GOOSTests
{
    internal class FakeAuctionServer
    {
        public const string ItemIdAsLogin = "auction-{0}";
        public const string AuctionResource = "Auction";
        public const string XmppHostname = "localhost";
        public const string AuctionPassword = "auction";

        private readonly string itemId;
        private readonly XmppClientConnection connection;
        private string currentChat;
        private readonly SingleMessageListener messageListener = new SingleMessageListener();

        private readonly ManualResetEvent loginEvent = new ManualResetEvent(false);

        public FakeAuctionServer(string item)
        {
            this.itemId = item;
            connection = new XmppClientConnection(XmppHostname);
        }

        public void StartSellingItem()
        {
            connection.OnLogin += ConnectionOnOnLogin;
            connection.Open(string.Format(ItemIdAsLogin, itemId), AuctionPassword, AuctionResource);
            connection.OnMessage += messageListener.OnMessage;
            loginEvent.WaitOne(TimeSpan.FromSeconds(5));
        }

        private void ConnectionOnOnLogin(object sender)
        {
            loginEvent.Set();
        }

        public void HasReceivedJoinRequestFromSniper()
        {
            messageListener.ReceivesAMessage();
        }

        public void AnnounceClosed()
        {
            //currentChat.sendMessage(new Message());
            connection.Send(new Message());
        }

        public void Stop()
        {
            connection.Close();
        }

        public string GetItemId()
        {
            return itemId;
        }
    }
}