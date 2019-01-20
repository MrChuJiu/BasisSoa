using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace BasisSoa.Common.AppSettingsHelper
{
    public static class ConfigManager
    {
        public static IConfiguration Configuration = null;

        public static void SetConfiguration(IConfiguration _configuration)
        {
            if (Configuration == null)
                Configuration = _configuration;
        }



    }
}
