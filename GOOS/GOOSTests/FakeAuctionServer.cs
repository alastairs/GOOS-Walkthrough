using System;
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

        public FakeAuctionServer(string item)
        {
            this.itemId = item;
            connection = new XmppClientConnection(XmppHostname);
        }

        public void StartSellingItem()
        {
            connection.Open(string.Format(ItemIdAsLogin, itemId), AuctionPassword, AuctionResource);
            connection.OnMessage += messageListener.OnMessage;
        }

        public void HasReceivedJoinRequestFromSniper()
        {
            throw new NotImplementedException();
        }

        public void AnnounceClosed()
        {
            throw new NotImplementedException();
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