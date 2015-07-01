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
        public string Html { get; set; }

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