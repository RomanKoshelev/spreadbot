// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Connectors.Ebay
// MipSecretData.cs
// romak_000, 2015-03-19 15:49

using System.Configuration;
using Crocodev.Common.SmartConfiguration;

namespace Spreadbot.Core.Connectors.Ebay.Configuration.Elements
{
    public class MipSecretData : SmartConfigurationElement
    {
        [ConfigurationProperty("UserName", IsRequired = true)]
        public string UserName
        {
            get { return (string) this[GetPropertyName()]; }
        }

        [ConfigurationProperty("Password", IsRequired = true)]
        public string Password
        {
            get { return (string) this[GetPropertyName()]; }
        }
    }
}