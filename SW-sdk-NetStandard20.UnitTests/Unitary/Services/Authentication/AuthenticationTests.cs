using System;
using NUnit.Framework;
using SW.NetStandard20.Config;
using SW.NetStandard20.Services.Parameters;

namespace SW_sdk_NetStandard20.UnitTests.Unitary.Services.Authentication
{
    [TestFixture]
    public class AuthenticationTests
    {
        private SW.NetStandard20.Services.Authentication.Authentication GetClient(TokenServiceParameters parameters, GlobalConfiguration configuration = null)
        {
            return configuration != null? new SW.NetStandard20.Services.Authentication.Authentication(parameters, configuration) : 
                new SW.NetStandard20.Services.Authentication.Authentication(parameters);
        }

        [Test]
        [Category("Unitary")]
        [Category("Authentication")]
        public void Constructor_TokenParametersNull_MustThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var _ = new SW.NetStandard20.Services.Authentication.Authentication((TokenServiceParameters) null);
            });
        } 
        
        [Test]
        [Category("Unitary")]
        [Category("Authentication")]
        public void Constructor_UserParametersNull_MustThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var _ = new SW.NetStandard20.Services.Authentication.Authentication((UserCredentialsParameters)null);
            });
        } 

        [Test]
        [Category("Unitary")]
        [Category("Authentication")]
        public void GetToken_InfiniteTokenInConstructor_ExpectedResult()
        {
            var token = new TokenServiceParameters {Token = "Expected"};
            var client = GetClient(token);

            var result = client.GetToken();

            Assert.AreEqual(token.Token, result.Data.Token);
        }

        
    }
}
