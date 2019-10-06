using System;
using NUnit.Framework;
using ComputerComplectorWebAPI.Services;

namespace ComputerComplectorWebAPITests.Services
{
    public class UtilityUnitTests
    {
        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void UtilityUnitTests_EmptyConnectionString_Expected_ArgumentException(string connectionString)
        {
            Assert.Throws(typeof(ArgumentException), () => { new DBUtility(connectionString); });
        }
    }
}
