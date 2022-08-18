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
using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Folding;
using ICSharpCode.AvalonEdit.Highlighting;

namespace CGEditor.UserControls
{
    /// <summary>
    /// Interaction logic for XMLEditorControl.xaml
    /// </summary>
    public partial class XMLEditorControl : UserControl
    {
        public XMLEditorControl()
        {
            InitializeComponent();
            TextBoxXMLDesc.SyntaxHighlighting = HighlightingManager.Instance.GetDefinition("XML");
            TextBoxXMLDesc.ShowLineNumbers = true;
            m_bAceptExternalUpdate = true;
        }

        private bool m_bChangedByUI; //Indicates that value was changed by control UI
        private bool m_bAceptExternalUpdate; //Enables or disables external update of the control

        private CGBaseItem m_Item;

        public void SetControlledObject(object item)
        {
            try
            {
                if (item != null && item is CGBaseItem)
                {
                    m_Item = (CGBaseItem)item;
                    FillControls();
                    if (item is IChangeNotify)
                    {
                        ((IChangeNotify)item).ItemChanged += External_ItemChanged;
                    }
                    this.IsEnabled = true;
                }
                else
                {
                    ClearControls();
                    m_Item = null;
                    this.IsEnabled = false;
                }
            }
            catch (System.Exception) { }
        }

        private void FillControls()
        {
            if (m_Item != null)
            {
                ClearControls();
                m_bChangedByUI = false;
                TextBoxXMLDesc.Text = m_Item.XML;
                m_bChangedByUI = true;
            }
        }

        private void ClearControls()
        {
            m_bChangedByUI = false;
            TextBoxXMLDesc.Text = "";
            m_bChangedByUI = true;
        }

        void External_ItemChanged(object sender, ItemChangedArgs e)
        {
            if (m_bAceptExternalUpdate)
            {
                FillControls();
            }
        }

        private void ApplyXML_Click(object sender, RoutedEventArgs e)
        {
            if (TextBoxXMLDesc.Text != string.Empty)
            {
                m_bAceptExternalUpdate = false;
                m_Item.XML = TextBoxXMLDesc.Text;
                m_bAceptExternalUpdate = true;
            }
        }
    }
}
