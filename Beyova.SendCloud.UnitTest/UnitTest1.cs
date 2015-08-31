using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using Beyova.SendCloud.SDK;
using Beyova.SendCloud.SDK.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Beyova.SendCloud.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        private string apiTriggerUser = "appior_noreply";
        private string apiBulkUser = "Appior_Bulk";
        private string apiKey = "ugenXIZo7haFEsGd";

        [TestMethod]
        public void TestTriggerAccount()
        {
            SendCloudClient client = new SendCloudClient(apiTriggerUser, apiKey);
            var result = client.SendMail(new MailAddress("noreply@appior.com", "Appior"), new MailAddress("rynn.wang@outlook.com"), "WelcomeToAppior", new Dictionary<string, string>
            {
                { "email", "Rynn Wang" },
                {"resetUrl","url"}
            });

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void TestBulkAccount()
        {
            SendCloudClient client = new SendCloudClient(apiBulkUser, apiKey);
            var result = client.SendMail(new MailAddress("noreply@appior.com", "Appior"), new MailAddress("rynn.wang@outlook.com"), "AppiorProjectMemberInvitation", new Dictionary<string, string>
            {
                { "email", "Rynn Wang" },
                {"currentUser","ANYONE"},
                {"invitationUrl","invitationUrlLLL"}
            });

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void TestGetTemplate()
        {
            SendCloudClient client = new SendCloudClient(apiTriggerUser, apiKey);
            var result = client.QueryTemplates(new EmailTemplateCriteria()
        {
        });

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void TestAddTemplate()
        {
            SendCloudClient client = new SendCloudClient(apiTriggerUser, apiKey);
            var result = client.AddTemplate(new EmailTemplate
            {

            });

            Assert.IsNotNull(result);
            Assert.IsTrue(result > 0);
        }
    }
}
