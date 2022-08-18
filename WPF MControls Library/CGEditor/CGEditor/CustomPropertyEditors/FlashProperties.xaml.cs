using System;
using System.Collections.ObjectModel;
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
using Xceed.Wpf.Toolkit;
using CGEditor.CGItemWrappers;
using CGEditor.Misc;

namespace CGEditor.CustomPropertyEditors
{
    /// <summary>
    /// Interaction logic for BasicProperties.xaml
    /// </summary>
    public partial class FlashProperties : UserControl
    {
        private CGFlashItem m_FlashItem;
        private bool bChangedByUI;

        public FlashProperties()
        {
            InitializeComponent();
        }

        public void SetControlledObject(CGFlashItem item)
        {
            try
            {
                m_FlashItem = item;
                if (m_FlashItem != null)
                    FillValues();
                else
                    ClearControls();
            }
            catch (System.Exception) { }
        }

        private void ClearControls()
        {
            bChangedByUI = false;
            TextBoxPath.Text = "";
            bChangedByUI = true;
        }

        private void FillValues()
        {
            if (m_FlashItem != null)
            {
                bChangedByUI = false;
                TextBoxPath.Text = m_FlashItem.Path;
                bChangedByUI = true;
            }
        }

        private void TextBoxPath_TextChanged(object sender, TextChangedEventArgs e)
        {
            m_FlashItem.Path = TextBoxPath.Text;
        }

        private void OpenDialog_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFile = new Microsoft.Win32.OpenFileDialog();
            openFile.Multiselect = false;
            openFile.Filter = "Flash Files (*.swf)|*.swf";
            if (openFile.ShowDialog() == true)
            {
                TextBoxPath.Text = openFile.FileName;
            }
        }

    }
}
