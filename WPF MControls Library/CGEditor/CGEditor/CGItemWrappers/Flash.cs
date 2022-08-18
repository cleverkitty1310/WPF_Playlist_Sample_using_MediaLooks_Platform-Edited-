using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows;
using System.Windows.Media;
using System.ComponentModel;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using System.Diagnostics;
using MLCHARGENLib;

namespace CGEditor.CGItemWrappers
{
    public class CGFlashItem : CGBaseItem, IPath
    {
        public CGFlashItem(string itemId) : base(itemId) { }

        public string Path
        {
            get
            {
                return GetStringProperty("img::path");
            }
            set
            {
                SetProperty("img::path", value);
                InvokeItemChanged(this, new ItemChangedArgs("path", value));
            }
        }
    }
}
