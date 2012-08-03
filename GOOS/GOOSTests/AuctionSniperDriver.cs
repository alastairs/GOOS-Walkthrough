using System;
using NUnit.Framework;
using White.Core;
using White.Core.UIItems;
using White.Core.UIItems.WindowItems;

namespace GOOSTests
{
    internal class AuctionSniperDriver : IDisposable
    {
        private const string ProcessName = "GOOS.vshost";

        private readonly Window window;
        private readonly Application application;

        public AuctionSniperDriver(int timeoutMillis)
        {
            application = Application.Attach(ProcessName);
            
            window = application.GetWindow("Form1");

            Assert.That(window.DisplayState, Is.EqualTo(DisplayState.Maximized));
        }

        public void ShowsSniperStatus(string status)
        {
            var label = window.Get<Label>("SniperStatusName");
            
            Assert.AreEqual(status, label.Text);
        }

        public void Dispose()
        {
            application.Kill();
        }
    }
}