using System.Windows;
using MPLATFORMLib;

namespace WPFMControls
{
    public partial class FormPlaylist : Window
    {
        public IMPlaylist m_pPlaylist;
        public FormPlaylist()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            mPlaylistControl1.SetControlledObject(m_pPlaylist);
        }
    }
}
