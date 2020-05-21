using OTLib.Collections;
using System;
using System.Diagnostics;
using System.IO;
using System.Xml.Linq;

namespace OTLib.Server.Items
{
    public class ItemsXmlReader
    {
        public string Directory { get; private set; }
        public string File { get; private set; }

        public bool Read(string directory, ServerItemList items)
        {
            if (directory == null)
                throw new ArgumentNullException(nameof(directory));

            if (!System.IO.Directory.Exists(directory))
                throw new DirectoryNotFoundException(directory);

            if (items == null)
                throw new ArgumentNullException(nameof(items));

            string file = Path.Combine(directory, "items.xml");
            if (!System.IO.File.Exists(file))
                return false;

            try
            {
                XDocument xml = XDocument.Load(file);
                foreach (XElement element in xml.Root.Elements("item"))
                {
                    if (element.Attribute("id") != null)
                    {
                        ushort id = ushort.Parse(element.Attribute("id").Value);
                        if (items.TryGetValue(id, out ServerItem item))
                            ParseItem(item, element);
                    }
                    else if (element.Attribute("fromid") != null && element.Attribute("toid") != null)
                    {
                        ushort fromid = ushort.Parse(element.Attribute("fromid").Value);
                        ushort toid = ushort.Parse(element.Attribute("toid").Value);
                        for (ushort id = fromid; id <= toid; id++)
                        {
                            if (items.TryGetValue(id, out ServerItem item))
                                ParseItem(item, element);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
                return false;
            }

            Directory = directory;
            File = file;

            return true;
        }

        protected virtual bool ParseItem(ServerItem item, XElement element)
        {
            if (element.Attribute("name") != null)
                item.NameXml = element.Attribute("name").Value;
            else
                Trace.WriteLine($"The item  {item.ID} is unnamed.");

            return true;
        }
    }
}
