using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Beyova.SendCloud.SDK.Model
{
    /// <summary>
    /// Class TemplateEmailSubstitution.
    /// </summary>
    public class TemplateEmailSubstitution
    {
        /// <summary>
        /// Gets or sets to.
        /// </summary>
        /// <value>To.</value>
        [JsonProperty("to")]
        public List<string> To { get; set; }

        /// <summary>
        /// Gets or sets the substitution.
        /// </summary>
        /// <value>The substitution.</value>
        [JsonIgnore]
        public Dictionary<string, List<string>> Substitution { get; set; }

        /// <summary>
        /// Gets or sets the substitution output.
        /// </summary>
        /// <value>The substitution output.</value>
        [JsonProperty("sub")]
        public Dictionary<string, List<string>> SubstitutionOutput
        {
            get
            {
                return Substitution == null ? null : Substitution.ToDictionary(one => ToPlaceholder(one.Key), one => one.Value);
            }
            set
            {
                //do nothing
            }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateEmailSubstitution" /> class.
        /// </summary>
        /// <param name="placeholders">The placeholders.<remarks>
        /// Names as placeholder. No % is needed.
        /// </remarks></param>
        public TemplateEmailSubstitution(List<string> placeholders)
        {
            this.To = new List<string>();
            this.Substitution = new Dictionary<string, List<string>>();
            if (placeholders != null)
            {
                foreach (var one in placeholders)
                {
                    this.Substitution.Add(one, new List<string>());
                }
            }
        }

        /// <summary>
        /// Adds the specified to.
        /// </summary>
        /// <param name="to">To.</param>
        /// <param name="replacements">The replacements.</param>
        public void Add(string to, Dictionary<string, string> replacements)
        {
            this.To.Add(to);
            foreach (var one in this.Substitution)
            {
                one.Value.Add(replacements.SafeGetValue(one.Key));
            }
        }

        /// <summary>
        /// To the placeholder.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>System.String.</returns>
        protected static string ToPlaceholder(string name)
        {
            return string.Format("%{0}%", name);
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            return this.ToJson(false);
        }
    }
}
