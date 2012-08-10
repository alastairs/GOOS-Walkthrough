using System;
using System.Threading;
using GOOS;

namespace GOOSTests
{
    internal class ApplicationRunner
    {
        private const string XmppHostname = "localhost";
        private const string SniperId = "sniper";
        private const string SniperPassword = "sniper";
        private const string StatusJoining = "Joining";
        private const string StatusLost = "Lost";
        private AuctionSniperDriver driver;

        private readonly ManualResetEvent applicationLaunchEvent = new ManualResetEvent(false);

        public void StartBiddingIn(FakeAuctionServer auction)
        {
            ThreadStart threadStart = () => Run(auction);
            var thread = new Thread(threadStart) {IsBackground = true};
            thread.Start();

            applicationLaunchEvent.WaitOne(TimeSpan.FromSeconds(1));
            driver = new AuctionSniperDriver(1000); 
            driver.ShowsSniperStatus(StatusJoining);
        }

        private void Run(FakeAuctionServer auction)
        {
            try
            {
                Program.Main(XmppHostname, SniperId, SniperPassword, auction.GetItemId());
                applicationLaunchEvent.Set();
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
}