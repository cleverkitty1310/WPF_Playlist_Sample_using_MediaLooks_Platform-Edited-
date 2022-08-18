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
using MPLATFORMLib;

namespace WPFMControls
{
    /// <summary>
    /// Interaction logic for MFileRateControl.xaml
    /// </summary>
    public partial class MFileRateControl : UserControl
    {
        private IMFile m_pMFile;
        public MFileRateControl()
        {
            InitializeComponent();
        }
        public Object SetControlledObject(Object pObject)
        {
            Object pOld = (Object)m_pMFile;
            try
            {
                m_pMFile = (IMFile)pObject;

                UpdateRate();
            }
            catch (System.Exception) { }

            return pOld;
        }

        void UpdateRate()
        {
            try
            {
                double dblRate;
                m_pMFile.FileRateGet(out dblRate);
                trackBarRate.Value = (int)(dblRate / 5.0 * trackBarRate.Maximum + 0.5);
                labelRate.Content = (dblRate).ToString("0.0%");
            }
            catch (System.Exception) { }
        }

        private void trackBarRate_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                double dblRate = (double)trackBarRate.Value * 5.0 / trackBarRate.Maximum;
                m_pMFile.FileRateSet(dblRate);
                labelRate.Content = (dblRate).ToString("0.0%");
            }
            catch (System.Exception) { }
        }

        private void trackBarRate_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                int nTBPadding = 14;
                double dblPos = (double)(e.GetPosition(null).X - nTBPadding) / (trackBarRate.Width - nTBPadding * 2);
                dblPos = dblPos > 0 ? dblPos < 1.0 ? dblPos : 1.0 : 0;
                trackBarRate.Value = trackBarRate.Minimum + (int)((trackBarRate.Maximum - trackBarRate.Minimum) * dblPos + 0.5);

                // For update rate
                trackBarRate_ValueChanged(sender, null);
            }
            catch (System.Exception) { }
        }

        private void buttonRev10_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                m_pMFile.FileRateSet(-10.0);
                UpdateRate();
            }
            catch (System.Exception) { }
        }

        private void buttonRev2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                m_pMFile.FileRateSet(-2.0);
                UpdateRate();
            }
            catch (System.Exception) { }
        }

        private void buttonRev1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                m_pMFile.FileRateSet(-1.0);
                UpdateRate();
            }
            catch (System.Exception) { }
        }

        private void buttonRev5_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                m_pMFile.FileRateSet(-0.5);
                UpdateRate();
            }
            catch (System.Exception) { }
        }

        private void buttonFor5_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                m_pMFile.FileRateSet(0.5);
                UpdateRate();
            }
            catch (System.Exception) { }
        }

        private void buttonFor1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                m_pMFile.FileRateSet(1.0);
                UpdateRate();
            }
            catch (System.Exception) { }
        }

        private void buttonFor2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                m_pMFile.FileRateSet(2.0);
                UpdateRate();
            }
            catch (System.Exception) { }
        }

        private void buttonFor10_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                m_pMFile.FileRateSet(10.0);
                UpdateRate();
            }
            catch (System.Exception) { }
        }
    }
}
