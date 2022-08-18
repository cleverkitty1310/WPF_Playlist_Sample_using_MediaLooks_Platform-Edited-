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
using System.Windows.Shapes;

namespace CGEditor.ContentManagers
{
    /// <summary>
    /// Interaction logic for TextContentManager.xaml
    /// </summary>
    public partial class TextContentManager : Window
    {
        public TextContentManager()
        {
            InitializeComponent();
        }

        private string m_strNewContent;
        public string NewContent
        {
            get { return m_strNewContent; }
            set { m_strNewContent = value; }
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFile = new Microsoft.Win32.OpenFileDialog();
            openFile.Multiselect = false;
            openFile.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";

            if (openFile.ShowDialog() == true)
            {
                try
                {
                    string text = System.IO.File.ReadAllText(openFile.FileName);
                    TextBoxContent.Text = text;
                }
                catch (System.Exception ex)
                {
                	
                }
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            m_strNewContent = TextBoxContent.Text;
            this.DialogResult = true;
            this.Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.Key == Key.Enter)
            //{
            //    ButtonOk_Click(this, new RoutedEventArgs());
            //}
            if (e.Key == Key.Escape)
            {
                ButtonCancel_Click(this, new RoutedEventArgs());
            }
        }


    }
}
