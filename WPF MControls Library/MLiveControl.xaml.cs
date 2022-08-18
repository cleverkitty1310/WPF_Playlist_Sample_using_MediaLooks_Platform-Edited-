using System;
using System.Collections.Generic;
using System.Globalization;
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
using BBIM;
using MPLATFORMLib;

namespace WPFMControls
{
    /// <summary>
    /// Interaction logic for MLiveControl.xaml
    /// </summary>
    public partial class MLiveControl : UserControl
    {
        IMDevice m_pDevice;
        public MLiveControl()
        {
            InitializeComponent();
        }
        public Object SetControlledObject(Object pObject)
        {
            Object pOld = (Object)m_pDevice;
            try
            {
                m_pDevice = (IMDevice)pObject;

                FillCombo("video", comboBoxVideo);
                UpdateDelay();
            }
            catch (System.Exception) { }

            if (pObject==null) 
            {
		try
		{
			dispatcherTimer.Stop();  
		}
		catch{}
		
	    }
            return pOld;
        }
        private void FillCombo(string strType, ComboBox cbxType)
        {
            cbxType.SelectionChanged -= new SelectionChangedEventHandler(ComboBoxAvSelectedIndexChanged);
            cbxType.Items.Clear();
            cbxType.SelectionChanged += new SelectionChangedEventHandler(ComboBoxAvSelectedIndexChanged);
            cbxType.Tag = strType;
            int nCount;
            m_pDevice.DeviceGetCount(0, strType, out nCount);
            cbxType.IsEnabled = nCount > 0;
            if (nCount > 0)
            {
                for (int i = 0; i < nCount; i++)
                {
                    string strName;
                    string strDesc;
                    m_pDevice.DeviceGetByIndex(0, strType, i, out strName, out strDesc);
                    cbxType.Items.Add(strName);
                }
                try
                {
                    int nIndex;
                    string strParam;
                    string strCur;
                    m_pDevice.DeviceGet(strType, out strCur, out strParam, out nIndex);
                    cbxType.SelectedIndex = !string.IsNullOrEmpty(strCur) ? cbxType.Items.IndexOf(strCur) : 0;
                }
                catch
                {
                    cbxType.SelectedIndex = 0;
                }
            }
        }
        private void FillComboFomat(string strType, ComboBox cbxTarget)
        {
            if (strType == "video")
            {
                int nCount;

                cbxTarget.SelectionChanged -= new SelectionChangedEventHandler(ComboBoxAvfSelectedIndexChanged);
                cbxTarget.Items.Clear();
                cbxTarget.SelectionChanged += new SelectionChangedEventHandler(ComboBoxAvfSelectedIndexChanged);
                ((IMFormat)m_pDevice).FormatVideoGetCount(eMFormatType.eMFT_Input, out nCount);
                cbxTarget.IsEnabled = nCount > 0;
                if (nCount > 0)
                {
                    M_VID_PROPS vidProps;
                    string strFormat;
                    for (int i = 0; i < nCount; i++)
                    {
                        ((IMFormat)m_pDevice).FormatVideoGetByIndex(eMFormatType.eMFT_Input, i, out vidProps, out strFormat);
                        cbxTarget.Items.Add(strFormat);

                    }
                    int nIndex;
                    ((IMFormat)m_pDevice).FormatVideoGet(eMFormatType.eMFT_Input, out vidProps, out nIndex, out strFormat);
                    cbxTarget.SelectedIndex = nIndex > 0 ? nIndex : 0;
                }

            }
            else if (strType == "audio")
            {
                int nCount;
                cbxTarget.SelectionChanged -= new SelectionChangedEventHandler(ComboBoxAvfSelectedIndexChanged);
                cbxTarget.Items.Clear();
                cbxTarget.SelectionChanged += new SelectionChangedEventHandler(ComboBoxAvfSelectedIndexChanged);
                ((IMFormat)m_pDevice).FormatAudioGetCount(eMFormatType.eMFT_Input, out nCount);
                cbxTarget.IsEnabled = nCount > 0;
                if (nCount > 0)
                {
                    M_AUD_PROPS audProps;
                    string strFormat;
                    for (int i = 0; i < nCount; i++)
                    {
                        ((IMFormat)m_pDevice).FormatAudioGetByIndex(eMFormatType.eMFT_Input, i, out audProps, out strFormat);
                        cbxTarget.Items.Add(strFormat);
                    }
                    int nIndex;
                    ((IMFormat)m_pDevice).FormatAudioGet(eMFormatType.eMFT_Input, out audProps, out nIndex, out strFormat);
                    cbxTarget.SelectedIndex = nIndex > 0 ? nIndex : 0;
                }
            }
            cbxTarget.Tag = strType;
        }
        private void ComboBoxAvSelectedIndexChanged(object sender, SelectionChangedEventArgs e)
        {
            var cbxChanged = (ComboBox)sender;
            var strType = (string)cbxChanged.Tag;
            try
            {
                m_pDevice.DeviceSet(strType, (string)cbxChanged.SelectedItem, "");
            }
            catch
            {
                MessageBox.Show("Can't set this device, it isn't supported", "Warning", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            switch (strType)
            {
                case "video":
                    FillCombo("audio", comboBoxAudio);
                    FillCombo(strType + "::line-in", comboBoxVL);
                    FillComboFomat(strType, comboBoxVF);
                    break;
                case "audio":
                    FillCombo(strType + "::line-in", comboBoxAL);
                    FillComboFomat(strType, comboBoxAF);
                    break;
            }
        }

        private void ComboBoxAvfSelectedIndexChanged(object sender, SelectionChangedEventArgs e)
        {
            var cbxChanged = (ComboBox)sender;
            var strType = (string)cbxChanged.Tag;

            if (strType == "video")
            {
                M_VID_PROPS vidProps;
                string strFormat;
                ((IMFormat)m_pDevice).FormatVideoGetByIndex(eMFormatType.eMFT_Input, cbxChanged.SelectedIndex, out vidProps, out strFormat);
                ((IMFormat)m_pDevice).FormatVideoSet(eMFormatType.eMFT_Input, ref vidProps);
            }
            else if (strType == "audio")
            {
                M_AUD_PROPS audProps;
                string strFormat;
                ((IMFormat)m_pDevice).FormatAudioGetByIndex(eMFormatType.eMFT_Input, cbxChanged.SelectedIndex, out audProps, out strFormat);
                ((IMFormat)m_pDevice).FormatAudioSet(eMFormatType.eMFT_Input, ref audProps);
            }
        }

        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            ((IMObject)m_pDevice).ObjectClose();
        }

        private void buttonInit_Click(object sender, RoutedEventArgs e)
        {
            ((IMObject)m_pDevice).ObjectStart(null);
            string objectName;
            ((IMObject)m_pDevice).ObjectNameGet(out objectName);
            preview.Source = new Uri("mplatform://" + objectName);
            preview.Visibility = checkBoxVideo.IsChecked == false ? Visibility.Hidden : Visibility.Visible;
            preview.IsMuted = checkBoxAudio.IsChecked == false;
            preview.Play();
           
        }
        private void CheckBoxVideoChecked(object sender, RoutedEventArgs e)
        {
            preview.Visibility = Visibility.Visible;
        }

        private void CheckBoxVideoUnchecked(object sender, RoutedEventArgs e)
        {
            preview.Visibility = Visibility.Hidden;
        }

        private void CheckBoxAudioChecked(object sender, RoutedEventArgs e)
        {
            preview.IsMuted = false;
        }

        private void CheckBoxAudioUnchecked(object sender, RoutedEventArgs e)
        {
            preview.IsMuted = true;
        }

        private void TrackBarValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var dblPos = trackBar.Value / trackBar.Maximum;
            preview.Volume = dblPos;
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                m_pDevice.DeviceShowProps("audio", "stream", 0);
            }
            catch { }
        }

