﻿using System;
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
        private ILog _log;
        private AuthenticationService _target;


        [SetUp]
        public void Setup()
        {
            _fakeProfileDao = Substitute.For<IProfileDao>();
            _fakeRsaToken = Substitute.For<ITokenDao>();
            _log = Substitute.For<ILog>();
            _target = new AuthenticationService(_fakeProfileDao, _fakeRsaToken, _log);
        }

        [Test()]
        public void IsValidTest()
        {
            GivenProfile("robbin", "eee333");
            GivenToken("000000");

            ShouldBeValid("robbin", "eee333000000");
        }

        [Test()]
        public void IsValidTest_如何驗證當非法登入時有正確紀錄log()
        {
            GivenProfile("robbin", "wrong password");
            GivenToken("000000");

            _target.IsValid("robbin", "eee333000000");

            _log.Received(1).Save(Arg.Is<string>(x => x.Contains("robbin") && x.Contains("login failed")));
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