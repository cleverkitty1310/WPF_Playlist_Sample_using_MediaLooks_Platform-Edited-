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
    /// Interaction logic for MFormatsControl.xaml
    /// </summary>
    public partial class MFormatsControl : UserControl
    {
        private IMFormat m_pMFormat;
        public bool m_bTextDescription = false;
        public eMFormatType m_eFormatType = eMFormatType.eMFT_Convert;
        public MFormatsControl()
        {
            InitializeComponent();
        }
        public bool MInputFormat
        {
            get { return m_eFormatType == eMFormatType.eMFT_Input ? true : false; }
            set { m_eFormatType = value ? eMFormatType.eMFT_Input : eMFormatType.eMFT_Convert; }
        }

        public bool MTextFormatDescription
        {
            get { return m_bTextDescription; }
            set { m_bTextDescription = value; }
        }

        public Object SetControlledObject(Object pObject)
        {
            Object pOld = (Object)m_pMFormat;
            try
            {
                m_pMFormat = (IMFormat)pObject;

                UpdateFormats();
            }
            catch (System.Exception) { }

            return pOld;
        }

        void UpdateFormats()
        {
            comboBoxAudio.Items.Clear();
            comboBoxVideo.Items.Clear();
            if (m_pMFormat != null)
            {
                FillVideoFormats();
                FillAudioFormats();
            }
        }

        void FillVideoFormats()
        {
            try
            {
                int nIndex;
                string strFormat;
                M_VID_PROPS vidProps;
                comboBoxVideo.Items.Clear();

                int nCount = 0;
                m_pMFormat.FormatVideoGetCount(m_eFormatType, out nCount);

                int n10805994 = 0;
                for (int i = 0; i < nCount; i++)
                {
                    m_pMFormat.FormatVideoGetByIndex(m_eFormatType, i, out vidProps, out strFormat);
                    if (m_bTextDescription)
                        comboBoxVideo.Items.Add(strFormat);
                    else
                        comboBoxVideo.Items.Add(vidProps.eVideoFormat);
                    if (vidProps.eVideoFormat == eMVideoFormat.eMVF_HD1080_5994i)
                        n10805994 = i;
                }

                m_pMFormat.FormatVideoGet(m_eFormatType, out vidProps, out nIndex, out strFormat);
                comboBoxVideo.SelectedIndex = n10805994;
            }
            catch (System.Exception) { }
        }

        void FillAudioFormats()
        {
            try
            {
                int nIndex;
                string strFormat;
                M_AUD_PROPS audProps;
                comboBoxAudio.Items.Clear();

                int nCount = 0;
                m_pMFormat.FormatAudioGetCount(m_eFormatType, out nCount);

                for (int i = 0; i < nCount; i++)
                {
                    m_pMFormat.FormatAudioGetByIndex(m_eFormatType, i, out audProps, out strFormat);

                    // For audio always text desc
                    comboBoxAudio.Items.Add(strFormat);
                }

                m_pMFormat.FormatAudioGet(m_eFormatType, out audProps, out nIndex, out strFormat);
                comboBoxAudio.SelectedIndex = 2;
            }
            catch (System.Exception) { }
        }

        private void comboBoxVideo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                M_VID_PROPS vidProps = new M_VID_PROPS();
                if (m_bTextDescription)
                {
                    string strFormat;
                    m_pMFormat.FormatVideoGetByIndex(m_eFormatType, comboBoxVideo.SelectedIndex, out vidProps, out strFormat);
                }
                else
                {
                    vidProps.eVideoFormat = (eMVideoFormat)comboBoxVideo.SelectedItem;
                }

                
                // vidProps.fccType = eMFCC.eMFCC_UYVY;
                //vidProps.eScaleType = eMScaleType.eMST_LetterBox;
                m_pMFormat.FormatVideoSet(m_eFormatType, ref vidProps);
            }
            catch (System.Exception)
            {
                FillVideoFormats();
            }
            
        }

        private void comboBoxAudio_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                M_AUD_PROPS audProps = new M_AUD_PROPS();
                string strFormat;
                m_pMFormat.FormatAudioGetByIndex(m_eFormatType, comboBoxAudio.SelectedIndex, out audProps, out strFormat);
                m_pMFormat.FormatAudioSet(m_eFormatType, ref audProps);
            }
            catch (System.Exception)
            {
                FillAudioFormats();
            }
        }


    }
}
