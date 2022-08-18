using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CGEditor.CGItemWrappers
{
    interface IChangeNotify
    {
        event ItemChangedHandler ItemChanged;
    }

    public delegate void ItemChangedHandler(object sender, ItemChangedArgs e);
    public class ItemChangedArgs : EventArgs
    {

        public ItemChangedArgs(string name, string value)
        {
            m_strPropertyName = name;
            m_strPropertyValue = value;
        }

        private string m_strPropertyName;
        public string PropertyName
        {
            get { return m_strPropertyName; }
            set { m_strPropertyName = value; }
        }

        private string m_strPropertyValue;
        public string PropertyValue
        {
            get { return m_strPropertyValue; }
            set { m_strPropertyValue = value; }
        }
    }
}
