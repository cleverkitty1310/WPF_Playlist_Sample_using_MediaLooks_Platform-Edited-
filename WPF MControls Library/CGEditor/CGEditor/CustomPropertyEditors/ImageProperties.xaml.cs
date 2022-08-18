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
    public partial class ImageProperties : UserControl
    {
        private CGImageItem m_ImageItem;
        private bool bChangedByUI;

        public ImageProperties()
        {
            InitializeComponent();
        }

        public void SetControlledObject(CGImageItem item)
        {
            try
            {
                m_ImageItem = item;
                if (m_ImageItem != null)
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
            if (m_ImageItem != null)
            {
                bChangedByUI = false;
                TextBoxPath.Text = m_ImageItem.Path;
                bChangedByUI = true;
            }
        }

        private void TextBoxPath_TextChanged(object sender, TextChangedEventArgs e)
        {
            m_ImageItem.Path = TextBoxPath.Text;
        }

        private void OpenDialog_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFile = new Microsoft.Win32.OpenFileDialog();
            openFile.Multiselect = false;
            openFile.Filter = "All Images|*.BMP;*.JPG;*.JPEG;*.GIF;*.TIF;*.TIFF;*.PNG";
            if (openFile.ShowDialog() == true)
            {
                TextBoxPath.Text = openFile.FileName;
            }
        }

    }
}
