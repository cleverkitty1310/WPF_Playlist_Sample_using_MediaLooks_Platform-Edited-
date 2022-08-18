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
    public partial class TextContentEditor : UserControl
    {
        private bool m_bChangedByUI; //Indicates that value was changed by control UI
        private bool m_bAceptExternalUpdate; //Enables or disables external update of the control

        private IText m_Item;
        private Editor m_Editor;

        public void SetEditor(Editor editor)
        {
            m_Editor = editor;
        }


        public TextContentEditor()
        {
            InitializeComponent();
            m_bChangedByUI = true;
            m_bAceptExternalUpdate = true;
            this.IsEnabled = false;
        }

        public void SetControlledObject(object item)
        {
            try
            {
                if (item != null && item is IText)
                {
                    m_Item = (IText)item;
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
                string strParsedText = parseFromCGFormat(m_Item.Text);
                TextBoxText.Text = strParsedText;
                m_bChangedByUI = true;
            }
        }

        private void ClearControls()
        {
            m_bChangedByUI = false;
            TextBoxText.Text = string.Empty;
            m_bChangedByUI = true;
        }

        private string parseToCGFormat (string text)
        {
            string strRes = text;
            if (strRes != null && strRes != string.Empty)
            {
                strRes = strRes.Replace("\r\n", "<br/>");
            }
            return strRes;
        }

        private string parseFromCGFormat(string text)
        {
            string strRes = text;
            if (strRes != null && strRes != string.Empty)
            {
                strRes = strRes.Replace("\t", ""); //remove tabs
                //Remove starting \r\n's
                while (true)
                {
                    if (strRes.Length > 0 && strRes.StartsWith("\r\n"))
                        strRes = strRes.Substring(2);
                    else
                        break;
                }
                //Remove trailing \r\n's
                while (true)
                {
                    if (strRes.Length > 0 && strRes.EndsWith("\r\n"))
                        strRes = strRes.Substring(0, strRes.Length - 2);
                    else
                        break;
                }
                //Replace <br/> tags
                strRes = strRes.Replace(@"<br/>", "");
                strRes = strRes.Replace(@"<br />", "");
            }
            return strRes;
        }

        void External_ItemChanged(object sender, ItemChangedArgs e)
        {
            if ( m_bAceptExternalUpdate)
            {
                if (e.PropertyName == "textcontent" || e.PropertyName == "xml")
                    FillControls();
            }
        }

        private void TextBoxText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (m_Item != null && TextBoxText.Text != string.Empty && m_bChangedByUI)
            {
                UndoRedoManager.Instance().Push<HistoryItem>(m_Editor.UndoAction, new HistoryItem(m_Editor.CurrentState, ((CGBaseItem)m_Item).ID));
                m_bAceptExternalUpdate = false;
                string strIn = parseToCGFormat(TextBoxText.Text);
                m_Item.Text = strIn;
                m_bAceptExternalUpdate = true;
            }
        }

    }
}
