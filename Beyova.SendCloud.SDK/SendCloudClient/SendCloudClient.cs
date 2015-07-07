using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using ifunction;
using ifunction.ExceptionSystem;
using Newtonsoft.Json.Linq;

namespace Beyova.SendCloud.SDK
{
    /// <summary>
    /// Class SendCloudClient.
    /// </summary>
    public partial class SendCloudClient
    {
        /// <summary>
        /// The base URL
        /// </summary>
        private const string baseUrl = "http://sendcloud.sohu.com/webapi/";

        #region Property

        /// <summary>
        /// Gets or sets the API user.
        /// </summary>
        /// <value>The API user.</value>
        public string ApiUser { get; protected set; }

        /// <summary>
        /// Gets or sets the secret key.
        /// </summary>
        /// <value>The secret key.</value>
        public string SecretKey { get; protected set; }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="SendCloudClient"/> class.
        /// </summary>
        /// <param name="apiUser">The API user.</param>
        /// <param name="secretKey">The secret key.</param>
        public SendCloudClient(string apiUser, string secretKey)
        {
            SecretKey = secretKey;
            ApiUser = apiUser;
        }

        #region Util

        /// <summary>
        /// Gets the HTTP request.
        /// </summary>
        /// <param name="module">The module.</param>
        /// <param name="action">The action.</param>
        /// <param name="httpMethod">The HTTP method.</param>
        /// <param name="parameters">The query string.</param>
        /// <returns>HttpWebRequest.</returns>
        protected HttpWebRequest GetHttpRequest(string module, string action, string httpMethod, Dictionary<string, string> parameters = null)
        {
            string url = string.Format("{0}/{1}.{2}.json", baseUrl, module, action);
            if (parameters == null)
            {
                parameters = new Dictionary<string, string>();
            }

            parameters.Add("api_user", this.ApiUser);
            parameters.Add("api_key", this.SecretKey);

            if (httpMethod.Equals(HttpConstants.HttpMethod.Get))
            {
                url += ("?" + parameters.ToKeyValueStringWithUrlEncode());
            }

            var httpRequest = url.CreateHttpWebRequest(httpMethod);

            if (httpMethod.Equals(HttpConstants.HttpMethod.Post, StringComparison.InvariantCultureIgnoreCase))
            {
                httpRequest.FillData(httpMethod, parameters);
            }

            return httpRequest;
        }

        /// <summary>
        /// Invokes the specified module.
        /// </summary>
        /// <param name="module">The module.</param>
        /// <param name="action">The action.</param>
        /// <param name="httpMethod">The HTTP method.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="responsePropertyName">Name of the response property.</param>
        /// <returns>JToken.</returns>
        /// <exception cref="ifunction.ExceptionSystem.RemoteServiceOperationFailureException">SendCloud;SendCloud.Sohu.com;null;null</exception>
        protected JToken Invoke(string module, string action, string httpMethod, Dictionary<string, string> parameters, string responsePropertyName)
        {
            var httpRequest = GetHttpRequest(module, action, httpMethod, parameters);
            var response = httpRequest.ReadResponseAsText(Encoding.UTF8);
            var responseObject = JObject.Parse(response);
            if (responseObject.GetValue("message")
                .SafeToString()
                .Equals("success", StringComparison.InvariantCultureIgnoreCase))
            {
                return !string.IsNullOrWhiteSpace(responsePropertyName)
                    ? responseObject.GetValue(responsePropertyName)
                    : null;
            }
            else
            {
                throw new RemoteServiceOperationFailureException("SendCloud", "SendCloud.Sohu.com", string.Format("{0}.{1}", module, action), null, null, responseObject.GetValue("errors").SafeToString());
            }
        }

        #endregion
    }
}
