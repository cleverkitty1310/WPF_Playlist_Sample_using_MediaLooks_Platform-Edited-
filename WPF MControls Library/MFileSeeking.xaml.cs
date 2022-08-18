using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MControls;
using MPLATFORMLib;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using UserControl = System.Windows.Controls.UserControl;

namespace WPFMControls
{
    /// <summary>
    /// Interaction logic for MFileSeeking.xaml
    /// </summary>
    public partial class MFileSeeking : UserControl
    {
        public MFileSeeking()
        {
            InitializeComponent();
        }
        private IMFile m_pFile;
        private System.Windows.Threading.DispatcherTimer dispatcherTimer;
        // Common controls methods
        public Object SetControlledObject(Object pObject)
        {
            Object pOld = (Object)m_pFile;
            try
            {
                m_pFile = (IMFile)pObject;

                UpdateControl();

                // Enable timer -> update by timer
                dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
                dispatcherTimer.Tick += DispatcherTimerTick;
                dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
                dispatcherTimer.Start();
            }
            catch (System.Exception) { }

            return pOld;
        }

        private void DispatcherTimerTick(object sender, EventArgs e)
        {
            UpdatePos();
        }

        public void UpdateControl()
        {
            UpdatePos();
        }

        public void UpdatePos()
        {
            try
            {
                // Get In/Out
                double dblIn, dblOut, dblLen;
                m_pFile.FileInOutGet(out dblIn, out dblOut, out dblLen);
                dblOut = dblOut > dblIn ? dblOut : dblLen;

                // Get pos
                double dblPos;
                m_pFile.FilePosGet(out dblPos);


                // Correct in/out according to check state
                if (checkBoxInOut.IsChecked==true)
                {
                    labelInOut.Visibility = Visibility.Hidden;
                    dblLen = dblOut - dblIn;
                    dblPos -= dblIn;
                }
                else
                {
                    if (dblLen == 0.0) return;
                        labelInOut.Width = (int)(labelPosBase.Width * (dblOut - dblIn) / dblLen + 0.5);
                        Canvas.SetLeft(labelInOut, labelPosBase.Margin.Left + (int)(labelPosBase.Width * dblIn / dblLen + 0.5));
                        //labelPosBase.SetLeft
                        //labelInOut.Canvas.Left = labelPosBase.Margin.Left + (int)(labelPosBase.Width * dblIn / dblLen + 0.5);
                        //labelPosBase.canvas

                        labelInOut.Visibility = Visibility.Visible;
                  
                    
                }

                dblLen = dblLen > 0 ? dblLen : 0.001;
                dblPos = dblPos > 0 ? dblPos : 0;
                dblPos = dblPos < dblLen ? dblPos : dblLen;

                labelPosStr.Content = MHelpers.PosToString(dblPos, 1) + " / " + MHelpers.PosToString(dblLen, 1);
                labelPos.Width = (int)(labelPosBase.Width * dblPos / dblLen + 0.5);
            }
            catch (System.Exception) { }
        }

        private void SetPos(double dblValue, double dblPreroll)
        {
            try
            {
                // Get In/Out
                double dblIn, dblOut, dblLen;
                m_pFile.FileInOutGet(out dblIn, out dblOut, out dblLen);

                // Correct in/out according to check state
                if (checkBoxInOut.IsChecked == true)
                {
                    dblLen = (dblOut > dblIn ? dblOut : dblLen) - dblIn;
                }
                else
                {
                    dblIn = 0;
                }

                dblLen = dblLen > 0 ? dblLen : 0.001;


                double dblPos = dblIn + dblLen * dblValue;
                m_pFile.FilePosSet(dblPos, dblPreroll);

                UpdatePos();
            }
            catch (System.Exception) { }
        }

        private void slider1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SetPos((double)trackBarSeek.Value / (double)trackBarSeek.Maximum, 0);
        }

        private void trackBarSeek_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int nTBPadding = 14;
            double dblPos = (double)(e.GetPosition(null).X - nTBPadding) / (trackBarSeek.Width - nTBPadding * 2);
            dblPos = dblPos > 0 ? dblPos < 1.0 ? dblPos : 1.0 : 0;
            trackBarSeek.Value = (int)(trackBarSeek.Maximum * dblPos + 0.5);
            SetPos(dblPos, 0.5);
        }

        private void checkBoxInOut_Checked(object sender, RoutedEventArgs e)
        {
            UpdatePos();
        }

        private void checkBoxInOut_Unchecked(object sender, RoutedEventArgs e)
        {
            UpdatePos();
        }

        private void buttonRewind_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (m_pFile != null)
                    m_pFile.FilePosSet(MHelpers.ParsePos(textBoxRew.Text), 1.0);
            }
            catch (System.Exception) { }
        }

        private void textBoxRew_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return || e.Key == Key.Tab)
                textBoxRew.Text = MHelpers.PosToString(MHelpers.ParsePos(textBoxRew.Text));
        }
    }
}
