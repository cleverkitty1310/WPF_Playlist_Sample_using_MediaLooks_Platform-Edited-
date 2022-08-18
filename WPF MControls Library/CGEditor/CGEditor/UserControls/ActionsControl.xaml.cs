using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CGEditor.CGItemWrappers;

namespace CGEditor.CustomPropertyEditors
{
    public partial class ActionsControl : UserControl
    {
        private Editor m_Editor;

        public ActionsControl()
        {
            InitializeComponent();
        }

        public void SetControlledObject(object source)
        {
            try
            {
                if (source != null && source is Editor)
                {
                    m_Editor = (Editor)source;
                }
            }
            catch (System.Exception) { }
        }
    }
}
