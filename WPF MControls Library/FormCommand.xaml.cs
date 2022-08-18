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
    /// Interaction logic for FormCommand.xaml
    /// </summary>
    public partial class FormCommand : Window
    {
        public FormCommand()
        {
            InitializeComponent();
        }
        public IMItem m_pPlaylistItem;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string strCommand;
            string strProps;
            Object pTarget;
            m_pPlaylistItem.ItemCommandGet(out strCommand, out strProps, out pTarget);
            textBoxCommand.Text = strCommand;

            mPropsControl1.SetControlledObject(m_pPlaylistItem);
        }

        private void buttonSet_Click(object sender, RoutedEventArgs e)
        {
            m_pPlaylistItem.ItemCommandSet(textBoxCommand.Text, textBoxParams.Text, null);
        }
    }
}
