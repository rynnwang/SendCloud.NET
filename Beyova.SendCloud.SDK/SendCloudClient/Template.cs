using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using Beyova.SendCloud.SDK.Model;
using ifunction;
using Newtonsoft.Json.Linq;

namespace Beyova.SendCloud.SDK
{
    /// <summary>
    /// Class SendCloudClient.
    /// </summary>
    partial class SendCloudClient
    {
        #region Template

        /// <summary>
        /// Queries the templates.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>List&lt;EmailTemplate&gt;.</returns>
        public List<EmailTemplate> QueryTemplates(EmailTemplateCriteria criteria)
        {
            const string module = "template";
            const string action = "get";
            const string responsePropertyName = "templateList";

            try
            {
                criteria.CheckNullObject("criteria");

                var parameters = new Dictionary<string, string>();
                if (!string.IsNullOrWhiteSpace(criteria.InvokeName))
                {
                    parameters.Add("invoke_name", criteria.InvokeName);
                }
                if (criteria.Start != null && criteria.Start.Value >= 0)
                {
                    parameters.Add("start", criteria.Start.ToString());
                }
                if (criteria.Count != null && criteria.Count.Value > 0)
                {
                    parameters.Add("limit", criteria.Count.ToString());
                }

                var response = Invoke(module, action, HttpConstants.HttpMethod.Post, parameters, responsePropertyName);
                return response.Value<List<EmailTemplate>>();
            }
            catch (Exception ex)
            {
                throw ex.Handle("QueryTemplates", criteria);
            }
        }

        #endregion

        #region Util


        #endregion
    }
}
