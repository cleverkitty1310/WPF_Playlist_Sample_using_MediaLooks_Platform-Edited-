using System;
using System.Windows;
using MPLATFORMLib;

namespace WPFMControls
{
    public partial class FormLive : Window
    {
        public FormLive()
        {
            InitializeComponent();
        }
        public IMDevice m_pDevice;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            mLiveControl1.SetControlledObject(m_pDevice);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            mLiveControl1.SetControlledObject(null);
            m_pDevice = null;
            GC.Collect();
        }
    }
}
