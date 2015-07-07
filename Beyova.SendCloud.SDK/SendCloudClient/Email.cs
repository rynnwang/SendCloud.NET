using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using Beyova.SendCloud.SDK.Model;
using ifunction;
using Newtonsoft.Json.Linq;

namespace Beyova.SendCloud.SDK
{
    partial class SendCloudClient
    {
        #region Email

        /// <summary>
        /// Sends the mail.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <param name="templateName">Name of the template.</param>
        /// <param name="replacements">The replacements.</param>
        /// <param name="replyTo">The reply to.</param>
        /// <param name="headers">The headers.</param>
        /// <param name="files">The files.</param>
        /// <param name="smtpApi">The SMTP API.</param>
        /// <param name="label">The label.</param>
        /// <param name="usingGZip">if set to <c>true</c> [using g zip].</param>
        /// <returns>List&lt;System.String&gt;.</returns>
        public List<string> SendMail(MailAddress from, MailAddress to, string templateName, Dictionary<string, string> replacements, string replyTo, Dictionary<string, string> headers, string files, string smtpApi, int? label = null, bool usingGZip = false)
        {
            try
            {
                var substitution = new TemplateEmailSubstitution(replacements.Keys.ToList());
                substitution.To.Add(to.Address);
                substitution.Add(to.Address, replacements);

                return SendMail(from, templateName, substitution, replyTo, headers, files, smtpApi, label, usingGZip);
            }
            catch (Exception ex)
            {
                throw ex.Handle("SendMail", new
                {
                    from = from.Address,
                    to = to != null ? to.Address : null
                });
            }
        }

        /// <summary>
        /// Sends the mail.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="templateName">Name of the template.</param>
        /// <param name="substitution">The substitution.</param>
        /// <param name="replyTo">The reply to.</param>
        /// <param name="headers">The headers.</param>
        /// <param name="files">The files.</param>
        /// <param name="smtpApi">The SMTP API.</param>
        /// <param name="label">The label.</param>
        /// <param name="usingGZip">if set to <c>true</c> [using g zip].</param>
        /// <returns>List&lt;System.String&gt;.</returns>
        public List<string> SendMail(MailAddress from, string templateName, TemplateEmailSubstitution substitution, string replyTo, Dictionary<string, string> headers, string files,
            string smtpApi, int? label = null, bool usingGZip = false)
        {
            const string module = "mail";
            const string action = "send_template";
            const string responsePropertyName = "email_id_list";

            try
            {
                #region Parameter prepare

                var parameters = new Dictionary<string, string>
                {
                    {"from", @from.Address},
                    {"template_invoke_name", templateName},
                    {"substitution_vars", substitution.ToJson()}
                };
                
                if (!string.IsNullOrWhiteSpace(from.DisplayName))
                {
                    parameters.Add("fromname", from.DisplayName);
                }

                if (!string.IsNullOrWhiteSpace(replyTo))
                {
                    parameters.Add("replyto", replyTo);
                }

                if (label != null)
                {
                    parameters.Add("label", label.ToString());
                }

                if (headers != null)
                {
                    parameters.Add("headers", headers.ToJson(false));
                }

                if (!string.IsNullOrWhiteSpace(files))
                {
                    parameters.Add("files", files);
                }

                if (!string.IsNullOrWhiteSpace(smtpApi))
                {
                    parameters.Add("x_smtpapi", smtpApi);
                }

                parameters.Add("resp_email_id", "true");
                parameters.Add("gzip_compress", usingGZip.ToString().ToLowerInvariant());

                #endregion

                var response = Invoke(module, action, HttpConstants.HttpMethod.Post, parameters, responsePropertyName);
                return response.Value<List<string>>();
            }
            catch (Exception ex)
            {
                throw ex.Handle("SendMail", new
                {
                    from = from.Address,
                    substitution = substitution
                });
            }
        }

        #endregion

        #region Util

        /// <summary>
        /// To the mail address.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns>System.String.</returns>
        protected static string ToMailAddress(MailAddress[] address)
        {
            return (address != null && address.Length > 0) ? address.Join(";", x => x.Address) : string.Empty;
        }

        #endregion
    }
}
