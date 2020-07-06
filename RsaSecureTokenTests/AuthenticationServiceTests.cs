using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using NUnit.Framework;
using RsaSecureToken;
using Assert = NUnit.Framework.Assert;

namespace RsaSecureToken.Tests
{
    [TestFixture]
    public class AuthenticationServiceTests
    {
        private IProfileDao _fakeProfileDao;
        private ITokenDao _fakeRsaToken;
        private ILog _fakeLog;
        private AuthenticationService _target;


        [SetUp]
        public void Setup()
        {
            _fakeProfileDao = Substitute.For<IProfileDao>();
            _fakeRsaToken = Substitute.For<ITokenDao>();
            _fakeLog = Substitute.For<ILog>();
            _target = new AuthenticationService(_fakeProfileDao, _fakeRsaToken, _fakeLog);
        }

        [Test()]
        public void should_log_when_invalid()
        {
            GivenProfile("robbin", "wrong password");
            GivenToken("000000");

            WhenInvalid();

            ShouldLogWith("robbin", "login failed");
        }

        [Test()]
        public void should_not_log_when_valid()
        {
            GivenProfile("robbin", "eee333");
            GivenToken("000000");
            WhenValid();
            ShouldNotLog();
        }

        private void ShouldNotLog()
        {
            _fakeLog.DidNotReceiveWithAnyArgs().Save(Arg.Any<string>());
        }

        private void WhenValid()
        {
            _target.IsValid("robbin", "eee333000000");
        }

        private void ShouldLogWith(string account, string status)
        {
            _fakeLog.Received(1).Save(Arg.Is<string>(x => x.Contains(account) && x.Contains(status)));
        }

        private void WhenInvalid()
        {
            _target.IsValid("robbin", "eee333000000");
        }

        private void ShouldBeValid(string account, string password)
        {
            Assert.IsTrue(_target.IsValid(account, password));
        }

        private void GivenToken(string token)
        {
            _fakeRsaToken.GetRandom("").ReturnsForAnyArgs(token);
        }

        private void GivenProfile(string account, string password)
        {
            _fakeProfileDao.GetPassword(account).Returns(password);
        }
    }
}