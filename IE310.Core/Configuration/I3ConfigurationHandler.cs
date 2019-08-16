using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Xml;

namespace IE310.Core.Configuration
{
    public class I3ConfigurationHandler : IConfigurationSectionHandler
    {
        public object Create(object parent, object configContext, XmlNode section)
        {
            Type type = Type.GetType(section.Attributes["type"].Value);
            object[] args = new object[] { section };
            object obj2 = null;
            try
            {
                obj2 = Activator.CreateInstance(type, args);
            }
            catch (Exception ex)
            {
            }
            return obj2;
        }
    }
}
