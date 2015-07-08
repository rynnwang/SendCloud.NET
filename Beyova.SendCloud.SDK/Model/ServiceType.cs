using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Beyova.SendCloud.SDK.Model
{
    /// <summary>
    /// Enum ServiceType
    /// </summary>
    public enum ServiceType
    {
        /// <summary>
        /// 触发
        /// </summary>
        ByTrigger = 0,
        /// <summary>
        /// 批量
        /// </summary>
        ByBulk = 1
    }
}