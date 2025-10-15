using Automation_Framework.Framework.ElementWrappers;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation_Framework.Framework.Utilities
{
    public class Utils
    {
        public static string GetRequiredConfig(IConfiguration config, string key)
        {
            return config[key] ?? throw new InvalidOperationException($"Missing required config key: {key}");
        }

        
    }


}
