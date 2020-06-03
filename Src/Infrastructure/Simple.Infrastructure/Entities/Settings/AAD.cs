using System;
using System.Collections.Generic;
using System.Text;

namespace Simple.Infrastructure.Entities.Settings
{
    public class AADSettings
    {
        public Guid Tenant { get; set; }
        public Guid ClientId { get; set; }
        public string RedirectUri { get; set; }
        public string CacheLocation { get; set; }
    }
}
