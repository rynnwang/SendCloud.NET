using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Beyova.SendCloud.SDK.Model
{
    /// <summary>
    /// Class EmailTemplate.
    /// </summary>
    public class EmailTemplate
    {
        /// <summary>
        /// Gets or sets the name of the invoke.
        /// </summary>
        /// <value>The name of the invoke.</value>
        [JsonProperty("invoke_name")]
        public string InvokeName { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the HTML.
        /// </summary>
        /// <value>The HTML.</value>
        [JsonProperty("html")]
        public string Body { get; set; }

        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        /// <value>The subject.</value>
        [JsonProperty("subject")]
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the type of the email.
        /// </summary>
        /// <value>The type of the email.</value>
        [JsonProperty("email_type")]
        public ServiceType EmailType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is verified.
        /// </summary>
        /// <value><c>true</c> if this instance is verified; otherwise, <c>false</c>.</value>
        [JsonProperty("is_verify")]
        public bool IsVerified { get; set; }

        /// <summary>
        /// Gets or sets the created stamp.
        /// </summary>
        /// <value>The created stamp.</value>
        [JsonProperty("gmt_created")]
        public DateTime? CreatedStamp { get; set; }

        /// <summary>
        /// Gets or sets the last updated stamp.
        /// </summary>
        /// <value>The last updated stamp.</value>
        [JsonProperty("gmt_modified")]
        public DateTime? LastUpdatedStamp { get; set; }
    }
}