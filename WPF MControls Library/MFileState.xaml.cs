using System;
using System.Windows;
using System.Windows.Forms;
using MPLATFORMLib;

namespace WPFMControls
{
    /// <summary>
    /// Interaction logic for MFileState.xaml
    /// </summary>
    public partial class MFileState
    {
        public MFileState()
        {
            InitializeComponent();
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += DispatcherTimerTick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            dispatcherTimer.Start();
        }
        private IMFile _mPFile;
        private System.Windows.Threading.DispatcherTimer dispatcherTimer;
        public Object SetControlledObject(Object pObject)
        {
            var pOld = (Object)_mPFile;
            try
            {
                _mPFile = (IMFile)pObject;

                UpdateState();
            }
            catch
            { }

            return pOld;
        }
        public void UpdateState()
        {
            try
            {
                eMState eState;
                double dblTime;
                _mPFile.FileStateGet(out eState, out dblTime);

                labelState.Content = eState.ToString().Substring(3);
                if (dblTime > 0)
                    labelState.Content += "(" + dblTime.ToString("0.000") + ")";

            }
            catch
            { }
        }

        void DispatcherTimerTick(object sender, EventArgs e)
        {
            UpdateState();
        }

        private void ButtonPlayClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_mPFile != null)
                {
                    // Check for emty file
                    string strPath;
                    _mPFile.FileNameGet(out strPath);
                    if (strPath == null)
                    {
                        var openDlg = new OpenFileDialog();
                        if (openDlg.ShowDialog() == DialogResult.OK)
                        {
                            _mPFile.FileNameSet(openDlg.FileName, "");
                        }
                    }

                    _mPFile.FilePlayStart();
                }
            }
            catch
            { }
        }

        private void ButtonPauseClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_mPFile != null)
                    _mPFile.FilePlayPause(0);
            }
            catch
            { }
        }

        private void ButtonStopClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_mPFile != null)
                {
                    _mPFile.FilePlayStop(0);
                }
            }
            catch
            { }
        }
    }
}
