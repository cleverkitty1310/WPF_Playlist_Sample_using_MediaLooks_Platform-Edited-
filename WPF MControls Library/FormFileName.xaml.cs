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
using MPLATFORMLib;

namespace WPFMControls
{
    /// <summary>
    /// Interaction logic for FormFileName.xaml
    /// </summary>
    public partial class FormFileName : Window
    {
        public IMFile m_pFile;
        public FormFileName()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            mFileName1.SetControlledObject(m_pFile);
            mPropsControl1.SetControlledObject(m_pFile);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mFileName1.SetControlledObject(null);
            mPropsControl1.SetControlledObject(null);
        }
    }
}
