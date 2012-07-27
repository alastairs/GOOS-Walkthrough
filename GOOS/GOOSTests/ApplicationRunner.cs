using System;
using System.Threading;
using GOOS;

namespace GOOSTests
{
    internal class ApplicationRunner
    {
        private const string XmppHostname = "hostname";
        private const string SniperId = "sniper";
        private const string SniperPassword = "sniper";
        private const string StatusJoining = "Joining";
        private const string StatusLost = "Lost";
        private AuctionSniperDriver driver;

        public void StartBiddingIn(FakeAuctionServer auction)
        {
            ThreadStart threadStart = () => Run(auction);
            Thread thread = new Thread(threadStart);
            thread.IsBackground = true;
            thread.Start();
            driver = new AuctionSniperDriver(1000);
            driver.ShowsSniperStatus(StatusJoining);
        }

        private void Run(FakeAuctionServer auction)
        {
            try
            {
                Program.Main(XmppHostname, SniperId, SniperPassword, auction.GetItemId());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void ShowsSniperHasLostAuction()
        {
            driver.ShowsSniperStatus(StatusLost);
        }

        public void Stop()
        {
            if (driver != null)
            {
                driver.Dispose();
            }
        }
    }

    internal class AuctionSniperDriver
    {
        public AuctionSniperDriver(int timeoutMillis)
        {
            throw new NotImplementedException();
        }

        public void ShowsSniperStatus(string status)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}