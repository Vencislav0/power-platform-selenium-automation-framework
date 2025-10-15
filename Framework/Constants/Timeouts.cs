using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation_Framework.Framework.Constants
{
    public static class Timeouts
    {
        public static readonly TimeSpan LONG = TimeSpan.FromSeconds(50);
        public static readonly TimeSpan MEDIUM = TimeSpan.FromSeconds(20);
        public static readonly TimeSpan SHORT = TimeSpan.FromSeconds(5);
        public static readonly TimeSpan EXTRA_SHORT = TimeSpan.FromSeconds(2);
        public static readonly TimeSpan API = TimeSpan.FromSeconds(60);
        public static readonly TimeSpan DEFAULT_WAIT = TimeSpan.FromSeconds(10);
        public static readonly TimeSpan DEFAULT_INTERVAL = TimeSpan.FromSeconds(1);
        public static readonly TimeSpan WAIT_FOR_INTERVAL = TimeSpan.FromSeconds(0.5);
    }
}
