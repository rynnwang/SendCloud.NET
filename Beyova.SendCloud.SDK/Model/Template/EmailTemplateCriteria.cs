using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Beyova.SendCloud.SDK.Model
{
    /// <summary>
    /// Class EmailTemplateCriteria.
    /// </summary>
    public class EmailTemplateCriteria
    {
        /// <summary>
        /// Gets or sets the name of the invoke.
        /// </summary>
        /// <value>The name of the invoke.</value>
        public string InvokeName { get; set; }

        /// <summary>
        /// Gets or sets the start.
        /// </summary>
        /// <value>The start.</value>
        public int? Start { get; set; }

        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        /// <value>The count.</value>
        public int? Count { get; set; }

    }
}