        private void buttonA_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                m_pDevice.DeviceShowProps("audio", "device", 0);
            }
            catch { }
        }

        private void buttonVF_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                m_pDevice.DeviceShowProps("video", "stream", 0);
            }
            catch { }
        }

        private void buttonV_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                m_pDevice.DeviceShowProps("video", "device", 0);
            }
            catch { }
        }
        private System.Windows.Threading.DispatcherTimer dispatcherTimer;
        private void checkBoxDelay_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                IMProps pProps = (IMProps)m_pDevice;
                pProps.PropsSet("object::mdelay.enabled", "true");

                numericPos.IsEnabled = true;
                trackBarSeek.IsEnabled = true;
                dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
                dispatcherTimer.Tick += DispatcherTimerTick;
                dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 1000);
                dispatcherTimer.Start();
                //timerDelay.Enabled = checkBoxDelay.Checked;
            }
            catch (System.Exception) { }
        }

        private void DispatcherTimerTick(object sender, EventArgs e)
        {
           UpdatePos();
            //MessageBox.Show("111");
        }

        void UpdateDelay()
        {
            try
            {
                IMProps pProps = (IMProps)m_pDevice;
                string sValue;
                pProps.PropsGet("object::mdelay.enabled", out sValue);
                if (sValue == "true" || sValue == "1")
                    checkBoxDelay.IsChecked = true;
                else
                    checkBoxDelay.IsChecked = false;

                pProps.PropsGet("object::mdelay.buffer_duration", out sValue);  // The value in seconds


                pProps.PropsGet("object::mdelay.quality", out sValue);
                numericDelayQuality.Value = Decimal.Parse(sValue);

                pProps.PropsGet("object::mdelay.available", out sValue);
                numericDelayTime.Value = Decimal.Parse(sValue);
                trackBarSeek.Minimum = -1 * (int)Decimal.Parse(sValue);
                trackBarSeek.TickFrequency = -1 * trackBarSeek.Minimum / 20;

                pProps.PropsGet("object::mdelay.time", out sValue);
                numericPos.Value = Decimal.Parse(sValue);
                trackBarSeek.Value = -1 * Int32.Parse(sValue);
            }
            catch (System.Exception) { }
        }

        private void UpdatePos()
        {
            try
            {
                IMProps pProps = (IMProps)m_pDevice;
                string sValue;
                
                pProps.PropsGet("object::mdelay.available", out sValue);
                numericDelayTime.Value = Convert.ToDecimal(sValue, System.Globalization.CultureInfo.InvariantCulture);
                trackBarSeek.Minimum = -1 * (int)Convert.ToDecimal(sValue, System.Globalization.CultureInfo.InvariantCulture);
                trackBarSeek.TickFrequency = -1 * trackBarSeek.Minimum / 20;

                pProps.PropsGet("object::mdelay.time", out sValue);
                trackBarSeek.Value = -1 * Convert.ToInt32(Convert.ToDouble(sValue, System.Globalization.CultureInfo.InvariantCulture));
            }
            catch (System.Exception) { }
        }

        private void checkBoxDelay_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                IMProps pProps = (IMProps)m_pDevice;
                pProps.PropsSet("object::mdelay.enabled", "false");

                numericPos.IsEnabled = false;
                trackBarSeek.IsEnabled = false;
                //dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
                //dispatcherTimer.Tick += DispatcherTimerTick;
                //dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 1000);
                dispatcherTimer.Stop();
                //timerDelay.Enabled = checkBoxDelay.Checked;
            }
            catch (System.Exception) { }
        }

        private void trackBarSeek_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                IMProps pProps = (IMProps)m_pDevice;
                pProps.PropsSet("object::mdelay.time", (-1 * trackBarSeek.Value).ToString());
                numericPos.ValueChanged -= numericPos_ValueChanged;
                numericPos.Value = Convert.ToDecimal(-1 * trackBarSeek.Value, System.Globalization.CultureInfo.InvariantCulture);
                numericPos.ValueChanged += numericPos_ValueChanged;
            }
            catch (System.Exception) { }
        }

        private void numericDelayQuality_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                IMProps pProps = (IMProps)m_pDevice;
                pProps.PropsSet("object::mdelay.quality", numericDelayQuality.Value.ToString());
            }
            catch (System.Exception) { }
        }

        private void numericPos_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                IMProps pProps = (IMProps)m_pDevice;
                pProps.PropsSet("object::mdelay.time", numericPos.Value.ToString());
                trackBarSeek.Value = -1 * (int)numericPos.Value;
            }
            catch (System.Exception) { }
        }
    }
}
