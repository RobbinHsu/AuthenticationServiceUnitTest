using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using RsaSecureToken;
using Assert = NUnit.Framework.Assert;

namespace RsaSecureToken.Tests
{
    [TestFixture]
    public class AuthenticationServiceTests
    {
        [Test()]
        public void IsValidTest()
        {
            var target = new AuthenticationService();

            var actual = target.IsValid("robbin", "eee333");

            //always failed
            Assert.IsTrue(actual);
        }

        //[Test()]
        public void IsValidTest_如何驗證當非法登入時有正確紀錄log()
        {
            //Assert.Fail();
        }
    }
}