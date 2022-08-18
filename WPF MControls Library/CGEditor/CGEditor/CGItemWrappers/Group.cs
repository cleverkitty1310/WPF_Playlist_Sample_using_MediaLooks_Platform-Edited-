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
    [DisplayName("Group Item Properties")]
    public class CGGroupItem : CGBaseItem
    {
        public CGGroupItem(string itemId) : base(itemId) { }

    }
}
