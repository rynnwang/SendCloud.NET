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
        /// <param name="toList">To list.</param>
        /// <param name="ccList">The cc list.</param>
        /// <param name="bccList">The BCC list.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="html">The HTML.</param>
        /// <param name="replyTo">The reply to.</param>
        /// <param name="headers">The headers.</param>
        /// <param name="files">The files.</param>
        /// <param name="smtpApi">The SMTP API.</param>
        /// <param name="useMailList">if set to <c>true</c> [use mail list].</param>
        /// <param name="label">The label.</param>
        /// <param name="gzip">if set to <c>true</c> [gzip].</param>
        /// <returns>List&lt;System.String&gt;.</returns>
        public List<string> SendMail(MailAddress from, MailAddress[] toList, MailAddress[] ccList, MailAddress[] bccList, string subject, string html, string replyTo, Dictionary<string, string> headers, string files, string smtpApi, bool useMailList = false, int? label = null, bool gzip = false)
        {
            const string module = "mail";
            const string action = "send";
            const string responsePropertyName = "email_id_list";

            try
            {
                var parameters = new Dictionary<string, string>();
                #region Parameter prepare

                parameters.Add("from", from.Address);
                parameters.Add("to", ToMailAddress(toList));
                parameters.Add("subject", subject);
                parameters.Add("html", html);
                parameters.Add("bcc", ToMailAddress(bccList));
                parameters.Add("cc", ToMailAddress(ccList));

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
                parameters.Add("use_maillist", useMailList.ToString().ToLowerInvariant());
                parameters.Add("gzip_compress", gzip.ToString().ToLowerInvariant());

                #endregion

                var response = Invoke(module, action, HttpConstants.HttpMethod.Post, parameters, responsePropertyName);
                return response.Value<List<string>>();
            }
            catch (Exception ex)
            {
                throw ex.Handle("SendMail", new
                {
                    from = from.Address,
                    to = ToMailAddress(toList)
                });
            }
        }

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
        /// <param name="gzip">if set to <c>true</c> [gzip].</param>
        /// <returns>List&lt;System.String&gt;.</returns>
        public List<string> SendMail(MailAddress from, MailAddress to, string templateName, Dictionary<string, string> replacements, string replyTo, Dictionary<string, string> headers, string files, string smtpApi, int? label = null, bool gzip = false)
        {
            try
            {
                var substitution = new TemplateEmailSubstitution(replacements.Keys.ToList());
                substitution.To.Add(to.Address);
                substitution.Add(to.Address, replacements);

                return SendMail(from, templateName, substitution, replyTo, headers, files, smtpApi, label, gzip);
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

        public List<string> SendMail(MailAddress from, string templateName, TemplateEmailSubstitution substitution, string replyTo, Dictionary<string, string> headers, string files,
            string smtpApi, int? label = null, bool gzip = false)
        {
            const string module = "mail";
            const string action = "send_template";
            const string responsePropertyName = "email_id_list";

            try
            {
                var parameters = new Dictionary<string, string>();
                #region Parameter prepare

                parameters.Add("from", from.Address);
                parameters.Add("template_invoke_name", templateName);
                parameters.Add("substitution_vars", substitution.ToJson());

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
                parameters.Add("gzip_compress", gzip.ToString().ToLowerInvariant());

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
