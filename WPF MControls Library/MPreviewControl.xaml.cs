using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using MPLATFORMLib;

namespace WPFMControls
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class MPreviewControl
    {

        public IMPreview MpPreview;
        public IMObject MpObject;
        public MPreviewControl()
        {
            InitializeComponent();
        }

        public Object SetControlledObject(Object pObject)
        {
            var pOld = (Object)MpPreview;
            try
            {
                if (MpPreview != null)
                {
                    MpPreview.PreviewEnable("", 0, 0);
                }
                MpPreview = (IMPreview)pObject;
                MpObject = (IMObject)pObject;
                string objectName;
                MpObject.ObjectNameGet(out objectName);
                preview.Source = new Uri("mplatform://" + objectName);
                preview.Visibility = checkBoxVideo.IsChecked == false ? Visibility.Hidden : Visibility.Visible;
                preview.IsMuted = true;
                preview.Play();
            }
            catch
            { }
            return pOld;
        }

        private void CheckBoxVideoChecked(object sender, RoutedEventArgs e)
        {
            preview.Visibility = Visibility.Visible;
            if (dblAnim != null)
            {
                dblAnim.Freeze();
            }
        }

        private DoubleAnimation dblAnim;
        private void CheckBoxVideoUnchecked(object sender, RoutedEventArgs e)
        {
            preview.Visibility = Visibility.Hidden;
        }

        private void CheckBoxAudioChecked(object sender, RoutedEventArgs e)
        {
            preview.IsMuted = false;
            var dblPos = trackBar.Value / trackBar.Maximum;
            preview.Volume = dblPos;
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
    }
}
