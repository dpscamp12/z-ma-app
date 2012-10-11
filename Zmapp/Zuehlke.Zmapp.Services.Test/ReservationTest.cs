using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Zuehlke.Zmapp.Services.Test
{
    [TestClass]
    public class ReservationTest
    {       
        [TestMethod]
        public void Contains1Test()
        {
            var now = DateTime.Now;
            var r = new Reservation {Start = now, End = now + TimeSpan.FromDays(1)};

            Assert.IsTrue(r.Contains(r.Start));
            Assert.IsTrue(r.Contains(r.Start + TimeSpan.FromHours(1)));
            Assert.IsTrue(r.Contains(r.End - TimeSpan.FromHours(1)));
            Assert.IsTrue(r.Contains(r.End));

            Assert.IsFalse(r.Contains(r.Start - TimeSpan.FromDays(1)));
            Assert.IsFalse(r.Contains(r.End + TimeSpan.FromDays(1)));
            
            Assert.IsFalse(r.Contains(now - TimeSpan.FromDays(1)));
            Assert.IsTrue(r.Contains(now));
            Assert.IsTrue(r.Contains(now + TimeSpan.FromHours(12)));
            Assert.IsTrue(r.Contains(now + TimeSpan.FromDays(1)));
            Assert.IsFalse(r.Contains(now + TimeSpan.FromDays(2)));
        }

        [TestMethod]
        public void Contains2Test()
        {
            var r = new Reservation
                        {
                            Start = new DateTime(2013, 12, 23, 10, 23, 55),  // 23.Dez.2013  10:23:55
                            End = new DateTime(2014, 1, 3, 9, 42, 0)         //  3.Jan.2014   9:42:00
                        };

            // by definition
            Assert.IsTrue(r.Contains(r.Start));
            Assert.IsTrue(r.Contains(r.End));

            Assert.IsFalse(r.Contains(r.Start - TimeSpan.FromDays(1)));
            Assert.IsFalse(r.Contains(r.End + TimeSpan.FromDays(1)));

            // because it is defined by day, not hours
            Assert.IsTrue(r.Contains(new DateTime(2013, 12, 23)));
            Assert.IsTrue(r.Contains(new DateTime(2013, 12, 23, 0, 0, 0)));
            Assert.IsTrue(r.Contains(new DateTime(2014, 1, 3)));
            Assert.IsTrue(r.Contains(new DateTime(2014, 1, 3, 23, 59, 59, 999)));

            // some arbitrary values 
            Assert.IsFalse(r.Contains(new DateTime(2000, 1, 1)));
            Assert.IsFalse(r.Contains(new DateTime(2013, 12, 22)));
            Assert.IsTrue(r.Contains(new DateTime(2013, 12, 31)));
            Assert.IsTrue(r.Contains(new DateTime(2014, 1, 2)));
            Assert.IsFalse(r.Contains(new DateTime(2014, 1, 4)));
            Assert.IsFalse(r.Contains(new DateTime(2999)));
        }
    }
}
