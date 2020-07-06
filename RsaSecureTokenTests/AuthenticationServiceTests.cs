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
            var fakeProfileDao = new FakeProfileDao();
            var fakeRsaToken = new FakeRsaToken();
            var target = new AuthenticationService(fakeProfileDao, fakeRsaToken);

            var actual = target.IsValid("robbin", "eee333000000");

            Assert.IsTrue(actual);
        }

        //[Test()]
        public void IsValidTest_如何驗證當非法登入時有正確紀錄log()
        {
            //Assert.Fail();
        }
    }

    public class FakeRsaToken : ITokenDao
    {
        public string GetRandom(string account)
        {
            return "000000";
        }
    }

    public class FakeProfileDao : IProfileDao
    {
        public string GetPassword(string account)
        {
            if (account == "robbin")
            {
                return "eee333";
            }

            return string.Empty;
        }
    }
}